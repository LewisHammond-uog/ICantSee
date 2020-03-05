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

    //Rotation for registering job
    [SerializeField]
    private float jobRequiredRotation;

    // Start is called before the first frame update
    void Start()
    {
        startRot = this.transform.rotation;
    }

    public override void DoAction(VRHand hand)
    {
        //Check if trigger has been pressed to grab rotate Interactable
        if (hand.GetActionState(hand.VRInputController.GrabAction))
        {
            //Get the velocity of the hand
            Quaternion handRot = hand.CurrentPose.poseAction.localRotation;
            
            //Check if the rotation of the Interactable is less than the rotation limit
            if (handRot.z - startRot.z < rotateLimit)
            {
                //Rotate the Interactable with the player's controller
                this.transform.rotation = new Quaternion(this.transform.rotation.x, this.transform.rotation.y, handRot.z, 0);

                if((handRot.z - startRot.z)> jobRequiredRotation)
                {
                    JobManager.RegisterJobAction(jobInfo);
                }

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