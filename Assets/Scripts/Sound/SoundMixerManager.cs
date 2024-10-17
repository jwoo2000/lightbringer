using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    public void SetMasterVolume(float level)
    {
        audioMixer.SetFloat("masterVolume", linearTodB(level));
    }

    public void SetSFXVolume(float level)
    {
        audioMixer.SetFloat("sfxVolume", linearTodB(level));
    }

    public void SetMusicVolume(float level)
    {
        audioMixer.SetFloat("musicVolume", linearTodB(level));
    }

    public void SetUIVolume (float level)
    {
        audioMixer.SetFloat("uiVolume", linearTodB(level));
    }

    public static float dBtoLinear(float db)
    {
        return Mathf.Pow(10.0f, db / 20.0f);
    }

    // converts 0.0001 to 1 to dB
    public static float linearTodB(float linearValue)
    {
        return Mathf.Log10(Mathf.Clamp(linearValue, 0.0001f, 1.0f)) * 20.0f;
    }
}
