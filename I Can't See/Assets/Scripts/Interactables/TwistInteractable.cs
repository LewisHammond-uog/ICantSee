using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwistInteractable : Interactable
{
    private float rotationAngle;
    private Vector3 finalAxis;

    [SerializeField]
    private Vector3 rotationAxis;

    //Job Info - For Job Manager to register the associated job actions for the job manager
    [SerializeField]
    private JobActionInfo jobInfo;

    private void Update()
    {
        
    }

    public override void DoAction(VRHand hand)
    {
        
        //Check if D-Pad has been pressed to turn off radio
        if (hand.GetActionState(hand.VRInputController.SpecialDpadAction))
        {
            rotationAngle = -hand.CurrentPose.GetAngularVelocity().x;
            if(rotationAngle > 0)
            {
                int i = 0;
            }

            Vector3 handVel = new Vector3(rotationAngle, rotationAngle, rotationAngle);
            finalAxis = Vector3.Scale(handVel, rotationAxis);

            this.transform.Rotate(rotationAxis, rotationAngle);

            // call job manager
            //JobManager.RegisterJobAction(jobInfo);
        }
    }
}
