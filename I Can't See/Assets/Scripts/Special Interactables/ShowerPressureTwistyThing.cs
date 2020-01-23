using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowerPressureTwistyThing : Interactable
{
    private Vector3 handVel;
    private float rotationAngle;
    private Vector3 finalAxis;

    [SerializeField]
    private Vector3 rotationAxis;

    //Job Info - For Job Manager to register the associated job actions for the job manager
    [SerializeField]
    private JobActionInfo jobInfo;

    public override void DoAction(VRHand hand)
    {
        //Check if D-Pad has been pressed to turn off radio
        if (hand.GetActionState(hand.VRInputController.SpecialDpadAction))
        {
            handVel = hand.CurrentPose.GetAngularVelocity();
            finalAxis = Vector3.Scale(handVel, rotationAxis);

            rotationAngle = finalAxis.x = finalAxis.y + finalAxis.z;

            this.transform.Rotate(rotationAxis, rotationAngle);

            // call job manager
            JobManager.RegisterJobAction(jobInfo);
        }
    }
}
