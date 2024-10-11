using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlayerSpawner : MonoBehaviour
{
    [SerializeField] private float baseSpawnRate = 10.0f; 
    [SerializeField] private GameObject enemySmall; 
    [SerializeField] private GameObject enemyMedium;
    [SerializeField] private GameObject enemyLarge;  
    [SerializeField] private bool isSpawning = true; 
    [SerializeField] private float spawnDistance = 10.0f;
    GameObject player;
    private WeaponController weaponController;
    private PlayerStats playerStats;
    private int playerLevel;

    private float spawnRate;


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        weaponController = player.GetComponent<WeaponController>();
        playerStats = player.GetComponent<PlayerStats>();
    }

    void Start() {
        spawnRate = baseSpawnRate;
        StartCoroutine(Spawner());
        playerLevel = playerStats.level;
    }

    private IEnumerator Spawner () {
        Debug.Log("spawn rate:" + baseSpawnRate);
        while (isSpawning) {
            WaitForSeconds wait = new WaitForSeconds(spawnRate);
            yield return wait;
            playerLevel = playerStats.level;
            spawnRate = 20.0f / (playerLevel + 2);
            Debug.Log(spawnRate);
            if (weaponController.HighWeapon != null) 
            {
                int pick = Random.Range(0, 4);
                if (pick == 0)
                {
                    Instantiate(enemyLarge, SpawnPosition(spawnDistance * 2.0f), Quaternion.identity);
                }
                else
                {
                    Instantiate(enemyMedium, SpawnPosition(spawnDistance * 1.5f), Quaternion.identity);
                }
            }
            else if (weaponController.MidWeapon != null) 
            {
                int pick = Random.Range(0, 2);
                if (pick == 0)
                {
                    Instantiate(enemySmall, SpawnPosition(spawnDistance), Quaternion.identity);
                }
                else 
                {
                    Instantiate(enemyMedium, SpawnPosition(spawnDistance * 1.5f), Quaternion.identity);
                }
            }
            else if (weaponController.LowWeapon != null) 
            {
                Instantiate(enemySmall, SpawnPosition(spawnDistance), Quaternion.identity);
            }
        }
    }

    private Vector3 SpawnPosition(float distance) {
        int deg = Random.Range(0, 360);
        float rad = deg * Mathf.Deg2Rad;

        return (new Vector3(distance * Mathf.Sin(rad), 1, distance * Mathf.Cos(rad)) + transform.position);
    }

}
