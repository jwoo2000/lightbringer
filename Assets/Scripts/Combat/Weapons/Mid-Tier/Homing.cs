using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Homing : ProjWeapon
{
    [SerializeField]
    private int projCount = 1;

    // init values for Orbit
    public Homing()
    {
        weaponTier = Weapon.Tier.Mid;
        weaponName = "Seeking Light";
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
        HomingProjectile homingProj = projInstance.GetComponent<HomingProjectile>();
        homingProj.dir = playerTransform.forward;
        homingProj.speed = projSpeed;
    }

    protected override void upgradeSpeed()
    {
        
    }

    protected override void upgradeUnique()
    {
        projCount++;
    }
}
