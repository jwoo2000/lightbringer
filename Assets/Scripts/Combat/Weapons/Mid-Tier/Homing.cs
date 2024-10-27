using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Homing : ProjWeapon
{
    [SerializeField]
    private int projCount;
    [SerializeField]
    private float spreadAngle;
    [SerializeField]
    private float homingRadius;
    [SerializeField]
    private float homingAccel;
    [SerializeField]
    private float maxProjSpeed;

    // init values for Orbit
    private void Awake()
    {
        weaponTier = Weapon.Tier.Mid;
        weaponName = "Seeking Light";
        uniqueLabel = "Projectile Count";
        uniqueDesc = "Increases number of projectiles fired";
        baseDamage = 100.0f;
        baseCooldown = 2.0f;
        cdReducPerSpeedLevel = 0.2f;
        dmgPerDmgLevel = 0.1f;

        projCount = 1;
        projSpeed = 5.0f;
        spreadAngle = 30.0f;
        homingRadius = 5.0f;
        homingAccel = 10.0f;
        maxProjSpeed = 30.0f;
    }

    public override void Fire()
    {
        float angleBwProj = 0.0f;
        float startingProjAngle = 0.0f;

        if (projCount > 1)
        {
            angleBwProj = spreadAngle;
            startingProjAngle = -(angleBwProj * (projCount - 1)) / 2.0f;

        }

        for (int i = 0; i < projCount; i++)
        {
            float currAngle = startingProjAngle + i * angleBwProj;
            Vector3 fireDir = Quaternion.Euler(0.0f, currAngle, 0.0f) * playerTransform.forward;

            GameObject projInstance = Instantiate(weaponObject, playerTransform.position + weaponOriginOffset, Quaternion.LookRotation(fireDir));
            HomingProjectile homingProj = projInstance.GetComponent<HomingProjectile>();
            homingProj.damage = getDamage();
            homingProj.dir = fireDir;
            homingProj.speed = projSpeed;
            homingProj.homingRadius = homingRadius;
            homingProj.homingAccel = homingAccel;
            homingProj.maxProjSpeed = maxProjSpeed;
        }
    }

    protected override void upgradeSpeed()
    {
        
    }

    protected override void upgradeDamage()
    {
        
    }

    protected override void upgradeUnique()
    {
        projCount++;
    }
}
