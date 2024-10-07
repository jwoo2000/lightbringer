using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLocationScript : MonoBehaviour
{
    [SerializeField]
    private float spawnRingRadius = 140.0f;

    [SerializeField]
    private Vector3 spawnRingCenter = Vector3.zero;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(spawnRingCenter, spawnRingRadius);
    }

    void Awake()
    {
        float angle = Random.Range(0, Mathf.PI * 2);
        transform.position = new Vector3(
                spawnRingCenter.x + Mathf.Cos(angle) * spawnRingRadius,
                spawnRingCenter.y,
                spawnRingCenter.z + Mathf.Sin(angle) * spawnRingRadius
            );
    }
}
