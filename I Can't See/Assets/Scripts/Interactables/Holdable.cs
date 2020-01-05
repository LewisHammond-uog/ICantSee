using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Holdable : Interactable
{

    private bool isHeld = false;
    private VRHand currentHolder = null;
    public VRHand CurrentHolder
    {
        get { return currentHolder;  }
        set { currentHolder = value; }
    }

    public override void DoAction(VRHand hand)
    {
        //Get the vr controller
        VRInput vr = hand.VRInputController;

        //Check if we should pickup or drop object
        if (hand.GetActionState(vr.GrabAction) && !isHeld && currentHolder == null)
        {
            //Pickup
            hand.AttachObject(this);

        }else if(isHeld && currentHolder == this)
        {
            //Drop
            hand.DetachObject(this);
        }
    }

}
