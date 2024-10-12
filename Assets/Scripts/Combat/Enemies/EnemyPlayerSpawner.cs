using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlayerSpawner : MonoBehaviour
{
    [SerializeField] private bool isSpawning;

    [SerializeField] private GameObject enemySmall;
    [SerializeField] private GameObject enemyMedium;
    [SerializeField] private GameObject enemyLarge;

    [SerializeField] private WeaponController weaponController;
    [SerializeField] private PlayerStats playerStats;

    [SerializeField] private float baseSpawnDistance; // closest distance enemies are spawned (harder enemies spawn further out)

    [SerializeField] private float baseSpawnWaveCD; // slowest wave CD (low levels ~lv0)
    [SerializeField] private float minSpawnWaveCD; // fastest wave CD (high levels ~lv20)
    [SerializeField] private float spawnWaveCD; // seconds between waves
    [SerializeField] private float spawnRateScaling; // per level, decrease the CD by this % of the difference between the current CD and the minimum CD

    [SerializeField]
    private List<EnemyBehaviour.EnemyType> spawnWave = new List<EnemyBehaviour.EnemyType>();

    private int playerLevel;

    private void Awake()
    {
        isSpawning = true;
        baseSpawnDistance = 15.0f;
        baseSpawnWaveCD = 30.0f;
        minSpawnWaveCD = 5.0f;
        spawnRateScaling = 0.1f;
        spawnWaveCD = baseSpawnWaveCD;
    }

    private void Start()
    {
        StartCoroutine(Spawner());
    }

    private IEnumerator Spawner()
    {
        while (isSpawning)
        {
            Debug.Log("Spawning wave in: " + spawnWaveCD + " seconds.");
            yield return new WaitForSeconds(spawnWaveCD);
            SpawnWave();
            Debug.Log("Wave: "+spawnWave);

            playerLevel = playerStats.level;
            spawnWave.Clear();

            // player has any wep
            if (weaponController.LowWeapon != null || weaponController.MidWeapon != null || weaponController.HighWeapon != null)
            {
                for (int i = 0; i < playerLevel; i++)
                {
                    // add small enemies
                    spawnWave.Add(EnemyBehaviour.EnemyType.Small);
                }
            }
            // player has mid or high tier wep
            if (weaponController.MidWeapon != null || weaponController.HighWeapon != null)
            {
                for (int i = 0; i < (playerLevel/2); i++)
                {
                    // add medium enemies
                    spawnWave.Add(EnemyBehaviour.EnemyType.Medium);
                }
            }
            // player has high tier wep
            if (weaponController.HighWeapon != null)
            {
                for (int i = 0; i < (playerLevel/5); i++)
                {
                    // add large enemies
                    spawnWave.Add(EnemyBehaviour.EnemyType.Large);
                }
            }

            // update spawn cd for next wave
            spawnWaveCD = getSpawnCD(playerStats.level);
        }
    }

    private float getSpawnCD(int playerLevel)
    {
        return (minSpawnWaveCD + ((baseSpawnWaveCD - minSpawnWaveCD) * Mathf.Exp(-spawnRateScaling * (float) playerLevel)));
    }

    private void SpawnWave()
    {
        float spawnDist;
        foreach (EnemyBehaviour.EnemyType mobType in spawnWave)
        {
            spawnDist = baseSpawnDistance;
            switch (mobType)
            {
                case EnemyBehaviour.EnemyType.Medium:
                    spawnDist += 1.0f;
                    break;
                case EnemyBehaviour.EnemyType.Large:
                    spawnDist += 2.0f;
                    break;
            }
            Instantiate(getMobFromType(mobType), SpawnPosition(spawnDist), Quaternion.identity);
        }
    }

    private Vector3 SpawnPosition(float distance)
    {
        float rad = Random.Range(0, 360) * Mathf.Deg2Rad;

        return (transform.position + new Vector3(Mathf.Sin(rad), 0, Mathf.Cos(rad))*distance);
    }

    private GameObject getMobFromType(EnemyBehaviour.EnemyType type)
    {
        switch (type)
        {
            case EnemyBehaviour.EnemyType.Small:
                return enemySmall;
            case EnemyBehaviour.EnemyType.Medium:
                return enemyMedium;
            case EnemyBehaviour.EnemyType.Large:
                return enemyLarge;
            default:
                return null;
        }
    }
}
