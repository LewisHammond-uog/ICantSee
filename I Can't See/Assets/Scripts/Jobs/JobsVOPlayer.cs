using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Singleton
/// Plays the voice over lines for the jobs system
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class JobsVOPlayer : MonoBehaviour
{
    private static JobsVOPlayer instance;

    //Queue of voice lines to play
    private static Queue<AudioClip> voClipQueue;

    //Audio soruce to play VO from
    private AudioSource voAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        //If we already have an instance of the 
        //Player then destory this one
        if(instance != null)
        {
            Destroy(this);
            return;
        }

        //Set the instance of the VO Player
        instance = this;

        //Get the audio source of this object
        voAudioSource = GetComponent<AudioSource>();
        voAudioSource.playOnAwake = false; //Stop from playing on start
    }

    private void Update()
    {
        //Check if there is audio to play in the queue
        if(voClipQueue.Count > 0)
        {
            //Check that there is not audio already playing
            if (voAudioSource.isPlaying)
            {
                //Play the next audio line in the queue,
                //poping it off the queue so that it is no repeated
                voAudioSource.PlayOneShot(voClipQueue.Dequeue());
            }
        }
    }

    /// <summary>
    /// Add a voice over line to the play queue
    /// </summary>
    /// <param name="voClip"></param>
    public static void AddVOToPlayQueue(AudioClip voClip)
    {
        //Check that the jobs VO player exists
        if(instance == null){ return; }

        //Add the VO Line to the queue
        voClipQueue.Enqueue(voClip);
    }

    
}

//Lewis Hammond