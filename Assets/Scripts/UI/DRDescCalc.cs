using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DRDescCalc : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI drDescText;

    void Start()
    {
        PlayerLevelUp playerLevelUp = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLevelUp>();
        float currDR = playerLevelUp.calcDR(playerLevelUp.dmgReductionLvl, playerLevelUp.dmgRedScalingFactor);
        float newDR = playerLevelUp.calcDR(playerLevelUp.dmgReductionLvl+1, playerLevelUp.dmgRedScalingFactor);
        float drInc = newDR - currDR;
        drDescText.text = $"(+{drInc * 100.0f:F1}% damage reduction)";
    }
}
