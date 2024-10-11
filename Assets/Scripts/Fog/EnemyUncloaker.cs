using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUncloaker : MonoBehaviour
{
    [SerializeField]
    private EnemyBehaviour enemyBehaviour;

    [SerializeField]
    private float discoverDist = 15.0f;

    [SerializeField]
    private bool discovered = false;

    [SerializeField]
    private Color cloakedColor = new Color(0.0f,0.0f,0.0f,1.0f);

    private Color[] uncloakedColours;

    [SerializeField]
    private float uncloakDur = 1.0f;

    private Renderer[] allRenderers;

    private void Start()
    {
        allRenderers = GetComponentsInChildren<Renderer>();
        uncloakedColours = new Color[allRenderers.Length];

        for (int i = 0; i < allRenderers.Length; i++)
        {
            uncloakedColours[i] = allRenderers[i].material.color;
            allRenderers[i].material = new Material(allRenderers[i].material);
        }

        cloak();
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(transform.position, enemyBehaviour.target.position);
        if ((dist < discoverDist) && !discovered)
        {
            uncloak();
            discovered = true;
        }
    }

    private void cloak()
    {
        foreach (Renderer renderer in allRenderers)
        {
            if (renderer != null && renderer.material != null)
            {
                renderer.material.color = cloakedColor;
            }
        }
    }

    private void uncloak()
    {
        for (int i = 0;i < allRenderers.Length;i++)
        {
            if (allRenderers[i] != null && allRenderers[i].material != null)
            {
                StartCoroutine(fadeUncloak(allRenderers[i], i));
            }
        }
    }

    private IEnumerator fadeUncloak(Renderer renderer, int colourIndex)
    {
        float elapsed = 0.0f;
        while (elapsed < uncloakDur)
        {
            renderer.material.color = Color.Lerp(cloakedColor, uncloakedColours[colourIndex], elapsed/uncloakDur);
            elapsed += Time.deltaTime;
            yield return null;
        }

        renderer.material.color = uncloakedColours[colourIndex];
    }
}
