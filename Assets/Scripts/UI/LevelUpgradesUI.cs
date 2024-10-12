using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelUpgradesUI : MonoBehaviour
{
    [SerializeField] private PlayerLevelUp playerLevelUp;
    [SerializeField] private PlayerStats stats;
    [SerializeField] private Stamina stamina;

    [SerializeField] private TextMeshProUGUI movespeedValue;
    [SerializeField] private TextMeshProUGUI msActual;

    [SerializeField] private TextMeshProUGUI lightRadValue;
    [SerializeField] private TextMeshProUGUI mhpActual;

    [SerializeField] private TextMeshProUGUI lightRegenValue;
    [SerializeField] private TextMeshProUGUI lrActual;

    [SerializeField] private TextMeshProUGUI dmgReducValue;
    [SerializeField] private TextMeshProUGUI drActual;

    [SerializeField] private TextMeshProUGUI maxStaminaValue;
    [SerializeField] private TextMeshProUGUI mStActual;

    [SerializeField] private TextMeshProUGUI staminaRegenValue;
    [SerializeField] private TextMeshProUGUI StRActual;

    [SerializeField] private TextMeshProUGUI levelValue;

    [SerializeField] private TextMeshProUGUI expValue;

    public void updateUI()
    {
        movespeedValue.text = playerLevelUp.movespeedLvl.ToString();
        msActual.text = $"+{playerLevelUp.movespeedLvl*10.0f:F0}% ms";

        lightRadValue.text = playerLevelUp.lightRadiusLvl.ToString();
        mhpActual.text = $"{stats.currMaxHP} mHP";


        lightRegenValue.text = playerLevelUp.lightRegenLvl.ToString();
        lrActual.text = $"{stats.lightRadiusRegen*100.0f:F1}% HP/s";


        dmgReducValue.text = playerLevelUp.dmgReductionLvl.ToString();
        drActual.text = $"{stats.dmgReduction * 100.0f:F1}% DR";


        maxStaminaValue.text = playerLevelUp.maxStaminaLvl.ToString();
        mStActual.text = $"{stamina.maxStamina} mSt";


        staminaRegenValue.text = playerLevelUp.staminaRegenLvl.ToString();
        StRActual.text = $"{stamina.regenerationRate} St/s";


        levelValue.text = $"Level {stats.level}";
        expValue.text = $"To next level: {stats.Exp}/{stats.MaxExp}";
    }
}
