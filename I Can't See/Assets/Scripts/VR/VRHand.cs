using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;


/// <summary>
/// Class for handiling player Hands in VR
/// </summary>
[RequireComponent(typeof(ConfigurableJoint))]
public class VRHand : MonoBehaviour
{

    //VR Input abstracts away any VR Input management just get the actions by calling
    //vrInputController.GrabAction
    [SerializeField]
    private VRInput vrInputController;
    public VRInput VRInputController { get { return vrInputController; } }

    //List of interactable that we are colliding with
    private List<Interactable> colldingIteractables;

    //Info about objects we are holding
    private Holdable heldObject = null;
    public Holdable HeldObject { get { return heldObject;  } }

    [SerializeField]
    private ConfigurableJoint holdJoint = null;

    private void Start()
    {
        //Intialise list of colliding iteractables
        colldingIteractables = new List<Interactable>();
    }

    // Update is called once per frame
    void Update()
    {
        //Do Actions for each action that we are collding with
        foreach(Interactable obj in colldingIteractables)
        {
            obj.DoAction(this);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Get interactable component
        Interactable collidedInteractable = collision.gameObject.GetComponent<Interactable>();

        //Add items on collision enter to our update list
        if (collidedInteractable != null)
        {
            colldingIteractables.Add(collidedInteractable);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        //Get interactable component
        Interactable collidedInteractable = collision.gameObject.GetComponent<Interactable>();

        //Add items on collision enter to our update list
        if (collidedInteractable != null)
        {
            colldingIteractables.Remove(collidedInteractable);
        }
    }


    /// <summary>
    /// Attach and object to the hand
    /// </summary>
    /// <param name="obj"></param>
    public void AttachObject(Holdable obj, bool a_FixedX = false, bool a_FixedY = false, bool a_FixedZ = false)
    {
        //null Check
        if(obj == null || heldObject != null)
        {
            return;
        }
        

        //If we are held by a different hand - detach the object from that hand
        if(obj.CurrentHolder != null)
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

        //null Check
        if (obj == null || obj != heldObject)
        {
            return;
        }

        //Disconnect from the hand
        holdJoint.connectedBody = null;

        //Apply Velocity
        Rigidbody objRB = obj.GetComponent<Rigidbody>();
        objRB.velocity = vrInputController.CurrentPose.GetVelocity();
        objRB.angularVelocity = vrInputController.CurrentPose.GetAngularVelocity();

        //Null out vars
        heldObject = null;
        heldObject.CurrentHolder = null;
    }
}
