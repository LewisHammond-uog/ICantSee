using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothBrush : Holdable
{
    [SerializeField]
    private GameObject paste;
    
    //Job Info - For Job Manager to register the associated job actions for the job manager
    [SerializeField]
    protected JobActionInfo jobInfo;


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