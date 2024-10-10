using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    // basic projectile class
    // upon instantiation, objects of this type just move forward in the given direction at a speed until it dies (default after 5s)
    public Vector3 dir = Vector3.zero;
    public float speed = 1.0f;
    public float lifetime = 5.0f;

    public float damage;

    protected float timeAlive = 0.0f;

    protected virtual void Update()
    {
        if (dir != Vector3.zero)
        {
            transform.position += speed * Time.deltaTime * dir;
        }

        timeAlive += Time.deltaTime;
        if (timeAlive >= lifetime)
        {
            Destroy(gameObject);
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && other.isTrigger)
        {
            other.gameObject.GetComponent<EnemyBehaviour>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
