using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Nova : ProjWeapon
{
    [SerializeField]
    private int projCount = 4;

    // init values for Nova
    public Nova()
    {
        weaponTier = Weapon.Tier.Mid;
        weaponName = "Lightburst";
        uniqueLabel = "Projectile Count";
        uniqueDesc = "Increases number of projectiles fired";
        projSpeed = 1.0f;
        baseDamage = 20.0f;
        baseCooldown = 1.0f;
        cdReducPerSpeedLevel = 0.2f;
        dmgPerDmgLevel = 0.1f;
    }

    public override void Fire()
    {
        GameObject projInstance = Instantiate(weaponObject, playerTransform.position + weaponOriginOffset, Quaternion.identity);
        NovaProjectile novaProj = projInstance.GetComponent<NovaProjectile>();
        novaProj.damage = getDamage();
        novaProj.dir = playerTransform.forward;
        novaProj.speed = projSpeed;
    }

    protected override void upgradeSpeed()
    {
        
    }

    protected override void upgradeUnique()
    {
        projCount++;
    }
}
