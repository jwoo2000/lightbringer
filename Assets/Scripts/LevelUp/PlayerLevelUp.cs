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
    public const int movespeedType = 0;
    public const int lightRadiusType = 1;
    public const int lightRegenType = 2;
    public const int dmgReducType = 3;

    [SerializeField]
    PlayerStats stats;

    [SerializeField]
    int movespeedLvl = 0;

    [SerializeField]
    int lightRadiusLvl = 0;

    [SerializeField]
    int lightRegenLvl = 0;

    [SerializeField]
    int dmgReductionLvl = 0;

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
            default:
                Debug.Log("Attempted to upgrade unknown stat type: " + type);
                return;
        }
    }

    private void movespeed()
    {
        movespeedLvl++;
    }

    private void lightRadius()
    {
        lightRadiusLvl++;
    }

    private void lightRegen()
    {
        lightRegenLvl++;
    }

    private void dmgReduction()
    {
        dmgReductionLvl++;
    }
}
