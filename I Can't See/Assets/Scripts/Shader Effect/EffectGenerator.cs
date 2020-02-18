using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class generates the game effect based on the sound an object is creating
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class EffectGenerator : MonoBehaviour
{
    private AudioSource objectAudioSource;

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

    // Start is called before the first frame update
    void Start()
    {
        //Get the audio source component
        objectAudioSource = GetComponent<AudioSource>();

        //Allocate the memory for the array
        clipSampleData = new float[sampleDataLen];

        //Create a clip straigtht away
        CreateEffect(GetCurrentClipLoundness());

    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastUpdate += Time.deltaTime;
        if (timeSinceLastUpdate >= updateStep)
        {
            //Get the loudness of the current playing audio
            float loudness = GetCurrentClipLoundness();

            if (loudness > minEffectLoundness)
            {
                CreateEffect((loudness * loudness) * loudnessMultiplyer);
            }

            timeSinceLastUpdate = 0.0f;
        }
    }

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
        foreach(float sample in clipSampleData)
        {
            avgLoundess += Mathf.Abs(sample);
        }
        avgLoundess /= sampleDataLen;

        //Mutiply the loudness of the clip by the volume of the audio source
        avgLoundess *= objectAudioSource.volume;

        return avgLoundess;
    }

    /// <summary>
    /// Creates an effect with a given width at the current objects position
    /// </summary>
    private void CreateEffect(float effectWidth)
    {
        //Create an effect
        GameObject effectObj = new GameObject();
        effectObj.transform.position = transform.position;

        EffectPoint effect = effectObj.AddComponent<EffectPoint>();
        effect.ScanWidth = effectWidth * loudnessMultiplyer;
    }
}
