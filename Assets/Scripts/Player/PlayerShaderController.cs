using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShaderController : MonoBehaviour
{
    private Renderer playerRenderer;
    private Material playerMaterial;
    [SerializeField]
    private PlayerStats playerStats;
    private float playerHPratio;

    // Shader properties
    private float initBaseIntensity = 2.0f;
    private float initPulseSpeed = 2.0f;

    private void Start()
    {
        playerRenderer = GetComponent<Renderer>();
        if (playerRenderer != null)
        {
            playerMaterial = playerRenderer.material;
        }
    }

    private void Update()
    {
        playerHPratio = playerStats.currHP / playerStats.currMaxHP;
        adjustPulse(
            Mathf.Lerp(0.8f, initBaseIntensity, playerHPratio),
            Mathf.Lerp(6.0f, initPulseSpeed, playerHPratio)
            );
    }

    private void adjustPulse(float baseIntensity, float pulseSpeed)
    {
        if (playerMaterial != null)
        {
            playerMaterial.SetFloat("_BaseIntensity", baseIntensity);
            playerMaterial.SetFloat("_PulseSpeed", pulseSpeed);
        } else
        {
            Debug.LogWarning("playershadercontroller: player mat null");
        }
    }
}
