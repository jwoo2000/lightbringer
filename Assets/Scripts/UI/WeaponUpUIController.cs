using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUpUIController : MonoBehaviour
{
    [SerializeField]
    private MenuController menuController;

    [SerializeField] private GameObject dmgChoicePanel;
    [SerializeField] private TextMeshProUGUI dmgLabel;
    [SerializeField] private TextMeshProUGUI dmgDesc;
    [SerializeField] private GameObject speedChoicePanel;
    [SerializeField] private TextMeshProUGUI speedLabel;
    [SerializeField] private TextMeshProUGUI speedDesc;
    [SerializeField] private GameObject uniqueChoicePanel;
    [SerializeField] private TextMeshProUGUI uniqueLabel;
    [SerializeField] private TextMeshProUGUI uniqueDesc;

    [SerializeField] private Weapon weaponToUpgrade;
    [SerializeField] private TextMeshProUGUI weaponName;
    [SerializeField] private Image weaponImage;

    private Action choiceSelected;

    private void Awake()
    {
        choiceSelected = menuController.wepUpgradeChosen;
    }

    private void OnEnable()
    {
        if (weaponToUpgrade != null)
        {
            weaponName.text = weaponToUpgrade.weaponName;
            weaponImage.sprite = weaponToUpgrade.weaponImage;

            dmgChoicePanel.GetComponent<Button>().onClick.AddListener(() =>
            {
                weaponToUpgrade.Upgrade(Weapon.Stat.Damage);
                choiceSelected();
            });
            dmgLabel.text = weaponToUpgrade.damageLabel;
            dmgDesc.text = weaponToUpgrade.damageDesc;

            speedChoicePanel.GetComponent<Button>().onClick.AddListener(() =>
            {
                weaponToUpgrade.Upgrade(Weapon.Stat.Speed);
                choiceSelected();
            });
            speedLabel.text = weaponToUpgrade.speedLabel;
            speedDesc.text = weaponToUpgrade.speedDesc;

            uniqueChoicePanel.GetComponent<Button>().onClick.AddListener(() =>
            {
                weaponToUpgrade.Upgrade(Weapon.Stat.Unique);
                choiceSelected();
            });
            uniqueLabel.text = weaponToUpgrade.uniqueLabel;
            uniqueDesc.text = weaponToUpgrade.uniqueDesc;
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
