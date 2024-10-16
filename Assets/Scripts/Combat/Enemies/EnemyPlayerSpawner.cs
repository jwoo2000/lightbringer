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
    [SerializeField] private float spawnDistVari;

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
        baseSpawnDistance = 8.0f;
        spawnDistVari = 0.1f;
        baseSpawnWaveCD = 40.0f; // default: 40s
        minSpawnWaveCD = 15.0f; // default: 15s
        spawnRateScaling = 0.1f; // default: 0.1
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
            Debug.Log("Spawned wave: "+ spawnWave.Count +" enemies.");

            playerLevel = playerStats.level;
            spawnWave.Clear();

            // player has any wep
            int spawnCount;
            if (weaponController.LowWeapon != null || weaponController.MidWeapon != null || weaponController.HighWeapon != null)
            {
                spawnCount = (int) (-(1f / 15f) * ((playerLevel - 15f) * (playerLevel - 15f)) + 15f);
                for (int i = 0; i < spawnCount; i++)
                {
                    // add small enemies
                    spawnWave.Add(EnemyBehaviour.EnemyType.Small);
                }
            }
            // player has mid or high tier wep
            if (weaponController.MidWeapon != null || weaponController.HighWeapon != null || playerLevel > 20)
            {
                spawnCount = (int) ((40f * playerLevel) / (60f + playerLevel));
                for (int i = 0; i < spawnCount; i++)
                {
                    // add medium enemies
                    spawnWave.Add(EnemyBehaviour.EnemyType.Medium);
                }
            }
            // player has high tier wep
            if (weaponController.HighWeapon != null || playerLevel > 30)
            {
                spawnCount = playerLevel / 10;
                for (int i = 0; i < spawnCount; i++)
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
        // update spawn radius based on curr light radius
        float scaledSpawnDist = baseSpawnDistance * playerStats.lightRadius;
        //Debug.Log("scaled spawn dist:" + scaledSpawnDist);
        float spawnDist;
        foreach (EnemyBehaviour.EnemyType mobType in spawnWave)
        {
            spawnDist = scaledSpawnDist;
            switch (mobType)
            {
                case EnemyBehaviour.EnemyType.Medium:
                    spawnDist += 1.0f;
                    break;
                case EnemyBehaviour.EnemyType.Large:
                    spawnDist += 2.0f;
                    break;
            }
            GameObject mob = Instantiate(getMobFromType(mobType), SpawnPosition(spawnDist), Quaternion.identity);
            EnemyBehaviour mobBehav = mob.GetComponent<EnemyBehaviour>();
            mobBehav.detectionRange = mobBehav.detectionRange * playerStats.lightRadius;
            mobBehav.loseDetectionRange = mobBehav.loseDetectionRange * playerStats.lightRadius;
            //Debug.Log($"detec {mobBehav.detectionRange} lose {mobBehav.loseDetectionRange}");
        }
    }

    private Vector3 SpawnPosition(float distance)
    {
        float rad = Random.Range(0, 360) * Mathf.Deg2Rad;
        float variedDist = distance * (1.0f + Random.Range(-spawnDistVari, spawnDistVari));

        return (transform.position + (new Vector3(Mathf.Sin(rad), 0, Mathf.Cos(rad)) * variedDist));
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
