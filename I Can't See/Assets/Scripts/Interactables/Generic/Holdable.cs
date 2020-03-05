using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Require a rigidbody and collder so that thse objects are holdable
[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class Holdable : Interactable
{

    //Property for if this object has
    //a holder and thus is being held
    protected bool IsHeld
    {
        get
        {
            if(currentHolder != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
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
        if (isBtnPressed && !IsHeld && currentHolder == null)
        {
            //Pickup
            hand.AttachObject(this);

            //Register Job Info for Pickup
            JobActionInfo pickupJobInfo = jobInfo;
            pickupJobInfo.action = Job.JOB_ACTIONS.PICKUP;
            JobManager.RegisterJobAction(pickupJobInfo);

        }else if(!isBtnPressed && IsHeld)
        {
            //Drop
            hand.DetachObject(this);
        }
    }

}

//Lewis Hammond
// connor done