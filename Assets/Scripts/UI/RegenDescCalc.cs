using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RegenDescCalc : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI regenDescText;

    void Start()
    {
        PlayerLevelUp playerLevelUp = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLevelUp>();
        float currRegen = playerLevelUp.calcRegen(playerLevelUp.lightRegenLvl, playerLevelUp.regenScaleFactor);
        float newRegen = playerLevelUp.calcRegen(playerLevelUp.lightRegenLvl + 1, playerLevelUp.regenScaleFactor);
        float regenInc = newRegen - currRegen;
        regenDescText.text = $"(+{regenInc * 100.0f:F1}% max HP/s)";
    }
}
