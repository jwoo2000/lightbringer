using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private GameObject gameManager;

    [SerializeField]
    private MovementController movementController;

    [SerializeField]
    private GameObject fogClearer;

    [SerializeField]
    private GameObject visibilityPainter;

    [SerializeField]
    public float movespeed = 3.0f;

    [SerializeField]
    private float sprintMulti = 1.5f;

    [SerializeField]
    public float lightRadius = 1.0f;

    [SerializeField]
    public float maxLightRadius = 1.0f;

    [SerializeField]
    public float lightRadiusRegen = 1f; // % max hp per second

    [SerializeField]
    private float initMaxHP = 100.0f;

    private void Awake()
    {
        // initialise all player stats
        movementController.setMovespeed(movespeed);
        movementController.setSprintMulti(sprintMulti);
        updateLightRadius(maxLightRadius);
        initPlayerHP();
    }

    private void Update()
    {
        // get HP values and update GameManager with new values
        regenHP();
        // calc light radius based on HP ratios
        updateLightRadFromHP();
        // update visible light radius
        updateLightRadius(lightRadius);
    }

    private void initPlayerHP()
    {
        gameManager.GetComponent<GameManager>()._playerHealth = new UnitHealth(initMaxHP, initMaxHP);
    }

    private void updateLightRadFromHP()
    {
        maxLightRadius = gameManager.GetComponent<GameManager>()._playerHealth.MaxHealth / initMaxHP;
        lightRadius = calcHpToLightRadius(gameManager.GetComponent<GameManager>()._playerHealth);
    }

    private float calcHpToLightRadius(UnitHealth unitHealth)
    {
        float hpRatio = (float) unitHealth.Health / (float) unitHealth.MaxHealth;
        float radius = hpRatio * maxLightRadius;
        return radius;
    }

    private void regenHP()
    {
        float maxHP = gameManager.GetComponent<GameManager>()._playerHealth.MaxHealth;
        float currHP = gameManager.GetComponent<GameManager>()._playerHealth.Health;
        if (currHP < maxHP)
        {
            gameManager.GetComponent<GameManager>()._playerHealth.Health = Mathf.Clamp(currHP + (lightRadiusRegen * Time.deltaTime), 0.0f, maxHP);
        }
    }

    private void updateLightRadius(float newScale)
    {
        fogClearer.GetComponent<FogClearerController>().updateScale(newScale);
        visibilityPainter.transform.localScale = new Vector3(newScale, 0, newScale);
    }
}
