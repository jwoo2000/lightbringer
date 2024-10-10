using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Orbit : ProjWeapon
{
    [SerializeField]
    private int projCount = 1;

    // init values for Orbit
    private void Awake()
    {
        weaponTier = Weapon.Tier.Low;
        weaponName = "Celestial Orbit";
        speedLabel = "Orbit Speed";
        speedDesc = "Increases projectile orbit speed";
        uniqueLabel = "Projectile Count";
        uniqueDesc = "Increases number of projectiles orbiting";
        baseDamage = 20.0f;
        baseCooldown = 1.0f;
        cdReducPerSpeedLevel = 0.2f;
        dmgPerDmgLevel = 0.1f;

        projSpeed = 1.0f;
    }

    public override void Fire()
    {
        GameObject projInstance = Instantiate(weaponObject, playerTransform.position + weaponOriginOffset, Quaternion.identity);
        OrbitProjectile orbitProj = projInstance.GetComponent<OrbitProjectile>();
        orbitProj.damage = getDamage();
        orbitProj.dir = playerTransform.forward;
        orbitProj.speed = projSpeed;
    }

    protected override void upgradeSpeed()
    {
        
    }

    protected override void upgradeUnique()
    {
        projCount++;
    }
}
