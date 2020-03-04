using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MicrophoneAudioVolume : MonoBehaviour
{
    private static float micVol { get; }

    private string device;

    private AudioClip micRecord;

    private bool isInit = false;

    private const int sampleWindow = 128;
    private float[] waveData = new float[sampleWindow];
    private int sampleLength = 999;
    private int sampleRate = 44100;

    private void Start()
    {
        if(device == null)
        {
            device = Microphone.devices[0];
        }
    }

    private void StartRecording()
    {
        micRecord = Microphone.Start(device, true, sampleLength, sampleRate);
    }

    private void StopRecording()
    {
        Microphone.End(device);
    }

    private float GetCurrentClipLoundness()
    {
        micRecord.GetData(waveData, Microphone.GetPosition(device));

        float avgLoundess = 0.0f;
        foreach (float sample in waveData)
        {
            avgLoundess += Mathf.Abs(sample);
        }
        avgLoundess /= sampleWindow;

        return avgLoundess;
    }
}

// connor done
