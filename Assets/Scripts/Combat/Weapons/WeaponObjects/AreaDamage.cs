using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AreaDamage : MonoBehaviour
{
    // basic area damage class
    // upon instantiation, objects of this type remain stationary until it dies (default after 2s)
    // they deal damage every damageCD (default 1s)
    public float lifetime = 2.0f;
    public float damageCD = 1.0f;

    public float damage;

    private float timeAlive = 0.0f;

    protected virtual void Update()
    {
        timeAlive += Time.deltaTime;
        if (timeAlive >= lifetime)
        {
            Destroy(gameObject);
        }
    }
}
