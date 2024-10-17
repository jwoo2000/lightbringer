using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SliderUpdater : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] private Slider slider;
    [SerializeField] private string paramName;
    void Start()
    {
        float currVol;
        audioMixer.GetFloat(paramName, out currVol);
        slider.value = SoundMixerManager.dBtoLinear(currVol);
    }
}
