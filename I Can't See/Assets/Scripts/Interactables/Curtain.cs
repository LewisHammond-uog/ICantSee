using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curtain : Interactable
{
    [SerializeField]
    private AudioSource interactableAudioSource;
    private Vector3 moveAmount;
    [SerializeField]
    private float minMovementLimit = 150;
    private float maxMovementLimit = 250;
    [SerializeField]
    private Vector3 chosenAxis;
    private Vector3 startPos;

    [SerializeField]
    private float posScaleBias = 0.5f; // Higher numbers bias towards the scale, lower numbers bias towards the position

    private void Start()
    {
        //Get start position of the Interactable
        startPos = this.transform.position;
    }

    public override void DoAction(VRHand hand)
    {
        // Check if trigger has been pressed to grab the curtain
        if (hand.GetActionState(hand.VRInputController.GrabAction))
        {
            // Get the currect velocity of the hand and multiply by delta time to get how far the curtain should be moved
            moveAmount = hand.CurrentPose.GetVelocity() * Time.deltaTime;

            // Check if the distance between the curtain's start position and position after moving, is more than the minimum movement limit
            // And check if the distance is less than the maximum movement limit
            if (Vector3.Distance(this.transform.position + Vector3.Scale(moveAmount, chosenAxis), startPos) > minMovementLimit ||
                Vector3.Distance(this.transform.position + Vector3.Scale(moveAmount, chosenAxis), startPos) < maxMovementLimit)
            {
                // Change the scale by the 'moveAmout' multiplied by the 'chosenAxis' to act as a mask so as to only change the desired axis, muliplied by the bias to smooth the change and make it look correct
                this.transform.localScale += Vector3.Scale(moveAmount, chosenAxis) * posScaleBias;
                // Change the position by the 'moveAmout' multiplied by the 'chosenAxis' to act as a mask so as to only change the desired axis, muliplied by the bias to smooth the change and make it look correct
                this.transform.position += Vector3.Scale(moveAmount, chosenAxis) * (1.0f - posScaleBias);
            }

            //Check if audio source is not null
            if (interactableAudioSource != null)
            {
                if (!interactableAudioSource.isPlaying)
                {
                    //Play Interactable moving sound
                    interactableAudioSource.Play();
                }
            }
        }
        // Stop the sound from playing when the curtain isnt moving
        if (interactableAudioSource != null)
        {
            if(interactableAudioSource.isPlaying)
            {
                interactableAudioSource.Stop();
            }
        }
    }
}

// Connor Done
