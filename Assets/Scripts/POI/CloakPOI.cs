using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloakPOI : MonoBehaviour
{
    public Shader cloakShader;

    private Shader[][] ogShaders;
    private Renderer[] renderers;

    [SerializeField]
    private bool cloaked = false;

    // Start is called before the first frame update
    void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();
        saveOgShaders();
    }

    void saveOgShaders()
    {
        // store array of shaders for every renderer [shaders[],shaders[], etc]
        ogShaders = new Shader[renderers.Length][];

        for (int i = 0; i < renderers.Length; i++)
        {
            ogShaders[i] = new Shader[renderers[i].materials.Length];

            for (int j = 0; j < renderers[i].materials.Length; j++)
            {
                ogShaders[i][j] = renderers[i].materials[j].shader;
            }
        }
    }

    public void cloak()
    {
        cloaked = true;
        for (int i = 0; i < renderers.Length; i++)
        {
            // change all shaders in children to cloakshader
            for (int j = 0; j < renderers[i].materials.Length; j++)
            {
                renderers[i].materials[j].shader = cloakShader;
            }
        }
    }

    public void uncloak()
    {
        cloaked = false;
        for (int i = 0; i < renderers.Length; i++)
        {
            // revert the shader of all children to original shaders
            for (int j = 0; j < renderers[i].materials.Length; j++)
            {
                renderers[i].materials[j].shader = ogShaders[i][j];
            }
        }
    }
}
