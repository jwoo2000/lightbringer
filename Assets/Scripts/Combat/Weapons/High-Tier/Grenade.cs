using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Grenade : GrenadeWeapon
{
    // init values for Grenade
    public Grenade()
    {
        weaponTier = Weapon.Tier.High;
        weaponName = "Daybreak Charge";
        uniqueLabel = "AOE Size";
        uniqueDesc = "Increases impact area size";
        baseDamage = 20.0f;
        baseCooldown = 1.0f;
        cdReducPerSpeedLevel = 0.2f;
        dmgPerDmgLevel = 0.1f;

        flightTime = 1.0f;
    }

    public override void Fire()
    {
        GameObject grenadeInstance = Instantiate(weaponObject, playerTransform.position + weaponOriginOffset, Quaternion.identity);
        GrenadeGrenadeObject grenadeObject = grenadeInstance.GetComponent<GrenadeGrenadeObject>();
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
