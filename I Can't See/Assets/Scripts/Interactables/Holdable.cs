using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holdable : Interactable
{

    private bool isHeld = false;
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