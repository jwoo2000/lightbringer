using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInfoPanelController : MonoBehaviour
{
    public WeaponInfoPanel lowPanel;
    public WeaponInfoPanel midPanel;
    public WeaponInfoPanel highPanel;

    public void setLowWep(Weapon weapon)
    {
        lowPanel.weapon = weapon;
        lowPanel.showWeapon();
    }
    public void setMidWep(Weapon weapon)
    {
        midPanel.weapon = weapon;
        midPanel.showWeapon();
    }
    public void setHighWep(Weapon weapon)
    {
        highPanel.weapon = weapon;
        highPanel.showWeapon();
    }

    public void updateAllUI()
    {
        lowPanel.updateUI();
        midPanel.updateUI();
        highPanel.updateUI();
    }
}
