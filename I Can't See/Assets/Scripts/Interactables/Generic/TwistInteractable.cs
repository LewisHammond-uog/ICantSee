using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwistInteractable : Interactable
{
    //Starting Rotation of this twistable
    //so that we can get the amount that this object has been rotated
    private Quaternion startRotation;

    //Axis to get the controller rotation from
    [SerializeField]
    private Vector3 controllerRotationAxis = new Vector3(1,0,0);
    //Axis to rotate the object in
    [SerializeField]
    private Vector3 objectRotationAxis;

    //Job Info - For Job Manager to register the associated job actions for the job manager
    [SerializeField]
    private JobActionInfo jobInfo;

    private void Update()
    {
        //Initalise the start rotation
        startRotation = transform.rotation;
    }

    public override void DoAction(VRHand hand)
    {
        
        //Check if D-Pad has been pressed to turn use
        if (hand.GetActionState(hand.VRInputController.SpecialDpadAction))
        {
            //Get the rotation of the contoller in the axis we want
            Vector3 controllerRotation = Vector3.Scale(hand.CurrentPose.GetAngularVelocity(), controllerRotationAxis);
            //Get the max value (i.e the only non 0 value) as the amount to rotate
            float rotateAmount = Mathf.Max(controllerRotation.x, controllerRotation.y, controllerRotation.z);

            //Apply that rotation in the direction that we want to rotate
            gameObject.transform.Rotate(objectRotationAxis, rotateAmount);

            // call job manager
            JobManager.RegisterJobAction(jobInfo);
        }
    }
}

//Lewis Hammond