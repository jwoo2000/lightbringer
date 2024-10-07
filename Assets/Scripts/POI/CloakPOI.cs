using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CloakPOI : MonoBehaviour
{
    public Material cloakMat;

    [SerializeField]
    private GameObject POIObjects;

    [SerializeField]
    private GameObject visibilityPainter;

    [SerializeField]
    private GameObject wepFirefly;

    private Material[][] ogMats;
    private Renderer[] renderers;

    [SerializeField]
    private bool discovered = false;

    [SerializeField]
    public Transform playerTransform;

    [SerializeField]
    public float discoverDist;

    // Start is called before the first frame update
    void Start()
    {
        renderers = POIObjects.GetComponentsInChildren<Renderer>();
        saveOgMats();
        wepFirefly.GetComponent<AbsorbFireflies>().attractor = playerTransform;
        cloak();
    }
    private void Update()
    {
        float dist = Vector3.Distance(transform.position, playerTransform.position);
        if ((dist < discoverDist) && !discovered)
        {
            uncloak();
            discovered = true;
        }
    }

    void saveOgMats()
    {
        // store array of materials for every renderer [materials[],materials[], etc]
        ogMats = new Material[renderers.Length][];

        for (int i = 0; i < renderers.Length; i++)
        {
            ogMats[i] = new Material[renderers[i].materials.Length];

            for (int j = 0; j < renderers[i].materials.Length; j++)
            {
                ogMats[i][j] = renderers[i].materials[j];
            }
        }
    }

    public void cloak()
    {
        //Debug.Log("cloaking poi");
        visibilityPainter.SetActive(false);
        wepFirefly.SetActive(false);
        for (int i = 0; i < renderers.Length; i++)
        {
            Material[] materials = renderers[i].materials;
            // change all materials in children to cloakmat
            for (int j = 0; j < renderers[i].materials.Length; j++)
            {
                materials[j] = cloakMat;
            }
            renderers[i].materials = materials;
        }
    }

    public void uncloak()
    {
        visibilityPainter.SetActive(true);
        wepFirefly.SetActive(true);
        for (int i = 0; i < renderers.Length; i++)
        {
            Material[] materials = renderers[i].materials;
            // revert the materials of all children to original materials
            for (int j = 0; j < renderers[i].materials.Length; j++)
            {
                materials[j] = ogMats[i][j];
            }
            renderers[i].materials = materials;
        }
    }
}