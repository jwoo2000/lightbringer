using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelUpgradesUI : MonoBehaviour
{
    [SerializeField]
    private PlayerLevelUp playerLevelUp;

    [SerializeField]
    private TextMeshProUGUI movespeedValue;

    [SerializeField]
    private TextMeshProUGUI lightRadValue;

    [SerializeField]
    private TextMeshProUGUI lightRegenValue;

    [SerializeField]
    private TextMeshProUGUI dmgReducValue;

    [SerializeField]
    private TextMeshProUGUI maxStaminaValue;

    [SerializeField]
    private TextMeshProUGUI staminaRegenValue;

    public void updateUI()
    {
        movespeedValue.text = playerLevelUp.movespeedLvl.ToString();
        lightRadValue.text = playerLevelUp.lightRadiusLvl.ToString();
        lightRadValue.text = playerLevelUp.lightRegenLvl.ToString();
        dmgReducValue.text = playerLevelUp.dmgReductionLvl.ToString();
        maxStaminaValue.text = playerLevelUp.maxStaminaLvl.ToString();
        staminaRegenValue.text = playerLevelUp.staminaRegenLvl.ToString();
    }
}
