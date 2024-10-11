using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitProjectile : Projectile
{
    // orbit projectile
    protected override void moveProj()
    {
        transform.RotateAround(transform.parent.position, Vector3.up, speed * Time.deltaTime);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && other.isTrigger)
        {
            other.gameObject.GetComponent<EnemyBehaviour>().TakeDamage(damage);
        }
    }
}
