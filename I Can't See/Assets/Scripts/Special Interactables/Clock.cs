using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : Holdable
{
    [SerializeField]
    private AudioSource alarmAudioSource;

    [SerializeField]
    private AudioClip alarmAudioEndClip;

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
        }
    }
}
