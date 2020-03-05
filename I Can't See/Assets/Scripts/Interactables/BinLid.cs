using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinLid : Interactable
{
    [SerializeField]
    private AudioSource interactableAudioSource;
    private Vector3 moveAmount;

    [SerializeField]
    private Quaternion minRotationLimit;
    [SerializeField]
    private Quaternion maxRotationLimit;

    private Vector3 chosenAxis = new Vector3(-1, 0, 1);
    private Quaternion startRot;

    private void Start()
    {
        //Get start rotation of the Interactable
        startRot = this.transform.rotation;
    }

    public override void DoAction(VRHand hand)
    {
        // Check if trigger has been pressed to grab the bin lid
        if (hand.GetActionState(hand.VRInputController.GrabAction))
        {
            // Get the currect velocity of the hand and multiply by delta time to get how far the bin lid should be moved
            moveAmount = hand.CurrentPose.GetVelocity();

            if (Vector3.Scale(moveAmount, chosenAxis).magnitude > 0)
            {
                this.transform.rotation = Quaternion.Lerp(this.transform.rotation, maxRotationLimit, moveAmount.x);
                this.transform.rotation = Quaternion.Lerp(this.transform.rotation, maxRotationLimit, moveAmount.y);
                this.transform.rotation = Quaternion.Lerp(this.transform.rotation, maxRotationLimit, moveAmount.z);
            }
            else if (Vector3.Scale(moveAmount, chosenAxis).magnitude < 0)
            {
                this.transform.rotation = Quaternion.Lerp(this.transform.rotation, minRotationLimit, moveAmount.x);
                this.transform.rotation = Quaternion.Lerp(this.transform.rotation, minRotationLimit, moveAmount.y);
                this.transform.rotation = Quaternion.Lerp(this.transform.rotation, minRotationLimit, moveAmount.z);
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
        // Stop the sound from playing when the bin lid isnt moving
        if (interactableAudioSource != null)
        {
            if (interactableAudioSource.isPlaying)
            {
                interactableAudioSource.Stop();
            }
        }
    }
}
