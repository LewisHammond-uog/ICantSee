﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR;


/// <summary>
/// Class for handiling player Hands in VR
/// </summary>
public class VRHand : MonoBehaviour
{

    //VR Input abstracts away any VR Input management just get the actions by calling
    //vrInputController.GrabAction
    [SerializeField]
    private VRInput vrInputController;
    public VRInput VRInputController { get { return vrInputController; } }

    private SteamVR_Behaviour_Pose pose = null;
    public SteamVR_Behaviour_Pose CurrentPose { get { return pose; } }

    //List of interactable that we are colliding with
    private List<Interactable> collidingIteractables;

    //Store if the controller is being tracked by the base stations
    public ETrackingResult ControllerTrackingState { private set; get; }

    [SerializeField]
    private Joint holdJoint = null;

    //Info about objects we are holding
    public Holdable HeldObject
    {
        get
        {

            if (holdJoint.connectedBody?.GetComponent<Holdable>())
            {
                return holdJoint.connectedBody.GetComponent<Holdable>();
            }

            return null;

        }
    }

    private void Start()
    {
        //Intialise list of colliding iteractables
        collidingIteractables = new List<Interactable>();

        //Make sure that we have connected a joint and vr input controller
        if (vrInputController == null)
        {
            Debug.LogWarning("WARNING: VR Input Controller not assigned to: " + gameObject.name);
        }
        if (holdJoint == null)
        {
            Debug.LogWarning("WARNING: Hold Joint not assigned to: " + gameObject.name);
        }
    }



    // Update is called once per frame
    void Update()
    {
        //Check if controller is being tracked
        if (ControllerTrackingState != ETrackingResult.Running_OK)
        {
            return;
        }

        //Do Actions for each action that we are collding with
        foreach (Interactable obj in collidingIteractables)
        {
            obj.DoAction(this);
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        //Get interactable component
        Interactable collidedInteractable = other.gameObject.GetComponent<Interactable>();

        //Add items on collision enter to our update list
        if (collidedInteractable != null)
        {
            collidingIteractables.Add(collidedInteractable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Get interactable component
        Interactable collidedInteractable = other.gameObject.GetComponent<Interactable>();

        //Add items on collision enter to our update list
        if (collidedInteractable != null)
        {
            collidingIteractables.Remove(collidedInteractable);
        }
    }


    /// <summary>
    /// Attach and object to the hand
    /// </summary>
    /// <param name="obj"></param>
    public void AttachObject(Holdable obj)
    {
        //Check if controller is being tracked
        if(ControllerTrackingState != ETrackingResult.Running_OK)
        {
            return;
        }

        //null Check
        if (obj == null)
        {
            return;
        }

        //Check that we are not already holding an object
        if(HeldObject != null)
        {
            return;
        }

        //If we are held by a different hand - don't allow pickup
        if (obj.CurrentHolder != null)
        {
            obj.CurrentHolder.DetachObject(obj);
        }

        //Position object to controller
        obj.transform.position = transform.position;

        //Attach to Hand
        Rigidbody objRB = obj.GetComponent<Rigidbody>();
        holdJoint.connectedBody = objRB;

        //Set this to be the active hand
        obj.CurrentHolder = this;

    }

    /// <summary>
    /// Detach  an object from the hand
    /// </summary>
    /// <param name="obj"></param>
    public void DetachObject(Holdable obj)
    {
        //Check if controller is being tracked
        if (ControllerTrackingState != ETrackingResult.Running_OK)
        {
            return;
        }

        //null Check
        if (obj == null || obj != HeldObject || HeldObject == null)
        {
            return;
        }

        //Disconnect from the hand
        holdJoint.connectedBody = null;

        //Apply Velocity
        Rigidbody objRB = obj.GetComponent<Rigidbody>();
        objRB.velocity = CurrentPose.GetVelocity();
        objRB.angularVelocity = CurrentPose.GetAngularVelocity();

        //Null out vars
        obj.CurrentHolder = null;

    }

    /// <summary>
    /// Gets the state of a steam VR action
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    public bool GetActionState(SteamVR_Action_Boolean action)
    {
        return action.GetState(pose.inputSource);
    }

    /// <summary>
    /// Updates the tracking state of the controller
    /// Event called when SteamVR detected a tracking update change
    /// </summary>
    /// <param name="fromAction"></param>
    /// <param name="fromSource"></param>
    /// <param name="trackingState"></param>
    void UpdateControllerTrackingState(SteamVR_Behaviour_Pose fromAction, SteamVR_Input_Sources fromSource, ETrackingResult trackingState)
    {
        ControllerTrackingState = trackingState;
    }

    #region Event Subs/Unsubs
    private void OnEnable()
    {

        //Assign Pose
        pose = GetComponent<SteamVR_Behaviour_Pose>();

        CurrentPose.onTrackingChangedEvent += UpdateControllerTrackingState;
    }
    private void OnDisable()
    {
        CurrentPose.onTrackingChangedEvent -= UpdateControllerTrackingState;
    }
    #endregion

}

//Lewis Hammond