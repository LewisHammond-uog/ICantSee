using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curtain : Interactable
{
    [SerializeField]
    private AudioSource interactableAudioSource;
    private Vector3 moveAmount;
    [SerializeField]
    private float movementLimit;
    [SerializeField]
    private Vector3 chosenAxis;
    private Vector3 startPos;

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
                this.transform.localScale += Vector3.Scale(moveAmount, chosenAxis) * 0.5f;
                this.transform.position += Vector3.Scale(moveAmount, chosenAxis) * 0.5f;
            }

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

// connor done
