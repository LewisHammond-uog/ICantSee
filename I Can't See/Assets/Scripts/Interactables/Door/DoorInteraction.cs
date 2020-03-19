using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HingeJoint))]
[RequireComponent(typeof(Rigidbody))]
public class DoorInteraction : Holdable
{
    //Parent Object for rotation
    [SerializeField]
    private Transform parent;

    //Amount to mutiply the force of the rotation
    [SerializeField]
    private float forceMutiplier = 50f;

    //Rigidbody of the door
    private Rigidbody doorRigidBody;

    private void Start()
    {
        //Get the rigidboy of the door - we must have this
        //as we require it to be a component
        doorRigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (IsHeld)
        {
            //Get a vector between the position of our holder and the door
            Vector3 targetDelta = CurrentHolder.transform.position - transform.position;
            //Ignore the y component
            targetDelta.y = 0;

            //Get the difference in angle between the door it's target position
            float angleDiff = Vector3.Angle(transform.forward, targetDelta);

            //Get the cross of our forward and target pos
            Vector3 cross = Vector3.Cross(transform.forward, targetDelta);

            //Set our velocity
            if (doorRigidBody)
            {
                doorRigidBody.angularVelocity = cross * angleDiff * forceMutiplier;
            }
        }
    }
}
