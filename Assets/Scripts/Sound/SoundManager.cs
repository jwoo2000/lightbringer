using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public enum AudioType
    {
        SFX,
        UI
    }
    public static SoundManager instance;

    [SerializeField] private AudioSource SFXObject;
    [SerializeField] private AudioSource UIObject;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void playSound(AudioType type, AudioClip audioClip, Vector3 location, float volume)
    {
        AudioSource sourceObject;
        switch (type)
        {
            case AudioType.SFX:
                sourceObject = SFXObject;
                break;
            case AudioType.UI:
                sourceObject = UIObject;
                break;
            default:
                sourceObject = SFXObject;
                break;
        }
        AudioSource audioSource = Instantiate(sourceObject, location, Quaternion.identity);
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.Play();
        Destroy(audioSource.gameObject, audioSource.clip.length);
    }

    public void playOneShot(AudioSource audioSource, AudioClip audioClip, float volume)
    {
        audioSource.PlayOneShot(audioClip, volume);
    }
}
