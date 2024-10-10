using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Aura : AreaWeapon
{
    // init values for Aura
    private void Awake()
    {
        weaponTier = Weapon.Tier.High;
        weaponName = "Blazing Radiance";
        speedLabel = "Damage frequency";
        speedDesc = "Increases aura damage frequency";
        uniqueLabel = "AOE Size";
        uniqueDesc = "Increases aura size";
        baseDamage = 20.0f;
        baseCooldown = 999.0f;
        cdReducPerSpeedLevel = 0.2f;
        dmgPerDmgLevel = 0.1f;

        // aura has a long duration around player
        areaDuration = 999.0f;
        damageCD = 1.0f;
    }

    public override void Fire()
    {
        GameObject areaInstance = Instantiate(weaponObject, playerTransform.position + weaponOriginOffset, Quaternion.identity, transform);
        AuraArea auraArea = areaInstance.GetComponent<AuraArea>();
        auraArea.damage = getDamage();
        auraArea.lifetime = areaDuration;
        auraArea.damageCD = damageCD;
    }

    protected override void upgradeSpeed()
    {
        
    }

    protected override void upgradeUnique()
    {

    }
}
