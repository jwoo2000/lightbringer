using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShaderController : MonoBehaviour
{
    [SerializeField]
    private Renderer playerRenderer;
    private Material playerMaterial;
    [SerializeField]
    private PlayerStats playerStats;
    private float playerHPratio;

    // Shader properties
    private float baseIntensity;
    private float pulseSpeed;
    private float pulseIntensity;

    private float initBaseIntensity;
    private float initPulseSpeed;
    private float initPulseIntensity;

    private float accumulatedPhase;

    private bool isPulsingFromDamage = false;
    private float damagePulseDuration = 0.1f;
    private float pulseBoostFactor = 1.2f;

    private void Awake()
    {
        accumulatedPhase = 0.0f;
    }

    private void Start()
    {
        if (playerRenderer != null)
        {
            playerMaterial = playerRenderer.material;
            initBaseIntensity = playerMaterial.GetFloat("_BaseIntensity");
            initPulseSpeed = playerMaterial.GetFloat("_PulseSpeed");
            initPulseIntensity = playerMaterial.GetFloat("_PulseIntensity");
        }
    }

    private void Update()
    {
        playerHPratio = playerStats.currHP / playerStats.currMaxHP;

        baseIntensity = Mathf.Lerp(0.5f, initBaseIntensity, playerHPratio);
        pulseSpeed = Mathf.Lerp(10.0f, initPulseSpeed, playerHPratio);
        pulseIntensity = Mathf.Lerp(0.1f, initPulseIntensity, playerHPratio);

        accumulatedPhase += Time.deltaTime * pulseSpeed;

        if (!isPulsingFromDamage)
        {
            adjustPulse(baseIntensity, pulseSpeed, pulseIntensity, accumulatedPhase);
        }
    }

    private void adjustPulse(float baseIntensity, float pulseSpeed, float pulseIntensity, float phase)
    {
        if (playerMaterial != null)
        {
            playerMaterial.SetFloat("_BaseIntensity", baseIntensity);
            playerMaterial.SetFloat("_PulseSpeed", pulseSpeed);
            playerMaterial.SetFloat("_PulseIntensity", pulseIntensity);
            playerMaterial.SetFloat("_Phase", phase);
        }
        else
        {
            Debug.LogWarning("PlayerShaderController: player material is null");
        }
    }

    public void TriggerDamagePulse()
    {
        if (!isPulsingFromDamage)
        {
            StartCoroutine(DamagePulseCoroutine());
        }
    }

    private IEnumerator DamagePulseCoroutine()
    {
        isPulsingFromDamage = true;

        float boostedIntensity = baseIntensity * pulseBoostFactor;
        float boostedPulseSpeed = 2.0f*damagePulseDuration;
        accumulatedPhase = damagePulseDuration;

        float timeElapsed = 0.0f;
        while (timeElapsed < damagePulseDuration)
        {
            adjustPulse(boostedIntensity, boostedPulseSpeed, pulseIntensity, accumulatedPhase);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        isPulsingFromDamage = false;
    }
}
