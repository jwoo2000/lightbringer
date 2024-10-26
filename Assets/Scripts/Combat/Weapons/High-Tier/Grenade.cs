using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Grenade : GrenadeWeapon
{
    // init values for Grenade
    private void Awake()
    {
        weaponTier = Weapon.Tier.High;
        weaponName = "Daybreak Charge";
        uniqueLabel = "AOE Size";
        uniqueDesc = "Increases impact area size";
        baseDamage = 100.0f;
        baseCooldown = 1.0f;
        cdReducPerSpeedLevel = 0.2f;
        dmgPerDmgLevel = 0.1f;

        flightTime = 1.0f;
        targetRadius = 10.0f;
        aoeSize = 5.0f;
        timeToAoe = 0.2f;

        damageCD = 1.0f;
        areaLifetime = 0.1f;
    }

    public override void Fire()
    {
        if (enemyInRange())
        {
            targetPosition = nearestEnemyPos;
            targetPosition.y = 0.0f;
        } else
        {
            targetPosition = playerTransform.position + (playerTransform.forward * 5.0f);
            targetPosition.y = 0.0f;
        }

        GameObject grenadeInstance = Instantiate(weaponObject, playerTransform.position + weaponOriginOffset, Quaternion.identity);
        GrenadeGrenadeObject grenadeObject = grenadeInstance.GetComponent<GrenadeGrenadeObject>();
        grenadeObject.damage = getDamage();
        grenadeObject.targetPosition = targetPosition;
        grenadeObject.flightTime = flightTime;
        grenadeObject.aoeSize = aoeSize;
        grenadeObject.damageCD = damageCD;
        grenadeObject.areaLifetime = areaLifetime;
        grenadeObject.timeToAoe = timeToAoe;
    }

    protected override void upgradeDamage()
    {
        
    }

    protected override void upgradeSpeed()
    {
        
    }

    protected override void upgradeUnique()
    {
        aoeSize++;
    }
}
