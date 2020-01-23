using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateInteractable : Interactable
{
    [SerializeField]
    private AudioSource interactableAudioSource;
    private Vector3 rotateAmount;
    [SerializeField]
    private float rotateLimit;
    [SerializeField]
    private Vector3 chosenAxis;
    private Quaternion startRot;



    // Start is called before the first frame update
    void Start()
    {
        startRot = this.transform.rotation;
    }

    public override void DoAction(VRHand hand)
    {
        //Check if trigger has been pressed to grab sliding Interactable
        if (hand.GetActionState(hand.VRInputController.GrabAction))
        {

            //Get the velocity of the hand
            rotateAmount = hand.CurrentPose.GetVelocity();
            //Check if the rotation of the Interactable is less than the rotation limit
            if (Vector3.Distance(this.transform.position + Vector3.Scale(moveAmount, chosenAxis), startPos) < movementLimit)
            {
                //Rotate the Interactable with the player's controller
                
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


//Rhys Wareham