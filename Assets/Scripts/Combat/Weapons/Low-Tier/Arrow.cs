using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Arrow : ProjWeapon
{
    [SerializeField]
    private int pierceCount = 1;

    // init values for Arrow
    public Arrow()
    {
        weaponTier = Weapon.Tier.Low;
        weaponName = "Lightlance";
        uniqueLabel = "Pierce Count";
        uniqueDesc = "Increases number of enemies pierced";
        baseDamage = 20.0f;
        baseCooldown = 1.0f;
        cdReducPerSpeedLevel = 0.2f;
        dmgPerDmgLevel = 0.1f;

        projSpeed = 1.0f;
    }

    public override void Fire()
    {
        GameObject projInstance = Instantiate(weaponObject, playerTransform.position + weaponOriginOffset, Quaternion.identity);
        ArrowProjectile arrowProj = projInstance.GetComponent<ArrowProjectile>();
        arrowProj.damage = getDamage();
        arrowProj.dir = playerTransform.forward;
        arrowProj.speed = projSpeed;
    }

    protected override void upgradeSpeed()
    {
        
    }

    protected override void upgradeUnique()
    {
        pierceCount++;
    }
}
