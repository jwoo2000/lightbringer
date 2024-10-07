using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInfoPanel : MonoBehaviour
{
    [SerializeField]
    public Weapon weapon;

    [SerializeField]
    public Image weaponImage;

    [SerializeField]
    public TextMeshProUGUI weaponNameText;

    [SerializeField]
    public TextMeshProUGUI damageLevelText;

    [SerializeField]
    public TextMeshProUGUI speedLevelText;

    [SerializeField]
    public TextMeshProUGUI uniqueLevelText;

    public bool weaponShowing = false;

    private void Awake()
    {
        noWeapon();
    }

    public void updateUI()
    {
        if (weaponShowing)
        {
            if (weapon != null)
            {
                weaponImage.sprite = weapon.weaponImage;
                weaponNameText.text = weapon.weaponName;
                damageLevelText.text = weapon.damageLabel + " Lv." + weapon.damageLevel;
                speedLevelText.text = weapon.speedLabel + " Lv." + weapon.speedLevel;
                uniqueLevelText.text = weapon.uniqueLabel + " Lv." + weapon.uniqueLevel;
            } else
            {
                Debug.LogWarning("no weapon to display info for!");
            }
        }
    }

    public void showWeapon()
    {
        weaponShowing = true;
        weaponImage.enabled = true;

        damageLevelText.enabled = true;
        speedLevelText.enabled = true;
        uniqueLevelText.enabled = true;
        updateUI();
    }

    public void noWeapon()
    {
        weaponShowing = false;
        weaponImage.enabled = false;
        weaponNameText.text = "No weapon";
        damageLevelText.enabled = false;
        speedLevelText.enabled = false;
        uniqueLevelText.enabled = false;
    }
}
