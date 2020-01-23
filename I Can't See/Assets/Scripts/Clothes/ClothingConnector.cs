using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ClothingConnector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //Check the we are connecting a clothing item
        if (other.GetComponent<ClothingItem>())
        {


            //Check that this is the next job that we need to do
            //Otherwise don't connect the clothing item
            ClothingItem clothing = other.GetComponent<ClothingItem>();
            if (clothing.CurrentHolder != null)
            {
                if (JobManager.GetCurrentJob().JobInfo == clothing.jobInfo)
                {
                    //Register that we have connected some clothing
                    JobManager.RegisterJobAction(clothing.jobInfo);
                    Destroy(clothing.gameObject);
                }
            }
        }
    }
}
