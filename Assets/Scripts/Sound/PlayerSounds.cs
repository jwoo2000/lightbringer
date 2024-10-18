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

    private void Awake()
    {
        baseSpeed = playerStats.movespeed;
        footstepTimer = baseFootstepDelay;
    }

    private void Update()
    {
        if (playerMovement.isMoving)
        {
            float speedRatio = playerMovement.currSpeed / baseSpeed;
            float adjustedSpeedEffect = Mathf.Pow(speedRatio, sprintDelayMulti);
            float currFootstepDelay = baseFootstepDelay / adjustedSpeedEffect;

            footstepTimer -= Time.deltaTime;

            if (footstepTimer <= 0.0f)
            {
                playFootstep();
                footstepTimer = Mathf.Clamp(currFootstepDelay, 0.1f, 1.0f);
            }
        }
    }

    public void playFootstep()
    {
        if (footstepSounds.Count == 0)
        {
            Debug.LogWarning("no footstep sounds");
            return;
        }
        SoundManager.instance.playOneShot(playerAudioSource, footstepSounds[Random.Range(0, footstepSounds.Count)], 0.5f);
    }
}
