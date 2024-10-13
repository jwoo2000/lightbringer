using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnRate; 
    [SerializeField] private GameObject[] enemyPrefabs; 
    [SerializeField] private bool isSpawning = true; 
    [SerializeField] private float spawnDistance;
    [SerializeField] private int maxEnemies = 0;
    [SerializeField] private int startSpawningDistance = 50;
    GameObject player;
    private int numEnemies;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }


    void Start() {
        StartCoroutine(Spawner());
        numEnemies = 0;
    }

    void Update()
    {
        
    }

    private IEnumerator Spawner () {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);
        while(true) {
            yield return wait;
            if (Vector3.Distance(player.transform.position, transform.position) < startSpawningDistance)
            {
                isSpawning = true;
            }
            else
            {
                isSpawning = false;
            }


            while (isSpawning) {
                yield return wait;
                int rand = Random.Range(0, enemyPrefabs.Length);
                GameObject enemyToSpawn = enemyPrefabs[rand];

                if (numEnemies < maxEnemies || maxEnemies == 0) 
                {
                    Instantiate(enemyToSpawn, SpawnPosition(), Quaternion.identity);
                    numEnemies++;
                } 
                else 
                {
                    isSpawning = false;
                }
            }
        }
        
    }

    private Vector3 SpawnPosition() {
        int deg = Random.Range(0, 360);
        float rad = deg * Mathf.Deg2Rad;

        return (new Vector3(spawnDistance * Mathf.Sin(rad), 1, spawnDistance * Mathf.Cos(rad)) + transform.position);
    }

}
