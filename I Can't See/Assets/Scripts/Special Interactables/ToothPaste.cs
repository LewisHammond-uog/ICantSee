using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothPaste : Holdable
{
    [SerializeField]
    private AudioSource interactableAudioSource;

    public bool isDPadPressed = false;

    public override void DoAction(VRHand hand)
    {
        base.DoAction(hand);
        VRInput vr = hand.VRInputController;

        isDPadPressed = hand.GetActionState(vr.SpecialDpadAction);

        while(isDPadPressed)
        {
            //Check if audio source is not null
            if (interactableAudioSource != null)
            {
                //Play Interactable moving sound
                interactableAudioSource.Play();
            }

        }
        interactableAudioSource.Stop();
    }
}


//Rhys Wareham