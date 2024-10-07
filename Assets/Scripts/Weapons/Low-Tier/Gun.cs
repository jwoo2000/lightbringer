using UnityEngine;

public class Gun : Weapon
{
    public GameObject attackObject;  // The attack object (e.g., a projectile)
    public float fireRate = 2.0f;    // Fire every 2 seconds
    public float baseFireRate = 2.0f;
    private float nextFireTime = 0f;

    public override void Update1()
    {
        float fireRate = baseFireRate / speedUpgradeLevel;
        // Fire automatically when the cooldown is over
        if (Time.time >= nextFireTime)
        {
            Fire();  // Fire projectiles in a spread pattern
            nextFireTime = Time.time + fireRate;  // Adjust fire rate dynamically based on upgrades
        }
    }

    public override void Fire()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        // The base angle for spread shots
        float spreadAngle = 15f;  // Adjust the spread angle as needed

        // Calculate the number of projectiles to fire based on uniqueUpgradeLevel
        int projectileCount = 1 + 2 * uniqueUpgradeLevel;  // Level 1 = 1 shot, Level 2 = 3 shots, Level 3 = 5 shots

        // Fire projectiles in a spread pattern
        for (int i = 0; i < projectileCount; i++)
        {
            // Calculate the angle offset for each projectile relative to the player
            float angleOffset = (i - (projectileCount - 1) / 2f) * spreadAngle;

            // Create the projectile at the player's attack position
            GameObject projectile = Instantiate(attackObject, AttackPosition(player, i), Quaternion.identity);

            // Calculate the direction of the spread based on the player's facing direction
            Vector3 spreadDirection = Quaternion.Euler(0, angleOffset, 0) * player.transform.forward;

            // Initialize the projectile with the calculated spread direction
            PelletController pelletController = projectile.GetComponent<PelletController>();
            if (pelletController != null)
            {
                pelletController.Initialize(
                    PelletController.WeaponType.Gun,
                    null,
                    player.transform.position + spreadDirection * 1000f,  // Set the direction based on spread
                    damageUpgradeLevel,
                    speedUpgradeLevel,
                    uniqueUpgradeLevel
                );
            }

            Debug.Log($"Gun fired projectile {i + 1} with spread at angle: {angleOffset}");
        }
    }



    private Vector3 AttackPosition(GameObject player, int i)
    {
        Vector3 position = player.transform.position;
        position += player.transform.forward * 3.0f;  // Adjust this multiplier to control distance
        position += player.transform.right * (i * 0.5f);  // Add a small horizontal offset based on the index
        position.y = 1;  // Set the height
        return position;
    }

}
