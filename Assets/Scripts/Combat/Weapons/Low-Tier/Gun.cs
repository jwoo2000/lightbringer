using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gun : Weapon
{
    private float projSpeed = 1.0f;

    // init values for Gun
    public Gun()
    {
        baseDamage = 20.0f;
        baseCooldown = 1.0f;
        cdReducPerSpeedLevel = 0.2f;
        dmgPerDmgLevel = 0.1f;
    }

    public override void Fire()
    {
        GameObject projInstance = Instantiate(projectile, playerTransform.position + weaponOriginOffset, Quaternion.identity);
        GunProjectile gunProj = projInstance.GetComponent<GunProjectile>();
        gunProj.dir = playerTransform.forward;
        gunProj.speed = projSpeed;
    }

    protected override void upgradeSpeed()
    {
        
    }

    protected override void upgradeUnique()
    {
        
    }
}
