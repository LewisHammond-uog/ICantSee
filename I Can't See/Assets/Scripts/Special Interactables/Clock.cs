using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : Holdable
{
    //Audio
    [SerializeField]
    private AudioSource alarmAudioSource;
    [SerializeField]
    private AudioClip alarmAudioEndClip;

    //Job Info - For Job Manager to register the associated job actions for the job manager
    [SerializeField]
    private JobActionInfo jobInfo;

    private void Start()
    {
        alarmAudioSource.Play();
    }

    public override void DoAction(VRHand hand)
    {
        //Does pickup check for holdable
        base.DoAction(hand);

        //Check if D-Pad has been pressed to turn off alarm
        if(hand.GetActionState(hand.VRInputController.SpecialDpadAction))
        {
            // turn off the alarm
            alarmAudioSource.Stop();

            // play alarm cancel sound
            alarmAudioSource.PlayOneShot(alarmAudioEndClip);

            // call job manager
            //JobManager.RegisterJobAction(objectJobAction, objectJobRoom, objectJobType);
        }
    }
}
