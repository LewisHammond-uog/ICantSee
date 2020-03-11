using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class generates the game effect based on the sound an object is creating
/// </summary>
public class EffectGenerator : MonoBehaviour
{
    //Audio Source for whatever sound the object is playing
    //(e.g music coming out of the radio)
    private AudioSource objectAudioSource;
    //Flags if this object has an existing audio source (i.e a radio)
    //if it does then we should poll the object for updates on what
    //it is currently playing
    private bool hasExistingAudioSoruce;

    //Audio Source for dropped sound - created by the script
    //so that it can't be messed up
    private AudioSource dropAudioSource;
    [SerializeField]
    private AudioClip dropSound;

    //How often the effect updates (i.e a new effect is created) in seconds
    [SerializeField]
    private float updateStep = 1f;
    private float timeSinceLastUpdate = 0.0f;

    //Number of sample to average out when calculating the strength of the effect,
    //and the arrray that the data will be stored in
    private int sampleDataLen = 1024;
    private float[] clipSampleData;

    //Minimum Loudness to create an effect
    [SerializeField]
    private float minEffectLoundness = 0.2f;

    //Amount to amplify the loudness when creating the effect width
    [SerializeField]
    private float loudnessMultiplyer = 20;

    //Name of the object that we create when creating an effect
    private const string effectObjName = "EffectPoint";

    // Start is called before the first frame update
    void Start()
    {
        //Get the audio source component already on the object,
        //if there isn't one then we can disable
        //regular updates
        objectAudioSource = GetComponent<AudioSource>();
        hasExistingAudioSoruce = objectAudioSource != null ? true : false;

        //Create the drop audio source and assign values - this audio soruce
        //is played only when the object collides with another
        dropAudioSource = gameObject.AddComponent<AudioSource>();
        dropAudioSource.playOnAwake = false;
        dropAudioSource.clip = dropSound;

        //Allocate the memory for the array
        clipSampleData = new float[sampleDataLen];

        if (hasExistingAudioSoruce)
        {
            //Create a clip straigtht away
            CreateEffect(GetCurrentClipLoundness() * loudnessMultiplyer, transform.position);
        }

    }

    // Update is called once per frame
    void Update()
    {
        //Don't poll for updates if we don't have a audio source that is not
        //the drop audio source
        if (hasExistingAudioSoruce)
        {
            timeSinceLastUpdate += Time.deltaTime;
            if (timeSinceLastUpdate >= updateStep)
            {
                //Get the loudness of the current playing audio
                float loudness = GetCurrentClipLoundness();

                if (loudness > minEffectLoundness)
                {
                    CreateEffect((loudness * loudness)/*^2*/ * loudnessMultiplyer, transform.position);
                }

                timeSinceLastUpdate = 0.0f;
            }
        }
    }

    /// <summary>
    /// Get the average loudness of the current audio clip
    /// </summary>
    /// <returns></returns>
    private float GetCurrentClipLoundness()
    {
        //If there is no audio playing then there is no
        //loundness
        if (!objectAudioSource.isPlaying || !objectAudioSource.clip)
        {
            return 0.0f;
        }

        //Get the audio samples from the clip at the current playing position
        objectAudioSource.clip.GetData(clipSampleData, objectAudioSource.timeSamples);

        //Loop and get the average of all samples in the sample array
        float avgLoundess = 0.0f;
        for (int i = 0; i < clipSampleData.Length; i++)
        {
            // Add the peak volume for each point in the wave data
            avgLoundess += Mathf.Abs(clipSampleData[i]);
        }
        avgLoundess /= sampleDataLen;

        //Mutiply the loudness of the clip by the volume of the audio source
        avgLoundess *= objectAudioSource.volume;

        return avgLoundess;
    }

    /// <summary>
    /// Creates an effect with a given width at the current objects position
    /// </summary>
    public static void CreateEffect(float effectWidth, Vector3 position)
    {
        //Create an effect
        GameObject effectObj = new GameObject();
        effectObj.name = effectObjName;
        effectObj.transform.position = position;

        //Add the effect point 
        EffectPoint effect = effectObj.AddComponent<EffectPoint>();
        effect.ScanWidth = effectWidth;
    }

    //Create an instant effect when colliding with something
    private void OnCollisionEnter(Collision collision)
    {
        //Create an effect as soon as we collide with something - don't wait
        CreateEffect(gameObject.GetComponent<Rigidbody>().velocity.magnitude, transform.position);

        //Play audio source sound
        dropAudioSource.PlayOneShot(dropSound);
    }
}

//Lewis Hammond
// Connor Done
