using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnRate; 
    [SerializeField] private GameObject[] enemyPrefabs; 
    [SerializeField] private bool isSpawning = true; 
    [SerializeField] private float spawnDistanceToPlayer;
    [SerializeField] private int maxEnemies = 0;
    private int numEnemies;


    private void Start() {
        StartCoroutine(Spawner());
        numEnemies = 0;
    }

    private IEnumerator Spawner () {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);

        while (isSpawning) {
            yield return wait;
            int rand = Random.Range(0, enemyPrefabs.Length);
            GameObject enemyToSpawn = enemyPrefabs[rand];

            if (numEnemies < maxEnemies || maxEnemies == 0) {
                Instantiate(enemyToSpawn, SpawnPosition(), Quaternion.identity);
                numEnemies++;
            } else {
                isSpawning = false;
            }

        }
    }

    private Vector3 SpawnPosition() {
        int deg = Random.Range(0, 360);
        float rad = deg * Mathf.Deg2Rad;

        return (new Vector3(spawnDistanceToPlayer * Mathf.Sin(rad), 1, spawnDistanceToPlayer * Mathf.Cos(rad)) + transform.position);
    }

}
