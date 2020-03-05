using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Door Handle, an object that the player can drag around the world
/// Child of the door object
/// </summary>
public class InteractableDoorHandle : Interactable
{

    //Amount of force to apply when moving the door
    [SerializeField]
    private float doorForceMultiplier = 1.0f;

    public override void DoAction(VRHand hand)
    {

        //Init Variables to 0
        Vector3 force = Vector3.zero;
        Vector3 cross = Vector3.zero;
        float angle = 0f;

        //If Door Grabed
        if (hand.GetActionState(hand.VRInputController.GrabAction))
        {

            //Get the direction from door pivot point
            Vector3 doorPivotToHand = hand.transform.position - transform.parent.position;

            //Ignore the y axis of the direction vector
            doorPivotToHand.y = 0;

            //Direction vector from door hanle to hand's current position
            force = hand.transform.position - transform.position;

            //Get the cross product between the force and direction
            cross = Vector3.Cross(doorPivotToHand, force);
            angle = Vector3.Angle(doorPivotToHand, force);
        }

        //Apply Door Movement
        GetComponentInParent<Rigidbody>().angularVelocity = cross * angle * doorForceMultiplier;
    }
}

//Lewis Hammond