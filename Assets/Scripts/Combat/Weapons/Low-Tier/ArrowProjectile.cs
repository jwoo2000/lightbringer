using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProjectile : Projectile
{
    // arrow projectile
    public int pierceCount;
    protected override void OnTriggerEnter(Collider other)
    {
        if (other != null && other.CompareTag("Enemy") && other.isTrigger)
        {
            pierceCount--;
            EnemyBehaviour enemy = other.GetComponent<EnemyBehaviour>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            if (pierceCount < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
