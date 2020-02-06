using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoorHandle : InteractableDoorHandle
{

    //Door joint so that we can stop the door from moving
    [SerializeField]
    private HingeJoint doorJoint;
    private JointLimits unlockedJointLimits;

    [SerializeField]
    private Job.JOB_ROOMS doorRoom;


    private bool doorLocked = false;


    // Start is called before the first frame update
    void Start()
    {
        //Store the current limits for the door
        //so that we can restore to them after unlock
        doorJoint.useLimits = true;
        unlockedJointLimits = doorJoint.limits;

        //Lock the door limits
        JointLimits lockedLimits = new JointLimits();
        lockedLimits.min = 0.0f;
        lockedLimits.max = 0.0f;
        doorJoint.limits = lockedLimits;

        doorLocked = true;
    }

    public override void DoAction(VRHand hand)
    {
        //Only allow garbbing/ moving while unlocked
        if (!doorLocked)
        {
            base.DoAction(hand);
        }
    }

    /// <summary>
    /// Attempt to unlock the door if the next job is in another room
    /// </summary>
    private void AttemptToUnlockDoor()
    {
        if (doorLocked)
        {
            if (JobManager.GetCurrentJob().JobRoom != doorRoom)
            {
                UnlockDoor();
            }
        }
    }

    /// <summary>
    /// Unlock door
    /// </summary>
    private void UnlockDoor()
    {
        doorJoint.useLimits = true;
        doorJoint.limits = unlockedJointLimits;
    }


    #region event Subscribe/Unsubscribe
    private void OnEnable()
    {
        //Attempt to unlock door when a new job is started
        JobManager.JobStarted += AttemptToUnlockDoor;
    }
    private void OnDisable()
    {
        //Attempt to unlock door when job is completed
        JobManager.JobStarted -= AttemptToUnlockDoor;
    }
    #endregion
}

//Lewis Hammond