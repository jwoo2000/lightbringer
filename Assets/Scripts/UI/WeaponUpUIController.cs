using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUpUIController : MonoBehaviour
{
    [SerializeField]
    private MenuController menuController;

    [SerializeField] private GameObject dmgChoicePanel;
    [SerializeField] private GameObject speedChoicePanel;
    [SerializeField] private GameObject uniqueChoicePanel;

    [SerializeField] private Weapon weaponToUpgrade;

    private Action choiceSelected;

    private void Awake()
    {
        choiceSelected = menuController.wepUpgradeChosen;
    }

    private void OnEnable()
    {
        if (weaponToUpgrade != null)
        {
            dmgChoicePanel.GetComponent<Button>().onClick.AddListener(() =>
            {
                weaponToUpgrade.Upgrade(Weapon.Stat.Damage);
                choiceSelected();
            });
            speedChoicePanel.GetComponent<Button>().onClick.AddListener(() =>
            {
                weaponToUpgrade.Upgrade(Weapon.Stat.Speed);
                choiceSelected();
            });
            uniqueChoicePanel.GetComponent<Button>().onClick.AddListener(() =>
            {
                weaponToUpgrade.Upgrade(Weapon.Stat.Unique);
                choiceSelected();
            });

        }
        else
        {
            Debug.LogWarning("WeaponUPUIController: No weapon");
        }
    }

    private void OnDisable()
    {
        dmgChoicePanel.GetComponent<Button>().onClick.RemoveAllListeners();
        speedChoicePanel.GetComponent<Button>().onClick.RemoveAllListeners();
        uniqueChoicePanel.GetComponent<Button>().onClick.RemoveAllListeners();
        weaponToUpgrade = null;
    }

    public void SetWeapon(Weapon weapon)
    {
        weaponToUpgrade = weapon;
    }
}
