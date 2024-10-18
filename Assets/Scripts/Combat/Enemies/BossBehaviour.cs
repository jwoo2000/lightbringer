using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : EnemyBehaviour
{
    [SerializeField]
    public GameManager gameManager;

    [SerializeField] public AudioSource playerSoundSource;
    [SerializeField] private AudioClip bossDefeatSound;

    protected override void Awake()
    {
        base.Awake();
        startingHealth = playerStats.level * 400;
    }

    protected override void OnDeath()
    {
        base.OnDeath();
        if (behaviourType == EnemyType.Boss)
        {
            gameManager.playerWin();
            SoundManager.instance.playOneShot(playerSoundSource, bossDefeatSound, 1.0f);
            SoundManager.instance.playMenuMusic();
        }
    }
}
