using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothPaste : Holdable
{
    [SerializeField]
    private AudioSource interactableAudioSource;
    [SerializeField]
    private Transform pasteSpawn;
    [SerializeField]
    private GameObject tpPaste;
    private Vector3 squirtLocation;

    private bool isDPadPressed = false;

    public override void DoAction(VRHand hand)
    {
        base.DoAction(hand);
        VRInput vr = hand.VRInputController;

        isDPadPressed = hand.GetActionState(vr.SpecialDpadAction);

        if(isDPadPressed)
        {
            //Check if audio source is not null
            if (interactableAudioSource != null)
            {
                //Play Interactable moving sound
                interactableAudioSource.Play();
            }
            squirtLocation = new Vector3(pasteSpawn.position.x, pasteSpawn.position.y, pasteSpawn.position.z);
            Instantiate(tpPaste, squirtLocation, Quaternion.identity);

        }
        interactableAudioSource.Stop();
    }
}


//Rhys Wareham