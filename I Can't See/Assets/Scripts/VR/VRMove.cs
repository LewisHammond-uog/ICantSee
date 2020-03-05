using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class VRMove : MonoBehaviour
{
    //Left/Right Hand VR Controllers
    [SerializeField]
    private VRHand rightHand;
    [SerializeField]
    private VRHand leftHand;

    //VR Rig Object that we need to move
    [SerializeField]
    private GameObject vrRig;

    //Overall VR Input Controller that 
    //is used to store actions/poses
    [SerializeField]
    private VRInput vrInputController;

    //If the move action has been clicked
    private bool moveButtonClicked = false;

    //Velcoities of each of the controllers
    private Vector3 rightVel;
    private Vector3 leftVel;

    //Audio Source and move sound
    private AudioSource walkAudioSource;
    [SerializeField]
    private AudioClip walkSound;

    //Width of walk effect
    private const float walkEffectWidth = 5f;

    private void Start()
    {
        //Get the audio soruce to play walking sound on
        walkAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        //Get if the move action has been pressed on either controler
        if (rightHand.GetActionState(vrInputController.MoveAction) || leftHand.GetActionState(vrInputController.MoveAction))
        {
            moveButtonClicked = true;
        }
        else
        {
            moveButtonClicked = false;
        }

        //Get the velocity of both controllers
        rightVel = rightHand.CurrentPose.GetVelocity();
        leftVel = leftHand.CurrentPose.GetVelocity();

        //Set Y Component to 0 so that we don't move in the Y Direction
        rightVel.y = 0f;
        leftVel.y = 0f;

        //If move button is clicked...
        if(moveButtonClicked)
        {
            //Check if a controller velocity is not 0
            if (rightVel.magnitude != 0 || leftVel.magnitude != 0)
            {
                //Increase the position by whatever the higher velocity of the controllers is
                //FOR FIXES  - NORMLIZE THIS
                Vector3 controllerVelocity = ((Mathf.Abs(leftVel.x) + Mathf.Abs(leftVel.z)) > (Mathf.Abs(rightVel.x) + Mathf.Abs(rightVel.z))) ? leftVel : rightVel;
                
                // apply negative velocity to move in opposite direction to hand
                vrRig.transform.position += -controllerVelocity * Time.deltaTime;

                //Play Walk Sound
                if (walkAudioSource != null)
                {
                    walkAudioSource.PlayOneShot(walkSound);
                }

                //Create world effect at the current position
                EffectGenerator.CreateEffect(walkEffectWidth, vrRig.transform.position);
                
            }
        }
        

    }
}


//Rhys Wareham
//Lewis Hammond
// connnor done