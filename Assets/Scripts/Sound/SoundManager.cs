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
    [SerializeField] private float fadeToBossMusicTime = 5.0f;
    [SerializeField] private float fadeToMenuMusicTime = 5.0f;

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
        StartCoroutine(fadeToBossMusic());
    }

    private IEnumerator fadeToBossMusic()
    {
        bossMusicSource.Play();
        float timeElapsed = 0.0f;
        while (timeElapsed < fadeToBossMusicTime)
        {
            float progress = timeElapsed / fadeToBossMusicTime;
            ambienceSource.volume = 1.0f-progress;
            bossMusicSource.volume = progress;
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        ambienceSource.volume = 0.0f;
        bossMusicSource.volume = 1.0f;
        ambienceSource.Stop();
    }

    public void playMenuMusic()
    {
        StartCoroutine(fadeToMenuMusic());
    }

    private IEnumerator fadeToMenuMusic()
    {
        menuMusicSource.Play();
        float timeElapsed = 0.0f;
        while (timeElapsed < fadeToMenuMusicTime)
        {
            float progress = timeElapsed / fadeToMenuMusicTime;
            bossMusicSource.volume = 1.0f - progress;
            menuMusicSource.volume = progress;
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        bossMusicSource.volume = 0.0f;
        menuMusicSource.volume = 1.0f;
        bossMusicSource.Stop();
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
