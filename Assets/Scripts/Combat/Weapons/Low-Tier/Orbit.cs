using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Orbit : ProjWeapon
{
    [SerializeField]
    private int projCount = 1;
    [SerializeField]
    private float orbitRadius = 2.0f;
    [SerializeField]
    private float baseProjSpeed;

    [SerializeField]
    private List<GameObject> projList = new List<GameObject>();

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
        baseCooldown = 999.0f;
        cdReducPerSpeedLevel = 0.2f;
        dmgPerDmgLevel = 0.1f;

        projSpeed = 180.0f;
        baseProjSpeed = projSpeed;

    }

    public override void Fire()
    {
        float angleStep = 360.0f / projCount;
        float currAngle = 0.0f;
        for (int i = 0; i < projCount; i++)
        {
            float spawnX = playerTransform.position.x + Mathf.Cos(Mathf.Deg2Rad * currAngle) * orbitRadius;
            float spawnZ = playerTransform.position.z + Mathf.Sin(Mathf.Deg2Rad * currAngle) * orbitRadius;
            Vector3 projSpawnPos = new Vector3(spawnX, playerTransform.position.y + weaponOriginOffset.y, spawnZ);
            
            GameObject projInstance = Instantiate(weaponObject, projSpawnPos, Quaternion.identity, playerTransform);
            projList.Add(projInstance);

            OrbitProjectile orbitProj = projInstance.GetComponent<OrbitProjectile>();
            orbitProj.damage = getDamage();
            orbitProj.dir = playerTransform.forward;
            orbitProj.speed = projSpeed;
            orbitProj.lifetime = baseCooldown;

            currAngle += angleStep;
        }
    }
    protected override float GetCooldownTime()
    {
        // orbit speed upgrade affects projspeed not fire cd
        return baseCooldown;
    }

    protected override void upgradeSpeed()
    {
        projSpeed = (baseProjSpeed * (1 + (speedLevel * 20.0f)));
    }

    protected override void upgradeUnique()
    {
        projCount++;
        foreach (GameObject proj in projList)
        {
            Destroy(proj);
        }
        Fire();
    }
}
