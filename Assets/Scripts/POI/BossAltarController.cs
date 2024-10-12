using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAltarController : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Transform playerTransform;

    [SerializeField] private float activationRange;
    [SerializeField] private bool activated;
    [SerializeField] private int levelCondition;
    [SerializeField] private float activationTime; // activation sequence time

    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private Light playerLight;
    [SerializeField] private float playerLightInitIntensity;
    [SerializeField] private Light altarLight;
    [SerializeField] private float altarLightInitRange;
    [SerializeField] private ParticleSystem altarPS;

    private void Awake()
    {
        activated = false;
        activationRange = 5.0f;
        levelCondition = 20;
        activationTime = 5.0f;
    }

    private void Start()
    {
        altarLightInitRange = altarLight.range;
        playerLightInitIntensity = playerLight.intensity;
    }

    private void Update()
    {
        float dist = Vector3.Distance(transform.position, playerTransform.position);
        if ((dist < activationRange) && !activated && (Time.timeScale !=  0))
        {
            if (conditionsMet())
            {
                activated = true;
                activateAltar();
            } else
            {
                activationFail();
            }
        }
    }

    private bool conditionsMet()
    {
        return (playerStats.level > levelCondition);
    }

    private void activateAltar()
    {
        Debug.Log("Activating altar...");
        StartCoroutine(activationSequence());
    }

    private IEnumerator activationSequence()
    {
        altarPS.Stop();

        float timeLeft = activationTime;
        float progress = 0.0f;

        while (timeLeft > 0.0f)
        {
            progress = timeLeft / activationTime;
            altarLight.range = altarLightInitRange * progress;
            playerLight.intensity = playerLightInitIntensity * progress;
            timeLeft -= Time.deltaTime;
            yield return null;
        }

        altarLight.range = 0.0f;
        playerLight.intensity = 0.0f;
        Debug.Log("SPAWN THE BOSS");
        timeLeft = 0.0f;
        while (timeLeft < 0.3f)
        {
            progress = timeLeft / 0.3f;
            playerLight.intensity = playerLightInitIntensity * progress;
            timeLeft += Time.deltaTime;
            yield return null;
        }
    }

    private void activationFail()
    {
        Debug.Log("Activation failed!");
    }
}
