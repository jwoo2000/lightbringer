using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class PlayerLevelUp : MonoBehaviour
{
    // level up stat type codes
    // 0: movespeed
    // 1: light radius
    // 2: light regen
    // 3: dmg reduction (light integrity)
    // 4: max stamina
    // 5: stamina regen
    public const int movespeedType = 0;
    public const int lightRadiusType = 1;
    public const int lightRegenType = 2;
    public const int dmgReducType = 3;
    public const int maxStaminaType = 4;
    public const int staminaRegenType = 5;

    [SerializeField]
    PlayerStats stats;

    [SerializeField]
    Stamina staminaController;

    [SerializeField]
    MovementController movementController;

    [SerializeField]
    int movespeedLvl = 0;

    [SerializeField]
    int lightRadiusLvl = 0;

    [SerializeField]
    int lightRegenLvl = 0;

    [SerializeField]
    int dmgReductionLvl = 0;

    [SerializeField]
    int maxStaminaLvl = 0;

    [SerializeField]
    int staminaRegenLvl = 0;

    private float initMS;
    private float initMaxHP;
    private float initRegen;
    private float initDmgRed;
    private float initMaxStam;
    private float initStamRegen;

    private void getInitStats()
    {
        initMS = stats.movespeed;
        initMaxHP = stats.currMaxHP;
        initRegen = stats.lightRadiusRegen;
        initDmgRed = stats.dmgReduction;
        initMaxStam = staminaController.maxStamina;
        initStamRegen = staminaController.regenerationRate;
    }

    private void Start()
    {
        getInitStats();
    }

    public void upgradeStat(int type)
    {
        switch (type)
        {
            case movespeedType:
                movespeed();
                break;
            case lightRadiusType:
                lightRadius();
                break;
            case lightRegenType:
                lightRegen();
                break;
            case dmgReducType:
                dmgReduction();
                break;
            case maxStaminaType:
                maxStamina();
                break;
            case staminaRegenType:
                staminaRegen();
                break;
            default:
                Debug.Log("Attempted to upgrade unknown stat type: " + type);
                return;
        }
    }

    private void movespeed()
    {
        movespeedLvl++;
        stats.movespeed = initMS + (movespeedLvl * 1.0f);
        movementController.setMovespeed(stats.movespeed);
    }

    private void lightRadius()
    {
        lightRadiusLvl++;
        stats.setMaxHP(initMaxHP + (lightRadiusLvl * 10.0f));
        stats.setCurrHP(stats.currMaxHP);
    }

    private void lightRegen()
    {
        lightRegenLvl++;
        stats.lightRadiusRegen = initRegen + (lightRegenLvl * 5.0f);
    }

    private void dmgReduction()
    {
        dmgReductionLvl++;
        stats.dmgReduction = initDmgRed + (dmgReductionLvl * 0.1f);
    }

    private void maxStamina()
    {
        maxStaminaLvl++;
        staminaController.maxStamina = initMaxStam + (maxStaminaLvl*20.0f);
        staminaController.stamina = staminaController.maxStamina;
    }

    private void staminaRegen()
    {
        staminaRegenLvl++;
        staminaController.regenerationRate = initStamRegen + (staminaRegenLvl * 5.0f);
    }
}
