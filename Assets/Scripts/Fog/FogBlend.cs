using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogBlend : MonoBehaviour
{
    [SerializeField]
    private Material material;

    [SerializeField]
    private float blendSpeed = 5;

    [SerializeField]
    private RenderTexture fogRenderTexture;

    private RenderTexture oldTexture;
    private RenderTexture currTexture;

    private float blendAmt;

    void Awake()
    {
        oldTexture = GenerateTexture();
        currTexture = GenerateTexture();
        material.SetTexture("_OldTex", oldTexture);
        material.SetTexture("_CurrTex", currTexture);


        UpdateTextures();
    }

    RenderTexture GenerateTexture()
    {
        RenderTexture texture = new RenderTexture(fogRenderTexture);
        return texture;
    }

    void UpdateTextures()
    {
        StopCoroutine(BlendTextures());

        blendAmt = 0.0f;
        Graphics.Blit(currTexture, oldTexture);
        Graphics.Blit(fogRenderTexture, currTexture);

        StartCoroutine(BlendTextures());
    }

    IEnumerator BlendTextures()
    {
        while (blendAmt < 1.0f)
        {
            blendAmt += blendSpeed*Time.deltaTime;
            material.SetFloat("_Blend", blendAmt);
            yield return null;
        }
        UpdateTextures();
    }

}
