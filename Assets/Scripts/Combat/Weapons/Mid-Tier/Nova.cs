using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Nova : ProjWeapon
{
    [SerializeField]
    private int projCount;

    // init values for Nova
    private void Awake()
    {
        weaponTier = Weapon.Tier.Mid;
        weaponName = "Lightburst";
        uniqueLabel = "Projectile Count";
        uniqueDesc = "Increases number of projectiles fired";
        baseDamage = 50.0f;
        baseCooldown = 1.0f;
        cdReducPerSpeedLevel = 0.2f;
        dmgPerDmgLevel = 0.1f;

        projCount = 4;
        projSpeed = 10.0f;
    }

    public override void Fire()
    {
        float angleStep = 360.0f / projCount;
        float currAngle = 0.0f;
        for (int i = 0; i < projCount; i++)
        {
            float projDirX = Mathf.Cos(currAngle * Mathf.Deg2Rad);
            float projDirZ = Mathf.Sin(currAngle * Mathf.Deg2Rad);
            Vector3 projDir = new Vector3(projDirX, 0.0f, projDirZ).normalized;
            projDir = playerTransform.rotation * projDir;

            GameObject projInstance = Instantiate(weaponObject, playerTransform.position + weaponOriginOffset, Quaternion.identity);
            NovaProjectile novaProj = projInstance.GetComponent<NovaProjectile>();
            novaProj.damage = getDamage();
            novaProj.dir = projDir;
            novaProj.speed = projSpeed;

            currAngle += angleStep;
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
        projCount += 4;
    }
}
