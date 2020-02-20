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
        Vector3 rightVel = rightHand.CurrentPose.GetVelocity();
        Vector3 leftVel = leftHand.CurrentPose.GetVelocity();

        //If move button is clicked...
        if(moveButtonClicked)
        {
            //Check if a controller velocity is more than 0
            if (rightVel.x + rightVel.z > 0 || leftVel.x + leftVel.z > 0)
            {
                //Increase the position by whatever the higher velocity of the controllers is
                vrRig.transform.position += ((leftVel.x + leftVel.z) > (rightVel.x + rightVel.z)) ? leftVel : rightVel;
            }
        }
        

    }
}


//Rhys Wareham