using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideInteractable : Interactable
{
    [Header("Audio Source")]
    [SerializeField]
    private AudioSource interactableAudioSource;
    [Header("Audio Clip")]
    [SerializeField]
    private AudioClip movementAudioClip;//Clip of the audio source moving
    private Vector3 moveAmount;

    [Header("Movement")]
    [SerializeField]
    private float movementLimit;
    [SerializeField]
    private Vector3 chosenAxis;
    private Vector3 startPos;

    private bool interactedLastFrame = false;

    private void Start()
    {
        //Get start position of the Interactable
        startPos = this.transform.position;
    }


    public override void DoAction(VRHand hand)
    {
        //Check if trigger has been pressed to grab sliding Interactable
        if (hand.GetActionState(hand.VRInputController.GrabAction))
        {

            //Get the velocity of the hand
            moveAmount = hand.CurrentPose.GetVelocity() * Time.deltaTime;
            //Check if the distance between the Interactable's startPos and position after moving, is less than the movement limit
            if (Vector3.Distance(this.transform.position + Vector3.Scale(moveAmount, chosenAxis), startPos) < movementLimit) 
            {
                //Move the Interactable with the player's controller
                this.transform.position += Vector3.Scale(moveAmount, chosenAxis);
            }

            //Check if audio source is not null
            if (interactableAudioSource != null)
            {
                if (!interactableAudioSource.isPlaying)
                {
                    interactableAudioSource.clip = movementAudioClip;
                    //Play Interactable moving sound
                    interactableAudioSource.Play();
                }
            }

        }
        else
        {
            //Check if audio source is not null
            if (interactableAudioSource != null)
            {
                if (interactableAudioSource.isPlaying)
                {
                    //Stop Interactable moving sound
                    interactableAudioSource.Stop();
                }
            }
        }

        //Reset Moveamount incase we are not interacting next
        //frame
        moveAmount = Vector3.zero;
    }
}


//Rhys Wareham