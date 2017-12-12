using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicrophoneStuff : MonoBehaviour
{
    ////////// PRIVATE //////////
    private int iSample_size = 256;
    private string sMicro;
    private AudioClip acAudio;

    ////////// PULIC //////////
    public float fLevel;

    // Use this for initialization
    void Start()
    {

        acAudio = new AudioClip();

        if (sMicro == null)
            sMicro = Microphone.devices[0];

        acAudio = Microphone.Start(sMicro, true, 1, 44100);
    }

    // Update is called once per frame
    void Update()
    {

        float[] fSpectrum = new float[iSample_size];

        fLevel = 0;

        int iMic_pos = Microphone.GetPosition(null) - (iSample_size + 1);

        if (iMic_pos < 0)
            return;

        acAudio.GetData(fSpectrum, iMic_pos);

        for (int i = 0; i < fSpectrum.Length; i++)
        {
            float fPeak = fSpectrum[i] * fSpectrum[i];

            if (fLevel < fPeak)
                fLevel = fPeak;
        }

        if (fLevel > 0.1f)
            Debug.Log("Super Noé, t'es content, tu viens de parler et tu t'entends pas");
    }

    private static MicrophoneStuff instance;
    public static MicrophoneStuff Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.Find("[CameraRig]").GetComponent<MicrophoneStuff>();
            }

            return instance;
        }
    }
}
