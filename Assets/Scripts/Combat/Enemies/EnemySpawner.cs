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
    private WeaponController playerWeps;
    private int numEnemies;
    public static int discoveredPOIcount = 0;
    private bool spawnerEnabled = false;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(transform.position, startSpawningDistance);
    }

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerWeps = player.GetComponent<WeaponController>();
    }

    private void Update()
    {
        if ((discoveredPOIcount > 1) && !spawnerEnabled)
        {
            spawnerEnabled = true;
            StartSpawner();
        }
    }

    private void StartSpawner()
    {
        numEnemies = 0;
        StartCoroutine(Spawner());
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
