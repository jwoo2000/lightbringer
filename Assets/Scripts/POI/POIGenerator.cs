using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POIGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject[] variants;

    [SerializeField]
    private int chosenVariant;

    // Start is called before the first frame update
    void Start()
    {
        if (variants.Length == 0)
        {
            Debug.LogError("No variants assigned!");
            return;
        }

        chosenVariant = Random.Range(0, variants.Length);

        float randomYRotation = Random.Range(0.0f, 360.0f);
        Quaternion randomRotation = Quaternion.Euler(0, randomYRotation, 0);

        Instantiate(variants[chosenVariant], transform.position, randomRotation);
    }
}
