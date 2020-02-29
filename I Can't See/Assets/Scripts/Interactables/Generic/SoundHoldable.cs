using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundHoldable : Holdable
{
    //Audio
    [SerializeField]
    private AudioSource holdableAudioSource;
    [SerializeField]
    private AudioClip holdableAudioEndClip;

    [SerializeField]
    private bool playOnStart = false;

    private void Start()
    {
        if (holdableAudioSource != null && playOnStart)
        {
            holdableAudioSource.Play();
        }
    }

    public override void DoAction(VRHand hand)
    {
        //Does pickup check for holdable
        base.DoAction(hand);

        //Check if D-Pad has been pressed to turn off radio
        if (hand.GetActionState(hand.VRInputController.SpecialDpadAction))
        {

            if (holdableAudioSource.isPlaying == true)
            {
                // turn off the radio
                holdableAudioSource.Stop();

                // play radio cancel sound
                holdableAudioSource.PlayOneShot(holdableAudioEndClip);
            }
            else
            {
                // Turn on the radio
                holdableAudioSource.Play();
            }

            // call job manager
            JobManager.RegisterJobAction(jobInfo);
        }
    }
}

// Connor Done