using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;

    [SerializeField]
    private MenuController menuController;

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
    public float dmgReduction = 0.0f; // % dmg taken reduced

    [SerializeField]
    private float initMaxHP = 100.0f;

    [SerializeField]
    public int exp = 0;

    [SerializeField]
    private int maxExp = 30;

    [SerializeField]
    public int level = 0;

    public int Exp
    {
        get
        {
            return exp;
        }
    }

    public int MaxExp
    {
        get
        {
            return maxExp;
        }
    }


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

    public void addExp(int expToAdd)
    {
        exp += expToAdd;
        checkLevelUp();
    }

    private void checkLevelUp()
    {
        while (exp >= maxExp)
        {
            exp -= maxExp;  // carry over extra exp to the next level
            level++;
            maxExp = CalculateNextMaxExp(level);
            levelUp();
        }
    }

    private int CalculateNextMaxExp(int level)
    {
        if (level <= 1)
        {
            return 30;  // starting max exp for level 0, 1
        }
        else if (level >= 2 && level <= 5)
        {
            return 30 + (level - 1) * 5; // increase by 5 from lv2~5
        }
        else if (level >= 6 && level <= 10)
        {
            return 50 + (level - 1) * 10; // increase by 10 from 6~10
        }

        else // level >= 11
        {
            return 140 + (level - 1) * 20; // increase by 20 from 11+
        }
    }

    private void levelUp()
    {
        Debug.Log("Level Up!");
        menuController.levelUp();
    }

    private void initPlayerHP()
    {
        gameManager._playerHealth = new UnitHealth(initMaxHP, initMaxHP);
    }

    private void updateLightRadFromHP()
    {
        maxLightRadius = gameManager._playerHealth.MaxHealth / initMaxHP;
        lightRadius = calcHpToLightRadius(gameManager._playerHealth);
    }

    private float calcHpToLightRadius(UnitHealth unitHealth)
    {
        float hpRatio = (float) unitHealth.Health / (float) unitHealth.MaxHealth;
        float radius = hpRatio * maxLightRadius;
        return radius;
    }

    private void regenHP()
    {
        float maxHP = gameManager._playerHealth.MaxHealth;
        float currHP = gameManager._playerHealth.Health;
        if (currHP < maxHP)
        {
            gameManager._playerHealth.Health = Mathf.Clamp(currHP + (lightRadiusRegen * Time.deltaTime), 0.0f, maxHP);
        }
    }

    private void updateLightRadius(float newScale)
    {
        fogClearer.GetComponent<FogClearerController>().updateScale(newScale);
        visibilityPainter.transform.localScale = new Vector3(newScale, 0, newScale);
    }
}
