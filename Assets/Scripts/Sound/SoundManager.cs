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

    [SerializeField] private AudioSource ambienceSource;
    [SerializeField] private AudioSource bossMusicSource;
    [SerializeField] private AudioSource menuMusicSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        if (bossMusicSource != null)
        {
            bossMusicSource.Stop();
            bossMusicSource.volume = 0.0f;
        }
        if (menuMusicSource != null)
        {
            menuMusicSource.Stop();
            menuMusicSource.volume = 0.0f;
        }
    }

    public void playBossMusic()
    {
        StartCoroutine(fadeOutSource(ambienceSource, 5.0f));
        StartCoroutine(fadeOutSource(menuMusicSource, 5.0f));
        StartCoroutine(fadeInSource(bossMusicSource, 5.0f));
    }

    public void playMenuMusic()
    {
        StartCoroutine(fadeOutSource(ambienceSource, 5.0f));
        StartCoroutine(fadeOutSource(bossMusicSource, 5.0f));
        StartCoroutine(fadeInSource(menuMusicSource, 5.0f));
    }

    private IEnumerator fadeInSource(AudioSource source, float fadeTime)
    {
        source.Play();
        float timeElapsed = 0.0f;
        while (timeElapsed < fadeTime)
        {
            float progress = timeElapsed / fadeTime;
            source.volume = progress;
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        source.volume = 1.0f;
    }

    private IEnumerator fadeOutSource(AudioSource source, float fadeTime)
    {
        float timeElapsed = 0.0f;
        while (timeElapsed < fadeTime)
        {
            float progress = 1.0f - timeElapsed / fadeTime;
            source.volume = progress;
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        source.volume = 0.0f;
        source.Stop();
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
