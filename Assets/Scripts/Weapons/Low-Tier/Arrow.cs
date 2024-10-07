using UnityEngine;

public class Arrow : Weapon
{
    public GameObject attackObject;  // The attack object (e.g., a projectile)
    public float minimumDistanceToShoot = 10.0f; // Minimum distance to consider for shooting
    private float nextFireTime = 0f;
    private float baseFireRate = 1.0f;  // Base fire rate (before applying speedUpgradeLevel)

    public override void Update1()
    {
        // Calculate the fire rate based on speedUpgradeLevel
        float calculatedFireRate = baseFireRate / speedUpgradeLevel;

        // Auto-fire based on the speedUpgradeLevel
        if (Time.time >= nextFireTime)
        {
            Fire();
            nextFireTime = Time.time + calculatedFireRate;  // Adjust fire rate dynamically
        }
    }

    public override void Fire()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        // Raycast from the mouse position to find where the player is aiming
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // If the ray hits something
        if (Physics.Raycast(ray, out hit))
        {
            // Check if the hit point is far enough from the player
            if (Vector3.Distance(player.transform.position, hit.point) >= minimumDistanceToShoot)
            {
                ShootProjectile(player, hit.point);
            }
            else
            {
                // If the hit point is too close, shoot forward instead
                Vector3 forwardPoint = player.transform.position + player.transform.forward * 1000f; // Far forward point
                ShootProjectile(player, forwardPoint);
                Debug.Log("Raycast hit too close, shooting forward instead.");
            }
        }
        else
        {
            // If the raycast doesn't hit anything, shoot forward
            Vector3 forwardPoint = player.transform.position + player.transform.forward * 1000f; // Far forward point
            ShootProjectile(player, forwardPoint);
            Debug.Log("Raycast did not hit, shooting forward.");
        }
    }

    // Method to handle projectile creation and shooting
    private void ShootProjectile(GameObject player, Vector3 targetPoint)
    {
        // Create the attackObject at the player's attack position
        GameObject projectile = Instantiate(attackObject, AttackPosition(player), Quaternion.identity);

        // Initialize the projectile to move toward the target position
        PelletController pelletController = projectile.GetComponent<PelletController>();
        if (pelletController != null)
        {
            pelletController.Initialize(PelletController.WeaponType.Arrow, null, targetPoint, damageUpgradeLevel, speedUpgradeLevel, uniqueUpgradeLevel);  // Pass uniqueUpgradeLevel as pierce count
        }

        Debug.Log("Arrow fired projectile towards: " + targetPoint);
    }

    // Helper method to calculate the attack position
    private Vector3 AttackPosition(GameObject player)
    {
        Vector3 position = player.transform.position;
        position += player.transform.forward * 1.5f;  // Adjust this multiplier to control distance
        position.y = 1;  // Set the height
        return position;
    }
}
