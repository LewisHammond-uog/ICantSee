using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(EffectGenerator))]
public class SoundInteractable : Interactable
{
    //Audio
    [SerializeField]
    protected AudioSource interactableAudioSource;
    [SerializeField]
    private AudioClip interactableAudioEndClip;

    [SerializeField]
    private bool playOnStart = false;

    //Job Info - For Job Manager to register the associated job actions for the job manager
    [SerializeField]
    protected JobActionInfo jobInfo;

    // Start is called before the first frame update
    void Start()
    {
        if (interactableAudioSource != null && playOnStart)
        {
            interactableAudioSource.Play();
        }
    }

    public override void DoAction(VRHand hand)
    {
        //Check if D-Pad has been pressed to turn off radio
        if (hand.GetActionState(hand.VRInputController.SpecialDpadAction))
        {

            if (interactableAudioSource.isPlaying == true)
            {
                // turn off the radio
                interactableAudioSource.Stop();

                // play radio cancel sound
                interactableAudioSource.PlayOneShot(interactableAudioEndClip);
            }
            else
            {
                // Turn on the radio
                interactableAudioSource.Play();
            }

            // call job manager
            JobManager.RegisterJobAction(jobInfo);
        }
    }
}


// Connor Done