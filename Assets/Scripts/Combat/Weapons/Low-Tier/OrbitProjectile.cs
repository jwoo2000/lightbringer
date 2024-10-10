using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitProjectile : Projectile
{
    // orbit projectile
    protected override void Update()
    {
        transform.RotateAround(transform.parent.position, Vector3.up, speed * Time.deltaTime);

        timeAlive += Time.deltaTime;
        if (timeAlive >= lifetime)
        {
            Destroy(gameObject);
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && other.isTrigger)
        {
            other.gameObject.GetComponent<EnemyBehaviour>().TakeDamage(damage);
        }
    }
}
