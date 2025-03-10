using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Molotov : GrenadeWeapon
{
    // init values for Molotov
    private void Awake()
    {
        weaponTier = Weapon.Tier.High;
        weaponName = "Afterglow";
        uniqueLabel = "AOE Size";
        uniqueDesc = "Increases damaging field size";
        baseDamage = 50.0f;
        baseCooldown = 1.0f;
        cdReducPerSpeedLevel = 0.2f;
        dmgPerDmgLevel = 0.1f;

        flightTime = 1.0f;
        targetRadius = 10.0f;
        aoeSize = 5.0f;
        timeToAoe = 0.3f;

        areaLifetime = 2.0f;
        damageCD = 0.5f;
    }

    public override void Fire()
    {
        if (enemyInRange())
        {
            targetPosition = nearestEnemyPos;
            targetPosition.y = 0.0f;
        }
        else
        {
            targetPosition = playerTransform.position + (playerTransform.forward * 5.0f);
            targetPosition.y = 0.0f;
        }

        GameObject grenadeInstance = Instantiate(weaponObject, playerTransform.position + weaponOriginOffset, Quaternion.identity);
        MolotovGrenadeObject grenadeObject = grenadeInstance.GetComponent<MolotovGrenadeObject>();
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
