using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Require a rigidbody and collder so that thse objects are holdable
[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class Holdable : Interactable
{

    protected bool isHeld = false;
    private VRHand currentHolder = null;

    bool isBtnPressed = false;
    public VRHand CurrentHolder
    {
        get { return currentHolder;  }
        set { currentHolder = value; }
    }

    public override void DoAction(VRHand hand)
    {
        //Get the vr controller
        VRInput vr = hand.VRInputController;

        isBtnPressed = hand.GetActionState(vr.GrabAction);

        //Check if we should pickup or drop object
        if (isBtnPressed && !isHeld && currentHolder == null)
        {
            //Pickup
            hand.AttachObject(this);
            isHeld = true;

        }else if(!isBtnPressed && isHeld)
        {
            //Drop
            hand.DetachObject(this);
            isHeld = false;
        }
    }

}

//Lewis Hammond
// connor done