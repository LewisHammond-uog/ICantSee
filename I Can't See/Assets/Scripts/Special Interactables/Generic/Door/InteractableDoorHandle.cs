using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Door Handle, an object that the player can drag around the world
/// The Door then uses its own handle script to move the door towards this interactable
/// </summary>
public class InteractableDoorHandle : Holdable
{
    Transform startPos;
    Rigidbody rb;

    private void Start()
    {
        startPos = transform;
        rb = GetComponent<Rigidbody>();
    }

    public override void DoAction(VRHand hand)
    {
        //Do Holdable actions to move this object around
        base.DoAction(hand);

        //Check if this object is not held and is not in the right position
        if(!isHeld && transform.position != startPos.position)
        {
            transform.position = startPos.position;
            transform.rotation = startPos.rotation;

            rb.isKinematic = true;
        }
        else
        {
            rb.isKinematic = false;
        }


    }
}
