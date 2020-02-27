using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRMove : MonoBehaviour
{
    [SerializeField]
    private VRHand rightHand;
    [SerializeField]
    private VRHand leftHand;

    [SerializeField]
    private GameObject vrRig;

    [SerializeField]
    private VRInput vrInputController;

    private bool moveButtonClicked = false;

    private Vector3 rightVel;
    private Vector3 leftVel;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if (rightHand.GetActionState(vrInputController.MoveAction) || leftHand.GetActionState(vrInputController.MoveAction))
        {
            moveButtonClicked = true;
        }
        else
        {
            moveButtonClicked = false;
        }

        //Get the velocity of both controllers
        //FOR FIXES? - GET THE ABS VELOCITY
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
                Vector3 controllerVelocity = ((leftVel.x + leftVel.z) > (rightVel.x + rightVel.z)) ? leftVel : rightVel;
                
                //Apply the velocity in the direction that the controller is facing
                //FOR FIXES - Not Local Rotation?
                Vector3 moveVector = Vector3.Scale(controllerVelocity, rightHand.gameObject.transform.localEulerAngles);
                
                vrRig.transform.position += moveVector * Time.deltaTime;
            }
        }
        

    }
}


//Rhys Wareham
//Lewis Hammond