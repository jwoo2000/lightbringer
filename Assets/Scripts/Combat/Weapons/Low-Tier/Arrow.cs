using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Arrow : ProjWeapon
{
    [SerializeField]
    private int pierceCount;

    // init values for Arrow
    private void Awake()
    {
        weaponTier = Weapon.Tier.Low;
        weaponName = "Lightlance";
        uniqueLabel = "Pierce Count";
        uniqueDesc = "Increases number of enemies pierced";
        baseDamage = 50.0f;
        baseCooldown = 1.0f;
        cdReducPerSpeedLevel = 0.2f;
        dmgPerDmgLevel = 0.1f;

        pierceCount = 2;
        projSpeed = 15.0f;
    }

    public override void Fire()
    {
        GameObject projInstance = Instantiate(weaponObject, playerTransform.position + weaponOriginOffset, playerTransform.rotation * Quaternion.Euler(90, 0, 0));
        ArrowProjectile arrowProj = projInstance.GetComponent<ArrowProjectile>();
        arrowProj.damage = getDamage();
        arrowProj.dir = playerTransform.forward;
        arrowProj.speed = projSpeed;
        arrowProj.pierceCount = pierceCount;
    }

    protected override void upgradeSpeed()
    {
        
    }

    protected override void upgradeUnique()
    {
        pierceCount++;
    }
}
