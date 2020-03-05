using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwistInteractable : SoundInteractable
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

    //Property for getting the difference in rotation between the start
    //and current rotation
    public Vector3 rotatedAmount { get { return (transform.rotation.eulerAngles - startRotation.eulerAngles); } }

    //Event for when the state of the twistable is changed
    public delegate void TwistInteractableEvent();
    public static event TwistInteractableEvent TwistMoved;

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
            //Play twist sound
            interactableAudioSource.Play();

            //If rotation was > 0 then call the event that 
            //the object was twisted
            if (Mathf.Abs(rotateAmount) > 0)
            {
                TwistMoved();
            }

            // call job manager
            JobManager.RegisterJobAction(jobInfo);
        }
    }
}

//Lewis Hammond