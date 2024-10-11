using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Aura : AreaWeapon
{
    [SerializeField]
    private float aoeSize;
    [SerializeField]
    private float baseAoe;

    [SerializeField]
    private float baseDmgCD;

    [SerializeField]
    private AuraArea activeAuraArea;
    [SerializeField]
    private GameObject activeAuraAreaInstance;

    // init values for Aura
    private void Awake()
    {
        weaponTier = Weapon.Tier.High;
        weaponName = "Blazing Radiance";
        speedLabel = "Damage Speed";
        speedDesc = "Increases aura damage frequency";
        uniqueLabel = "AOE Size";
        uniqueDesc = "Increases aura size";
        baseDamage = 20.0f;
        baseCooldown = 999.0f;
        cdReducPerSpeedLevel = 0.2f;
        dmgPerDmgLevel = 0.1f;

        // aura has a long duration around player
        areaDuration = 999.0f;
        damageCD = 1.0f;
        baseDmgCD = damageCD;
        aoeSize = 5.0f;
        baseAoe = aoeSize;
    }

    public override void Fire()
    {
        activeAuraAreaInstance = Instantiate(weaponObject, playerTransform.position + weaponOriginOffset, Quaternion.identity, transform);
        activeAuraAreaInstance.transform.localScale = Vector3.one * aoeSize;
        activeAuraArea = activeAuraAreaInstance.GetComponent<AuraArea>();
        activeAuraArea.damage = getDamage();
        activeAuraArea.lifetime = areaDuration;
        activeAuraArea.damageCD = damageCD;
    }

    protected override void upgradeSpeed()
    {
        damageCD = (baseDmgCD * (1 - (speedLevel * 0.15f)));
        activeAuraArea.damageCD = damageCD;
    }

    protected override void upgradeUnique()
    {
        aoeSize = (baseAoe * (1 + (uniqueLevel * 0.3f)));
        activeAuraAreaInstance.transform.localScale = Vector3.one * aoeSize;
    }
}
