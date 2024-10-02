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
    private int numLowTierPOIs = 5;

    [SerializeField]
    private int numMidTierPOIs = 5;

    [SerializeField]
    private int numHighTierPOIs = 5;

    [SerializeField]
    private float lowTierSpawnRadius = 130.0f;

    [SerializeField]
    private float midTierSpawnRadius = 90.0f;

    [SerializeField]
    private float highTierSpawnRadius = 50.0f;

    [SerializeField]
    private float spawnLocationVariance = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
