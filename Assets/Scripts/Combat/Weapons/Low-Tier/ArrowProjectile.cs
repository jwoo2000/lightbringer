using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProjectile : Projectile
{
    // arrow projectile
    public int pierceCount;
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && other.isTrigger)
        {
            pierceCount--;
            other.gameObject.GetComponent<EnemyBehaviour>().TakeDamage(damage);
            if (pierceCount < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
