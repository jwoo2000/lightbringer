using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GrenadeObject : MonoBehaviour
{
    // basic grenade object class
    // upon instantiation, objects of this type are to land at a targetPosition, landing after flightTime,
    // then creating an instance of spawnOnImpact before destroying itself
    // lifetime = flightTime
    public GameObject spawnOnImpact;

    public Vector3 targetPosition;
    public float flightTime = 1.0f;

    public float damage;

    private float timeAlive = 0.0f;

    protected virtual void Update()
    {
        timeAlive += Time.deltaTime;
        if (timeAlive >= flightTime)
        {
            GameObject spawnedAreaDamage = Instantiate(spawnOnImpact, transform.position, Quaternion.identity);
            spawnedAreaDamage.GetComponent<AreaDamage>().damage = damage;
            
            Destroy(gameObject);
        }
    }
}
