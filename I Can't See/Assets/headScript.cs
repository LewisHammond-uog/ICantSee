using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headScript : MonoBehaviour
{
    [SerializeField]
    private GameObject toothBrush;
    [SerializeField]
    private AudioSource brushingAudioSource;


    private void OnTriggerStay(Collider other)
    {
        //Check if toothbrush is within the collider
        if (other.gameObject == toothBrush)
        {

            if (toothBrush.GetComponent<Rigidbody>().velocity != new Vector3(0, 0, 0))
            {
                //Play brushing sound
                brushingAudioSource.Play();
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