using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BossAltarController : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private GameManager gameManager;

    [SerializeField] private float activationRange;
    [SerializeField] private bool activated;
    [SerializeField] private int levelCondition;
    [SerializeField] private float activationTime; // activation sequence time

    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private GameObject spawnParticle;
    [SerializeField] private Light playerLight;
    [SerializeField] private float playerLightInitIntensity;
    [SerializeField] private Light altarLight;
    [SerializeField] private float altarLightInitRange;
    [SerializeField] private ParticleSystem altarPS;

    [SerializeField] private GameObject conditionTextPanel;
    [SerializeField] private Color conditionTextColor;
    [SerializeField] private TMP_Text conditionText1;
    [SerializeField] private TMP_Text conditionText2;
    [SerializeField] private bool failMessageShowing;

    [SerializeField] private AudioSource altarSoundSource;
    [SerializeField] private AudioClip bossSpawnSound;
    [SerializeField] private AudioSource playerSoundSource;

    private void Awake()
    {
        activated = false;
        activationRange = 5.0f;
        levelCondition = 25;
        activationTime = 5.0f;
        failMessageShowing = false;
    }

    private void Start()
    {
        altarLightInitRange = altarLight.range;
        playerLightInitIntensity = playerLight.intensity;
        conditionText1.color = new Color(conditionTextColor.r, conditionTextColor.g, conditionTextColor.b, 0.0f);
        conditionText2.color = new Color(conditionTextColor.r, conditionTextColor.g, conditionTextColor.b, 0.0f);
        conditionText2.text = $"(Lv.{levelCondition}+ required)";
        conditionTextPanel.SetActive(false);
    }

    private void Update()
    {
        float dist = Vector3.Distance(transform.position, playerTransform.position);
        if ((dist < activationRange) && !activated && !failMessageShowing && (Time.timeScale !=  0))
        {
            if (conditionsMet())
            {
                activated = true;
                activateAltar();
            } else
            {
                if (!failMessageShowing)
                {
                    failMessageShowing = true;
                    StartCoroutine(activationFail());
                }
            }
        }
    }

    private bool conditionsMet()
    {
        return (playerStats.level >= levelCondition);
    }

    private void activateAltar()
    {
        Debug.Log("Activating altar...");
        StartCoroutine(activationSequence());
    }

    private IEnumerator activationSequence()
    {
        altarPS.Stop();
        SoundManager.instance.playBossMusic();

        float timeLeft = activationTime;
        float progress = 0.0f;

        while (timeLeft > 0.0f)
        {
            progress = timeLeft / activationTime;
            altarLight.range = altarLightInitRange * progress;
            altarSoundSource.volume = progress;
            playerLight.intensity = playerLightInitIntensity * progress;
            timeLeft -= Time.deltaTime;
            yield return null;
        }

        altarLight.range = 0.0f;
        playerLight.intensity = 0.0f;
        altarSoundSource.volume = 0.0f;
        altarSoundSource.Stop();

        Instantiate(spawnParticle, transform.position, Quaternion.identity);
        SoundManager.instance.playOneShot(playerSoundSource, bossSpawnSound, 0.5f);
        GameObject bossInstance = Instantiate(bossPrefab, transform.position, Quaternion.identity);
        BossBehaviour bossBehav = bossInstance.GetComponent<BossBehaviour>();
        bossBehav.gameManager = gameManager;
        bossBehav.playerSoundSource = playerSoundSource;

        timeLeft = 0.0f;
        while (timeLeft < 0.3f)
        {
            progress = timeLeft / 0.3f;
            playerLight.intensity = playerLightInitIntensity * progress;
            timeLeft += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator FadeOutText(TMP_Text text, float fadeDur)
    {
        Color ogColor = conditionTextColor;
        float timeElapsed = 0.0f;

        while (timeElapsed < fadeDur)
        {
            float currAlpha = Mathf.Lerp(1.0f, 0.0f, timeElapsed / fadeDur);
            text.color = new Color(ogColor.r, ogColor.g, ogColor.b, currAlpha);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        text.color = new Color(ogColor.r, ogColor.g, ogColor.b, 0.0f);
    }

    private IEnumerator FadeInText(TMP_Text text, float fadeDur)
    {
        Color ogColor = conditionTextColor;
        float timeElapsed = 0.0f;

        while (timeElapsed < fadeDur)
        {
            float currAlpha = Mathf.Lerp(0.0f, 1.0f, timeElapsed / fadeDur);
            text.color = new Color(ogColor.r, ogColor.g, ogColor.b, currAlpha);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        text.color = new Color(ogColor.r, ogColor.g, ogColor.b, 1.0f);
    }

    private IEnumerator activationFail()
    {
        Debug.Log("Activation failed!");
        conditionTextPanel.SetActive(true);
        StartCoroutine(FadeInText(conditionText1, 2.0f));
        yield return StartCoroutine(FadeInText(conditionText2, 2.0f));

        yield return new WaitForSeconds(2.0f);

        StartCoroutine(FadeOutText(conditionText1, 2.0f));
        yield return StartCoroutine(FadeOutText(conditionText2, 2.0f));

        conditionTextPanel.SetActive(false);

        yield return new WaitForSeconds(1.0f);

        failMessageShowing = false;
    }
}
