using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadScript : MonoBehaviour
{

    [SerializeField]
    private AudioSource brushingAudioSource;

    [SerializeField]
    protected JobActionInfo jobInfo;

    private float timeLeft = 5.0f;


    private void OnTriggerStay(Collider other)
    {
        //Check if toothbrush is within the collider
        ToothBrush brush = other.gameObject.GetComponent<ToothBrush>();
        if (brush)
        {
            //Check if brush is being moved within the head's collider
            if (brush.GetComponent<Rigidbody>().velocity != new Vector3(0, 0, 0))
            {
                //Play brushing sound
                brushingAudioSource.Play();
                timeLeft -= Time.deltaTime;
                if(timeLeft < 0)
                {
                    // call job manager
                    JobManager.RegisterJobAction(jobInfo);
                }
            }
            else
            {
                //Stop brushing sound
                brushingAudioSource.Stop();
            }
        }

    }
}


//Rhys Wareham