using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curtains : Holdable
{
    [SerializeField]
    private AudioSource curtainAudioSource;
    [SerializeField]
    private AudioClip curtainRailSound;

    public override void DoAction(VRHand hand)
    {
        //Does pickup check for holdable
        base.DoAction(hand);

        //Check if D-Pad has been pressed to turn off alarm
        if (hand.VRInputController.GetActionState(hand.VRInputController.SpecialDpadAction))
        {
            // turn off the alarm
            curtainAudioSource.PlayOneShot(curtainRailSound);
            // call job manager
        }
    }
}
