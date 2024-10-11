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

    [SerializeField]
    protected TrailRenderer trailRenderer;

    protected float timeAlive = 0.0f;

    protected virtual void Update()
    {
        moveProj();

        timeAlive += Time.deltaTime;
        if (timeAlive >= lifetime)
        {
            DestroyProj();
        }
    }

    protected virtual void moveProj()
    {
        if (dir != Vector3.zero)
        {
            transform.position += speed * Time.deltaTime * dir;
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && other.isTrigger)
        {
            other.gameObject.GetComponent<EnemyBehaviour>().TakeDamage(damage);
            DestroyProj();
        }
    }
    public virtual void DestroyProj()
    {
        trailRenderer.transform.parent = null;
        trailRenderer.autodestruct = true;
        Destroy(gameObject);
    }
}
