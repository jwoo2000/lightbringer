using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Swipe : AreaWeapon
{
    // init values for Swipe
    private void Awake()
    {
        weaponTier = Weapon.Tier.Mid;
        weaponName = "Radiant Sweep";
        speedLabel = "Attack Speed";
        speedDesc = "Increases attack speed";
        uniqueLabel = "AOE Size";
        uniqueDesc = "Increases swipe area";
        baseDamage = 60.0f;
        baseCooldown = 1.0f;
        cdReducPerSpeedLevel = 0.2f;
        dmgPerDmgLevel = 0.1f;

        // swipe has short duration (like a melee swipe) and damageCD longer than its area duration => only deals one tick of damage
        areaDuration = 0.2f;
        damageCD = 1.0f;
        aoeSize = 2.0f;
        timeToAoe = 0.1f;
    }

    public override void Fire()
    {
        GameObject areaInstance = Instantiate(weaponObject, playerTransform.position + weaponOriginOffset + (playerTransform.forward*(aoeSize/2.0f)), transform.rotation, transform);
        SwipeArea swipeArea = areaInstance.GetComponent<SwipeArea>();
        swipeArea.damage = getDamage();
        swipeArea.lifetime = areaDuration;
        swipeArea.damageCD = damageCD;
        swipeArea.aoeSize = aoeSize;
        swipeArea.timeToAoe = timeToAoe;
    }

    protected override void upgradeSpeed()
    {
        
    }

    protected override void upgradeUnique()
    {
        aoeSize++;
    }
}
