using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothBrush : Holdable
{
    [SerializeField]
    private GameObject paste;

    public override void DoAction(VRHand hand)
    {
        base.DoAction(hand);

        //VRInput vr = hand.VRInputController;
    }

    private void OnCollisionStay(Collision collision)
    {
        //Check if toothpaste is on toothbrush
        if(collision.gameObject == paste)
        {
            // call job manager
            JobManager.RegisterJobAction(jobInfo);
        }

    }

}


//Rhys Wareham