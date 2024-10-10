using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gun : ProjWeapon
{
    [SerializeField]
    private int projCount = 1;
    [SerializeField]
    private float spreadAngle = 10.0f; // angle between proj if projCount > 1
    // init values for Gun
    private void Awake()
    {
        weaponTier = Weapon.Tier.Low;
        weaponName = "Steadfast Bolt";
        uniqueLabel = "Projectile Count";
        uniqueDesc = "Increases number of projectiles fired";
        baseDamage = 20.0f;
        baseCooldown = 1.0f;
        cdReducPerSpeedLevel = 0.2f;
        dmgPerDmgLevel = 0.1f;

        projSpeed = 5.0f;
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

            GameObject projInstance = Instantiate(weaponObject, playerTransform.position + weaponOriginOffset, Quaternion.identity);
            GunProjectile gunProj = projInstance.GetComponent<GunProjectile>();
            gunProj.damage = getDamage();
            gunProj.dir = fireDir;
            gunProj.speed = projSpeed;
        }
    }

    protected override void upgradeSpeed()
    {
        
    }

    protected override void upgradeUnique()
    {
        projCount++;
    }
}
