using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holdable : Interactable
{

    private bool isHeld = false;
    private VRHand currentHolder = null;

    public override void DoAction(VRHand hand)
    {
        //Get the vr controller
        VRInput vr = hand.VRInputController;

        //Check if we should pickup or drop object
        if (vr.GetActionState(vr.GrabAction) && !isHeld && currentHolder == null)
        {
           //Pickup

        }else if(isHeld && currentHolder == this)
        {
            //Drop

        }
    }
}
