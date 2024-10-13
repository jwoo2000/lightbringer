using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GrenadeWeapon : Weapon
{
    // grenade weapons have a
    // - targetPosition to throw the grenades towards
    // - flightTime for how long the grenades take to land
    // - targetRadius, radius for detecting closest enemy to set targetPos to
    // - aoeSize, size of area to make on landing
    [SerializeField]
    protected Vector3 targetPosition;
    [SerializeField]
    protected float flightTime;
    [SerializeField]
    protected float targetRadius;
    [SerializeField]
    protected float aoeSize;
    [SerializeField] 
    protected float timeToAoe;
    [SerializeField]
    protected float damageCD;
    [SerializeField]
    protected float areaLifetime;


    [SerializeField]
    protected Vector3 nearestEnemyPos;

    // returns true if a target found
    protected virtual bool enemyInRange()
    {
        Collider[] collidersInRange = Physics.OverlapSphere(transform.position, targetRadius);
        float closestDist = Mathf.Infinity;

        foreach (Collider collider in collidersInRange)
        {
            if (collider.CompareTag("Enemy") && collider.isTrigger)
            {
                float dist = Vector3.Distance(transform.position, collider.transform.position);
                if (dist < closestDist)
                {
                    closestDist = dist;
                    nearestEnemyPos = collider.transform.position;
                }
            }
        }

        return (closestDist != Mathf.Infinity);
    }
}
