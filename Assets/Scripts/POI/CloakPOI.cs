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
    public GameObject wepFirefly;

    private Material[][] ogMats;
    private Renderer[] renderers;

    [SerializeField]
    public bool discovered = false;

    [SerializeField]
    public Transform playerTransform;

    [SerializeField]
    public float discoverDist;

    [SerializeField]
    private Color cloakedColor = new Color(0.0f, 0.0f, 0.0f, 1.0f);
    [SerializeField]
    private float cloakedMetallic = 1.0f;
    [SerializeField]
    private float cloakedSmooth = 0.0f;

    private Color[] uncloakedColours;
    private float[] uncloakedMetallic;
    private float[] uncloakedSmoothness;

    [SerializeField]
    private float uncloakDur = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        renderers = POIObjects.GetComponentsInChildren<Renderer>();
        uncloakedColours = new Color[renderers.Length];
        uncloakedMetallic = new float[renderers.Length];
        uncloakedSmoothness = new float[renderers.Length];
        saveOgMats();

        for (int i = 0; i < renderers.Length; i++)
        {
            if (renderers[i].material.HasProperty("_Color"))
            {
                uncloakedColours[i] = renderers[i].material.color;
            } else
            {
                uncloakedColours[i] = Color.white;
            }
                
            if (renderers[i].material.HasProperty("_Metallic"))
            {
                uncloakedMetallic[i] = renderers[i].material.GetFloat("_Metallic");
            } else
            {
                uncloakedMetallic[i] = 0;
            }
            if (renderers[i].material.HasProperty("_Smoothness"))
            {
                uncloakedSmoothness[i] = renderers[i].material.GetFloat("_Smoothness");
            } else
            {
                uncloakedSmoothness[i] = 0;
            }
            renderers[i].material = new Material(renderers[i].material);
        }

        cloak();
    }
    private void Update()
    {
        float dist = Vector3.Distance(transform.position, playerTransform.position);
        if ((dist < discoverDist) && !discovered)
        {
            uncloak();
            discovered = true;
            EnemySpawner.discoveredPOIcount++;
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
        if (wepFirefly != null)
        {
            wepFirefly.SetActive(false);
        }
        for (int i = 0; i < renderers.Length; i++)
        {
            if (renderers[i] != null && renderers[i].material != null)
            {
                if (renderers[i].material.HasProperty("_Color"))
                {
                    renderers[i].material.color = cloakedColor;
                }
                if (renderers[i].material.HasProperty("_Metallic"))
                {
                    renderers[i].material.SetFloat("_Metallic", cloakedMetallic);
                }
                if (renderers[i].material.HasProperty("_Smoothness"))
                {
                    renderers[i].material.SetFloat("_Glossiness", cloakedSmooth);
                }

                Material[] materials = renderers[i].materials;
                // change all materials in children to cloakmat
                for (int j = 0; j < renderers[i].materials.Length; j++)
                {
                    materials[j] = cloakMat;
                }
                renderers[i].materials = materials;
            }
        }
    }

    public void uncloak()
    {
        visibilityPainter.SetActive(true);
        if (wepFirefly != null)
        {
            wepFirefly.SetActive(true);
        }
        for (int i = 0; i < renderers.Length; i++)
        {
            if (renderers[i] != null && renderers[i].material != null)
            {
                Material[] materials = renderers[i].materials;
                // revert the materials of all children to original materials
                for (int j = 0; j < renderers[i].materials.Length; j++)
                {
                    materials[j] = ogMats[i][j];
                }
                renderers[i].materials = materials;
                StartCoroutine(fadeUncloak(renderers[i], i));
            }
        }
    }

    private IEnumerator fadeUncloak(Renderer renderer, int colourIndex)
    {
        float elapsed = 0.0f;
        while (elapsed < uncloakDur)
        {
            if (renderer.material.HasProperty("_Color"))
            { 
                renderer.material.color = Color.Lerp(cloakedColor, uncloakedColours[colourIndex], elapsed / uncloakDur);
            }
            if (renderer.material.HasProperty("_Metallic"))
            {
                renderer.material.SetFloat("_Metallic", Mathf.Lerp(cloakedMetallic, uncloakedMetallic[colourIndex], elapsed / uncloakDur));
            }
            if (renderer.material.HasProperty("_Smoothness"))
            {
                renderer.material.SetFloat("_Glossiness", Mathf.Lerp(cloakedSmooth, uncloakedSmoothness[colourIndex], elapsed / uncloakDur));
            }
            
            elapsed += Time.deltaTime;
            yield return null;
        }

        renderer.material.color = uncloakedColours[colourIndex];
    }
}