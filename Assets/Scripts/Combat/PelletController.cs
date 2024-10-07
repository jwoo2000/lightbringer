using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelletController : MonoBehaviour
{
    // Public attributes used to configure the projectile behavior
    [SerializeField] private ParticleSystem collisionParticles; // Particle effect on collision
    [SerializeField] private int damageAmount = 50; // Amount of damage dealt
    [SerializeField] private float projectileSpeed = 10000f; // Speed of the projectile
    [SerializeField] private float timeLeftAlive = 0.3f; // Lifetime of the projectile for Gun and Arrow

    private GameObject target; // Target enemy for Gun
    private Vector3 targetPosition; // Target position for Arrow
    private GameObject player; // Player's position for Orbit
    private WeaponType weaponType; // Current weapon type for the projectile
    private int pierceCount;

    // Enum for weapon types
    public enum WeaponType
    {
        Gun,   // Gun: Tracks the nearest enemy
        Orbit, // Orbit: Rotates around the player
        Arrow // Arrow: Moves toward a designated position
    }

    // Initialization method to set the projectile behavior based on weapon type
    public void Initialize(WeaponType type, GameObject targetEnemy = null, Vector3 targetPos = default(Vector3), int damageLevel = 1, int speedLevel = 1, int pierceLevel = 0)
    {
        weaponType = type;
        pierceCount = pierceLevel;

        if (type == WeaponType.Gun)
        {
            target = targetEnemy; // Gun: Set the target enemy
        }
        else if (type == WeaponType.Arrow)
        {
            targetPosition = targetPos; // Arrow: Set the target position
        } 

        // Orbit needs the player object as the center for rotation
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // Only check the lifetime for Gun and Arrow
        if (weaponType != WeaponType.Orbit)
        {
            if (timeLeftAlive <= 0)
            {
                Destroy(gameObject);  // Destroy the projectile when its lifetime expires
                return;
            }
            else
            {
                timeLeftAlive -= Time.deltaTime;
            }
        }

        MoveProjectile(); // Call the movement logic
    }

    // Moves the projectile based on the weapon type
    private void MoveProjectile()
    {
        if (weaponType == WeaponType.Gun)
        {
            // Gun: Move forward in the specified direction
            Vector3 direction = (targetPosition - transform.position).normalized;
            transform.position += direction * projectileSpeed * Time.deltaTime;
        }
        else if (weaponType == WeaponType.Orbit && player != null)
        {
            // Orbit: Rotate around the player indefinitely
            float angle = projectileSpeed;
            transform.RotateAround(player.transform.position, Vector3.up, angle);
        }
        else if (weaponType == WeaponType.Arrow)
        {
            // Arrow: Move toward a designated position
            Vector3 direction = (targetPosition - transform.position).normalized;
            transform.position += direction * projectileSpeed * Time.deltaTime;

            // Destroy the projectile if it gets close to the target position
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                Destroy(gameObject);
            }
        }
    }


    // Collision logic
    private void OnTriggerEnter(Collider col)
    {
        // Check if the collided object has the correct tag (e.g., "Enemy")
        if (col.gameObject.CompareTag("Enemy"))
        {
            // Apply damage to the enemy
            EnemyBehaviour enemy = col.gameObject.GetComponent<EnemyBehaviour>();
            if (enemy != null)
            {
                enemy.TakeDamage(damageAmount);
            }

            // Play collision particles if they are set
            if (collisionParticles != null)
            {
                ParticleSystem particles = Instantiate(collisionParticles, transform.position, Quaternion.identity);
                particles.Play();
                Destroy(particles.gameObject, particles.main.duration); // Destroy the particle system after playing
            }

            // Handle Arrow's pierce count
            if (weaponType == WeaponType.Arrow)
            {
                pierceCount--; // Decrease pierce count after hitting an enemy
                Debug.Log($"Arrow pierced an enemy. Remaining pierce count: {pierceCount}");

                // Destroy the projectile if it has hit the maximum number of enemies
                if (pierceCount <= 0)
                {
                    Debug.Log("Arrow reached pierce limit. Destroying projectile.");
                    Destroy(gameObject);
                }
            }

            // Handle Gun behavior
            else if (weaponType == WeaponType.Gun)
            {
                Debug.Log("Gun projectile hit an enemy. Destroying projectile.");
                Destroy(gameObject); // Gun hits and destroys instantly
            }
        }
    }

}
