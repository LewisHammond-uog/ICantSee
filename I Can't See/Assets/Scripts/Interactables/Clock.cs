﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Clock : Holdable
{ 

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

//Rhys Wareham