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
            //Play brushing sound
            if (brushingAudioSource)
            {
                brushingAudioSource.Play();
            }

            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                // call job manager
                JobManager.RegisterJobAction(jobInfo);
            }
        }
        else
        {
            //Stop brushing sound
            if (brushingAudioSource)
            {
                if (brushingAudioSource.isPlaying)
                {
                    brushingAudioSource.Stop();
                }
            }
        }
    }
}


//Rhys Wareham