using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : Holdable
{
    //Job Info - For Job Manager to register the associated job actions for the job manager
    [SerializeField]
    public JobActionInfo jobInfo;

    [SerializeField]
    private AudioSource alarmAudioSource;
    public override void DoAction(VRHand hand)
    {
        //Does pickup check for holdable
        base.DoAction(hand);

        //Check if D-Pad has been pressed to turn off alarm
        if (hand.GetActionState(hand.VRInputController.SpecialDpadAction))
        {
            // turn off the alarm
            alarmAudioSource.Stop();
            JobManager.RegisterJobAction(jobInfo);
        }
    }
}