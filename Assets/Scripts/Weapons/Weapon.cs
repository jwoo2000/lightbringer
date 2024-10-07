using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public int damageUpgradeLevel = 1;   // Damage upgrade level
    public int speedUpgradeLevel = 1;    // Speed upgrade level (affects fire rate, rotation speed, etc.)
    public int uniqueUpgradeLevel = 1;   // Unique stat upgrade level (based on the weapon type)

    public abstract void Update1();  // To be defined by specific weapons
    public abstract void Fire();     // To be defined by specific weapons

    // Method to upgrade the weapon's stats based on an external choice
    public void Upgrade(string statToUpgrade)
    {
        switch (statToUpgrade)
        {
            case "damage":
                damageUpgradeLevel++;
                Debug.Log($"{this.name}: Damage upgraded to level {damageUpgradeLevel}");
                break;
            case "speed":
                speedUpgradeLevel++;
                Debug.Log($"{this.name}: Speed upgraded to level {speedUpgradeLevel}");
                break;
            case "unique":
                uniqueUpgradeLevel++;
                Debug.Log($"{this.name}: Unique stat upgraded to level {uniqueUpgradeLevel}");
                break;
            default:
                Debug.LogWarning("Invalid stat choice for upgrade");
                break;
        }

        // Apply the effects of the upgrade
        ApplyUpgrades();
    }

    
    protected virtual void ApplyUpgrades()
    {
        // This can be implemented in each subclass based on how the upgrades affect that weapon
    }
}
