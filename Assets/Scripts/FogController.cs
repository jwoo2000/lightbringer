using UnityEngine;

public class FogController : MonoBehaviour
{
    [SerializeField]
    private GameObject fogPrefab; // The fog particle system prefab to spawn

    [SerializeField]
    private Transform player; // Player pos



    [SerializeField]
    private float ringGap = 5f; // Distance between each spawning ring

    [SerializeField]
    private float minDistance = 10f; // Minimum distance from player the fog begins to spawn

    [SerializeField]
    private float offsetVariance = 0.5f; // Fog distance variance from its spawning ring

    [SerializeField]
    private int numRings = 5; // Number of rings of fog to make within the spawning area ("thickness" of fog)

    [SerializeField]
    private int numberOfFogInstancesPerRing = 20; // Number of fog instances to spawn per ring

    [SerializeField]
    private int fogIncreasePerRing = 2; // Increase number of fog instances by this number as ring gets further from player

    void Start()
    {
        SpawnFog();
    }

    void SpawnFog()
    {
        if (fogPrefab == null || player == null)
        {
            Debug.LogError("Fog Prefab or Player Transform is not assigned.");
            return;
        }

        float angle;
        float distance;
        Vector3 fogPosition;
        GameObject fogInstance;
        for (int ring = 0; ring < numRings; ring++)
        {
            distance = minDistance + (ring * ringGap);

            for (int i = 0; i < numberOfFogInstancesPerRing; i++)
            {
                // evenly space fog around circumference of circle
                angle = i * Mathf.PI * 2 / numberOfFogInstancesPerRing;

                // introduce random distance variance offset from curr spawning ring
                if (ring == 0)
                {
                    distance += Random.Range(0f, offsetVariance);
                } else
                {
                    distance += Random.Range(-offsetVariance, offsetVariance);
                }

                fogPosition = new Vector3(
                    player.position.x + Mathf.Cos(angle) * distance,
                    player.position.y,
                    player.position.z + Mathf.Sin(angle) * distance
                );

                fogInstance = Instantiate(fogPrefab, fogPosition, Quaternion.identity);
                fogInstance.GetComponent<FogInstanceScript>().player = player;

            }
            numberOfFogInstancesPerRing += fogIncreasePerRing;
        }
    }
    //public GameObject fogPrefab;   // Assign the Fog Prefab in the Inspector
    //public Transform player;       // Assign the Player in the Inspector
    //public float fogAreaSize = 20f; // Adjust this to cover the game area
    //private GameObject[] fogInstances;

    //void Start()
    //{
    //    SpawnFog();
    //}

    //void SpawnFog()
    //{
    //    int gridSize = Mathf.CeilToInt(fogAreaSize / fogPrefab.GetComponent<ParticleSystem>().shape.scale.x);
    //    fogInstances = new GameObject[gridSize * gridSize];

    //    for (int x = 0; x < gridSize; x++)
    //    {
    //        for (int z = 0; z < gridSize; z++)
    //        {
    //            Vector3 spawnPosition = new Vector3(x * fogPrefab.GetComponent<ParticleSystem>().shape.scale.x, 0, z * fogPrefab.GetComponent<ParticleSystem>().shape.scale.z);
    //            fogInstances[x * gridSize + z] = Instantiate(fogPrefab, spawnPosition, Quaternion.identity);
    //        }
    //    }
    //}

    //public void DissipateFog(Vector3 position, float radius)
    //{
    //    foreach (var fogInstance in fogInstances)
    //    {
    //        if (Vector3.Distance(fogInstance.transform.position, position) < radius)
    //        {
    //            Destroy(fogInstance);
    //        }
    //    }
    //}
}
