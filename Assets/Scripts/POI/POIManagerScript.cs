using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POIManagerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject lowTierPOIGenerator;

    [SerializeField]
    private GameObject midTierPOIGenerator;

    [SerializeField]
    private GameObject highTierPOIGenerator;

    [SerializeField]
    public int numLowTierPOI = 10;

    [SerializeField]
    public int numMidTierPOI = 6;

    [SerializeField]
    public int numHighTierPOI = 3;

    [SerializeField]
    private float lowTierSpawnRadius = 130.0f;

    [SerializeField]
    private float midTierSpawnRadius = 90.0f;

    [SerializeField]
    private float highTierSpawnRadius = 50.0f;

    [SerializeField]
    private float tierBorderBufferDist = 20.0f;

    [SerializeField]
    private float spawnLocationAngleVariance = 10.0f; // in degrees e.g. default is [-10 to 10] degrees

    [SerializeField]
    private Vector3 spawnCenter = Vector3.zero;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(spawnCenter, lowTierSpawnRadius);
        Gizmos.DrawWireSphere(spawnCenter, midTierSpawnRadius + tierBorderBufferDist);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(spawnCenter, midTierSpawnRadius);
        Gizmos.DrawWireSphere(spawnCenter, highTierSpawnRadius + tierBorderBufferDist);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(spawnCenter, highTierSpawnRadius);
        Gizmos.DrawWireSphere(spawnCenter, tierBorderBufferDist);
    }

    void Start()
    {
        InstantiatePOIs();
    }

    private void InstantiatePOIs()
    {
        if (lowTierPOIGenerator == null)
        {
            Debug.LogError("Low Tier POI Generator is not assigned.");
            return;
        }
        if (midTierPOIGenerator == null)
        {
            Debug.LogError("Mid Tier POI Generator is not assigned.");
            return;
        }
        if (highTierPOIGenerator == null)
        {
            Debug.LogError("High Tier POI Generator is not assigned.");
            return;
        }

        float angle;
        float distance;
        Vector3 POIPos;
        GameObject POIGeneratorInstance;

        // instantiate low tier POIs ===========================
        for (int currPOI = 0; currPOI < numLowTierPOI; currPOI++)
        {
            // random distance between low and mid range
            distance = Random.Range(midTierSpawnRadius + tierBorderBufferDist, lowTierSpawnRadius);

            // evenly space POIs around spawning ring
            angle = currPOI * Mathf.PI * 2 / numLowTierPOI;
            // add angle random variance
            angle += Random.Range(-spawnLocationAngleVariance, spawnLocationAngleVariance) * Mathf.PI / 180.0f;


            POIPos = new Vector3(
                spawnCenter.x + Mathf.Cos(angle) * distance,
                spawnCenter.y,
                spawnCenter.z + Mathf.Sin(angle) * distance
            );

            POIGeneratorInstance = Instantiate(lowTierPOIGenerator, POIPos, Quaternion.identity);
            //POIGeneratorInstance.GetComponent<Script>().player = player;
        }

        // instantiate mid tier POIs ===========================
        for (int currPOI = 0; currPOI < numMidTierPOI; currPOI++)
        {
            // random distance between mid and high range
            distance = Random.Range(highTierSpawnRadius + tierBorderBufferDist, midTierSpawnRadius);

            // evenly space POIs around spawning ring
            angle = currPOI * Mathf.PI * 2 / numMidTierPOI;
            // add angle random variance
            angle += Random.Range(-spawnLocationAngleVariance, spawnLocationAngleVariance) * Mathf.PI / 180.0f;


            POIPos = new Vector3(
                spawnCenter.x + Mathf.Cos(angle) * distance,
                spawnCenter.y,
                spawnCenter.z + Mathf.Sin(angle) * distance
            );

            POIGeneratorInstance = Instantiate(midTierPOIGenerator, POIPos, Quaternion.identity);
            //POIGeneratorInstance.GetComponent<Script>().player = player;
        }

        // instantiate high tier POIs ===========================
        for (int currPOI = 0; currPOI < numHighTierPOI; currPOI++)
        {
            // random distance between center and high range
            distance = Random.Range(tierBorderBufferDist, highTierSpawnRadius);

            // evenly space POIs around spawning ring
            angle = currPOI * Mathf.PI * 2 / numHighTierPOI;
            // add angle random variance
            angle += Random.Range(-spawnLocationAngleVariance, spawnLocationAngleVariance) * Mathf.PI / 180.0f;


            POIPos = new Vector3(
                spawnCenter.x + Mathf.Cos(angle) * distance,
                spawnCenter.y,
                spawnCenter.z + Mathf.Sin(angle) * distance
            );

            POIGeneratorInstance = Instantiate(highTierPOIGenerator, POIPos, Quaternion.identity);
            //POIGeneratorInstance.GetComponent<Script>().player = player;
        }
    }
}
