using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinWeaponInfoPanelController : WeaponInfoPanelController
{
    [SerializeField] private WeaponInfoPanelController realController;

    public void setWeapons()
    {
        if (realController.lowPanel.weapon)
        {
            setLowWep(realController.lowPanel.weapon);
        }
        if (realController.midPanel.weapon) 
        {
            setMidWep(realController.midPanel.weapon);
        }
        if (realController.highPanel.weapon) 
        {
            setHighWep(realController.highPanel.weapon);
        }
    }
}
