using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] private MovementController playerMovement;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private AudioSource playerAudioSource;

    [SerializeField] private List<AudioClip> footstepSounds = new List<AudioClip>();
    [SerializeField] private float baseFootstepDelay = 0.4f;
    [SerializeField] private float baseSpeed;
    [SerializeField] private float sprintDelayMulti = 0.2f;
    private float footstepTimer;

    [SerializeField] private List<AudioClip> absorbSounds = new List<AudioClip>();

    [SerializeField] private AudioClip levelUpSound;
    [SerializeField] private AudioClip takeDamageSound;
    [SerializeField] private AudioClip deathSound;

    private void Awake()
    {
        baseSpeed = playerStats.movespeed;
        footstepTimer = baseFootstepDelay;
    }

    private void Update()
    {
        float speedRatio = playerMovement.currSpeed / baseSpeed;
        float adjustedSpeedEffect = Mathf.Pow(speedRatio, sprintDelayMulti);
        float currFootstepDelay = baseFootstepDelay / adjustedSpeedEffect;
        if (playerMovement.isMoving)
        {

            footstepTimer -= Time.deltaTime;

            if (footstepTimer <= 0.0f)
            {
                playFootstep();
                footstepTimer = Mathf.Clamp(currFootstepDelay, 0.1f, 1.0f);
            }
        } else
        {
            footstepTimer = Mathf.Clamp(currFootstepDelay, 0.1f, 1.0f);
        }
    }

    public void playFootstep()
    {
        if (footstepSounds.Count == 0)
        {
            Debug.LogWarning("no footstep sounds");
            return;
        }
        SoundManager.instance.playOneShot(playerAudioSource, footstepSounds[Random.Range(0, footstepSounds.Count)], 0.2f);
    }

    public void playAbsorb()
    {
        if (absorbSounds.Count == 0)
        {
            Debug.LogWarning("no absorb sounds");
            return;
        }
        SoundManager.instance.playOneShot(playerAudioSource, absorbSounds[Random.Range(0, absorbSounds.Count)], 0.1f);
    }

    public void playLevelUp()
    {
        SoundManager.instance.playOneShot(playerAudioSource, levelUpSound, 0.2f);
    }

    public void takeDamage()
    {
        SoundManager.instance.playOneShot(playerAudioSource, takeDamageSound, 0.3f);
    }

    public void playDeath()
    {
        SoundManager.instance.playOneShot(playerAudioSource, deathSound, 1.0f);
    }
}
