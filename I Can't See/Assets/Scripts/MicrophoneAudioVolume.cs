using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MicrophoneAudioVolume : MonoBehaviour
{
    private string device;

    private AudioClip micRecord;

    private const int sampleWindow = 128;
    private int sampleLength = 1;
    private int sampleRate = 44100;

    private float[] waveData = new float[sampleWindow];

    private void Start()
    {
        // Check if we have a microphone
        if(device == null)
        {
            // if not then get the first active microphone
            device = Microphone.devices[0];
        }
    }

    private void Update()
    {
        // Check if the mic is being recored
        if(!Microphone.IsRecording(device))
        {
            // If not then start recording
            StartRecording();
        }
        else
        {
            // Make sure the microphone has stopped recording
            StopRecording();
            // Send the sound data to the effect generator, with the players current position
            EffectGenerator.CreateEffect(GetCurrentClipLoundness(), this.transform.position);
        }
    }

    /// <summary>
    /// Tell the Mircophone to start recording the mic
    /// </summary>
    private void StartRecording()
    {
        micRecord = Microphone.Start(device, true, sampleLength, sampleRate);
    }

    /// <summary>
    /// Tell the Microphone to stop recording
    /// </summary>
    private void StopRecording()
    {
        Microphone.End(device);
    }
    /// <summary>
    /// Calculate the average loudness of the mic's recording
    /// </summary>
    /// <returns>A float representing the loudness of the mic's recording in a linear format</returns>
    private float GetCurrentClipLoundness()
    {
        // Get the wave data from the recording
        micRecord.GetData(waveData, sampleRate);

        float avgLoundess = 0.0f;
        // Loop through the waveData array
        for (int i = 0; i < waveData.Length; i++)
        {
            // Add the peak volume for each point in the wave data
            avgLoundess += Mathf.Abs(waveData[i]);
        }

        // Divide by the total to get the average
        avgLoundess /= sampleRate;

        return avgLoundess;
    }
}

// Connor Done
