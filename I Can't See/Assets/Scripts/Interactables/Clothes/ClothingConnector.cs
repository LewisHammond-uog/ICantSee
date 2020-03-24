using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ClothingConnector : MonoBehaviour
{
    [SerializeField]
    protected JobActionInfo jobInfo;

    //Allow clothes to be put on when they are not the next job
    [SerializeField]
    private bool bypassJobOrder = false;

    private void OnTriggerStay(Collider other)
    {
        //Check the we are connecting a clothing item
        if (other.gameObject.GetComponent<ClothingItem>())
        {
            //Check that this is the next job that we need to do
            //Otherwise don't connect the clothing item
            ClothingItem clothing = other.gameObject.GetComponent<ClothingItem>();
            if (clothing.CurrentHolder != null)
            {
                //Check if puting thos clothing on completes the curreny job
                //If it does then delete it
                if (JobManager.RegisterJobAction(clothing.jobInfo) || bypassJobOrder)
                {
                    //Register that we have connected some clothing
                    Destroy(clothing.gameObject);
                }
            }
        }
    }
}

//Lewis Hammond