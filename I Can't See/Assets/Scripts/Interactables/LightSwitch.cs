using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : SoundInteractable
{

    [SerializeField]
    private AudioClip switchSoundClip;

    // Start is called before the first frame update
    void Start()
    {

    }

    public override void DoAction(VRHand hand)
    {
        //Check if D-Pad has been pressed to turn off radio
        if (hand.GetActionState(hand.VRInputController.SpecialDpadAction))
        {
            //Play 1 Shot Audio for switch on/off
            interactableAudioSource.PlayOneShot(switchSoundClip);
            // call job manager
            JobManager.RegisterJobAction(jobInfo);
        }

    }
}


//Lewis Hammond