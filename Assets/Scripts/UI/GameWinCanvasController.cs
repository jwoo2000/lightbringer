using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWinCanvasController : MonoBehaviour
{
    [SerializeField] private LevelUpgradesUI levelUpgradesUI;
    [SerializeField] private WeaponInfoPanelController weaponInfoPanelController;

    private void OnEnable()
    {
        levelUpgradesUI.updateUI();
        weaponInfoPanelController.updateAllUI();
    }
}
