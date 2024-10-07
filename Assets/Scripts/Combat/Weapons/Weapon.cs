﻿using UnityEditor.PackageManager.Requests;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public enum Stat { Damage, Speed, Unique }
    public enum Tier { Low, Mid, High }

    [SerializeField]
    public Tier weaponTier;

    [SerializeField] // serialised for debugging dont manually assign these values
    public int damageLevel = 0;   // Damage upgrade level
    public int speedLevel = 0;    // Speed upgrade level (affects a speed related property: fire rate, rotation speed, etc.)
    public int uniqueLevel = 0;   // Unique stat upgrade level (based on the weapon type)

    [SerializeField]
    public Transform playerTransform;
    [SerializeField]
    protected Vector3 weaponOriginOffset = new Vector3(0.0f, 1.0f, 0.0f);
    [SerializeField]
    protected GameObject projectile; // prefab to fire

    [SerializeField]
    protected float baseDamage;
    [SerializeField]
    protected float baseCooldown = 1.0f; // cooldown in seconds, default: weapon fires once a second
    private float timeToNextFire = 0.0f;

    [SerializeField]
    protected float cdReducPerSpeedLevel = 0.2f; // cooldown reduction per speed upgrade, default: reduce cooldown by 20% per level (additive)
    [SerializeField]
    protected float dmgPerDmgLevel = 0.1f; // dmg scaling per damage upgrade, default: increase damage by 10% per level (additive)


    private void Update()
    {
        tickCooldown();
    }

    private void tickCooldown()
    {
        if (timeToNextFire > 0.0f)
        {
            timeToNextFire -= Time.deltaTime;
        } else
        {
            Fire();
            timeToNextFire = GetCooldownTime();
        }
    }

    protected virtual float GetCooldownTime()
    {
        float cooldown = baseCooldown / (1 + (speedLevel * cdReducPerSpeedLevel));
        return Mathf.Max(cooldown, 0.1f);
    }

    public float getDamage()
    {
        return (baseDamage * (1 + (damageLevel * dmgPerDmgLevel)));
    }

    public abstract void Fire();

    public void Upgrade(Stat statToUpgrade)
    {
        switch (statToUpgrade)
        {
            case Stat.Damage:
                damageLevel++;
                Debug.Log($"{this.name}: Damage upgraded to level {damageLevel}");
                break;
            case Stat.Speed:
                speedLevel++;
                upgradeSpeed();
                Debug.Log($"{this.name}: Speed upgraded to level {speedLevel}");
                break;
            case Stat.Unique:
                uniqueLevel++;
                upgradeUnique();
                Debug.Log($"{this.name}: Unique stat upgraded to level {uniqueLevel}");
                break;
            default:
                Debug.LogWarning("Invalid stat choice for upgrade");
                break;
        }
    }
    protected abstract void upgradeSpeed();
    protected abstract void upgradeUnique();
}
