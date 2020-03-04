using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothBrush : Holdable
{

    public override void DoAction(VRHand hand)
    {
        base.DoAction(hand);

        //VRInput vr = hand.VRInputController;
    }

    private void OnCollisionStay(Collision collision)
    {
        //Check if toothpaste is on toothbrush
        if(collision.gameObject.GetComponent<PasteObj>())
        {
            // call job manager
            JobManager.RegisterJobAction(jobInfo);
        }

    }

}


//Rhys Wareham