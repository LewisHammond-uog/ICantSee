using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MicrophoneAudioVolume : MonoBehaviour
{
    private float micLoudness; //

    //Device that we are uisng
    private string micDevice;

    //Audio clip that stores the mic data
    AudioClip clip;
    private const int clipLen = 20;
    private const int sampleDataLen = 1024; //Sample Rate in Hz (44100Hz)

    //Array to store mic data
    private float[] micSampleData;

    //How often the effect updates (i.e a new effect is created) in seconds
    [SerializeField]
    private float updateStep = 1f;
    private float timeSinceLastUpdate = 0.0f;

    //Min Value of mic volume before creating
    [SerializeField]
    private float minMicVolume = 0.3f;

    //Amount to amplify the loudness when creating the effect width
    [SerializeField]
    private float loudnessMultiplyer = 20;

    private void Start()
    {
        //Intalise mic sample data array
        micSampleData = new float[sampleDataLen * (int)updateStep];
    }

    private void Update()
    {
        //Increase time since last update
        timeSinceLastUpdate += Time.deltaTime;

        //Check if we should get a new reading from the mic
        if (timeSinceLastUpdate >= updateStep)
        {
            float micLevel = GetMaxMicLevel();

            if (micLevel > minMicVolume)
            {
                //Calculate the width of the effect
                float effectWidth = GetMaxMicLevel() * loudnessMultiplyer;

                //Create an effect
                EffectGenerator.CreateEffect(effectWidth, transform.position);

                //Reset Timer
                timeSinceLastUpdate = 0.0f;
            }
        }
    }

    /// <summary>
    /// Get the maximum volume of the current mic volume
    /// </summary>
    /// <returns></returns>
    private float GetMaxMicLevel()
    {
        int micPos = Microphone.GetPosition(null) - (sampleDataLen + 1);
        if (micPos < 0) return 0;

        clip.GetData(micSampleData, micPos);

        //Loop and get the max of all samples in the sample array
        float maxLoundness = 0.0f;
        for (int i = 0; i < micSampleData.Length; i++)
        {
            //if the current sample loudness is greater than the
            //then est it to the current sample loudness
            float currentSampleLoudness = Mathf.Abs(micSampleData[i]);
            if (maxLoundness < currentSampleLoudness)
            {
                maxLoundness = currentSampleLoudness;
            }
        }
        return maxLoundness;
    }

    /// <summary>
    /// Initalise the microphone
    /// </summary>
    private void InitaliseMic()
    {
        if(micDevice == null)
        {
            micDevice = Microphone.devices[0];
            clip = Microphone.Start(micDevice, true, clipLen, sampleDataLen);
        }
    }

    /// <summary>
    /// Stop the Mic Clip
    /// </summary>
    private void StopMic()
    {
        if (micDevice != null)
        {
            Microphone.End(micDevice);
            micDevice = null;
        }
    }

    #region Enable/Disable Microphone OnEnable/OnDisable
    private void OnEnable()
    {
        InitaliseMic();
    }
    private void OnDisable()
    {
        StopMic();
    }

    //Start/Stop the mic from playing on application focus
    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            //if focused then initalise mic
            if(micDevice == null)
            {
                InitaliseMic();
            }
        }
        else
        {
            if(micDevice != null)
            {
                StopMic();
            }
        }
    }

    #endregion

}

// Lewis Hammond
// Connor Done