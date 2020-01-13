using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curtains : Holdable
{
    [SerializeField]
    private AudioSource curtainAudioSource;
    [SerializeField]
    private AudioClip curtainRailSound;

    //Job Info - For Job Manager to register the associated job actions for the job manager
    [SerializeField]
    private JobActionInfo jobInfo;


    public override void DoAction(VRHand hand)
    {

        //Check if D-Pad has been pressed to turn off alarm
        if (hand.GetActionState(hand.VRInputController.SpecialDpadAction))
        {
            // turn off the alarm
            curtainAudioSource.PlayOneShot(curtainRailSound);

            JobManager.RegisterJobAction(jobInfo);
        }
    }
}
