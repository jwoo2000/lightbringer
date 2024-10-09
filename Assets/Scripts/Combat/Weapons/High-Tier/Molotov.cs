using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Molotov : GrenadeWeapon
{
    // init values for Molotov
    public Molotov()
    {
        weaponTier = Weapon.Tier.High;
        weaponName = "Afterglow";
        uniqueLabel = "AOE Size";
        uniqueDesc = "Increases damaging field size";
        baseDamage = 20.0f;
        baseCooldown = 1.0f;
        cdReducPerSpeedLevel = 0.2f;
        dmgPerDmgLevel = 0.1f;

        flightTime = 1.0f;
    }

    public override void Fire()
    {
        GameObject grenadeInstance = Instantiate(weaponObject, playerTransform.position + weaponOriginOffset, Quaternion.identity);
        MolotovGrenadeObject grenadeObject = grenadeInstance.GetComponent<MolotovGrenadeObject>();
        grenadeObject.targetPosition = targetPosition;
        grenadeObject.flightTime = flightTime;
    }

    protected override void upgradeSpeed()
    {
        
    }

    protected override void upgradeUnique()
    {
        
    }
}
