using UnityEngine;

public class Orbit : Weapon
{
    public GameObject attackObject;
    private GameObject[] projectiles;
    private bool projectilesCreated = false;
    private int projectileCount;

    public float rotationSpeed = 50f;  // Base rotation speed

    public override void Update1()
    {
        if (!projectilesCreated)
        {
            UpdateProjectileCount();
            CreateProjectiles();
        }

        RotateProjectiles();
    }

    // Update the number of projectiles based on the uniqueUpgradeLevel
    private void UpdateProjectileCount()
    {
        projectileCount = uniqueUpgradeLevel;  // Level 1: 1 projectile, Level 2: 2 projectiles, etc.
        projectiles = new GameObject[projectileCount];
    }

    private void CreateProjectiles()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        for (int i = 0; i < projectileCount; i++)
        {
            GameObject projectile = Instantiate(attackObject, AttackPosition(player, i), Quaternion.identity);

            projectile.transform.parent = player.transform;

            PelletController pelletController = projectile.GetComponent<PelletController>();
            if (pelletController != null)
            {
                pelletController.Initialize(PelletController.WeaponType.Orbit);
            }

            projectiles[i] = projectile;

            Debug.Log("Orbit created rotating projectile " + i + " at position: " + projectile.transform.position);
        }

        projectilesCreated = true;
    }

    private void RotateProjectiles()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        for (int i = 0; i < projectileCount; i++)
        {
            if (projectiles[i] != null)
            {
                float angle = rotationSpeed * speedUpgradeLevel * Time.deltaTime;
                projectiles[i].transform.RotateAround(player.transform.position, Vector3.up, angle);
            }
        }
    }

    private Vector3 AttackPosition(GameObject player, int index)
    {
        float angle = (360f / projectileCount) * index;
        Vector3 position = player.transform.position + Quaternion.Euler(0, angle, 0) * Vector3.right * 2f;
        position.y = 1;
        return position;
    }

    public override void Fire()
    {
        Debug.Log("Orbit: No firing needed as projectiles are always rotating.");
    }

    public void DestroyProjectiles()
    {
        for (int i = 0; i < projectileCount; i++)
        {
            if (projectiles[i] != null)
            {
                Destroy(projectiles[i]);
            }
        }
        projectilesCreated = false;
    }

    // Apply upgrades based on the levels
    protected override void ApplyUpgrades()
    {
        rotationSpeed = 50f + (10f * speedUpgradeLevel);  // Increase rotation speed
        UpdateProjectileCount();
        DestroyProjectiles();  // Recreate projectiles based on new upgrade levels
        CreateProjectiles();
    }
}
