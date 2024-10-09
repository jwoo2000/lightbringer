using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Orbit : ProjWeapon
{
    [SerializeField]
    private int projCount = 1;

    // init values for Orbit
    public Orbit()
    {
        weaponName = "Celestial Orbit";
        speedLabel = "Orbit Speed";
        speedDesc = "Increases projectile orbit speed";
        uniqueLabel = "Projectile Count";
        uniqueDesc = "Increases number of projectiles orbiting";
        projSpeed = 1.0f;
        baseDamage = 20.0f;
        baseCooldown = 1.0f;
        cdReducPerSpeedLevel = 0.2f;
        dmgPerDmgLevel = 0.1f;
    }

    public override void Fire()
    {
        GameObject projInstance = Instantiate(weaponObject, playerTransform.position + weaponOriginOffset, Quaternion.identity);
        OrbitProjectile orbitProj = projInstance.GetComponent<OrbitProjectile>();
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
