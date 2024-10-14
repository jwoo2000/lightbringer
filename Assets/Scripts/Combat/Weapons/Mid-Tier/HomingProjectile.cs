using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingProjectile : Projectile
{
    // homing projectile
    public float homingRadius;
    public float homingAccel;
    public float maxProjSpeed;
    [SerializeField]
    private Transform targetEnemy;
    [SerializeField]
    private bool homing;

    private void Awake()
    {
        homing = false;
    }

    protected override void Update()
    {
        base.Update(); // contains move forward and proj lifetime destroy logic

        if (Time.timeScale != 0.0f)
        {
            if (!homing)
            {
                if (checkForTarget())
                {
                    homing = true;
                }
            } else
            {
                if (targetEnemy != null)
                {
                    Vector3 dirToTarget = (targetEnemy.position - transform.position).normalized;
                    dirToTarget.y = 0;
                    dir = Vector3.Lerp(dir, dirToTarget, Time.deltaTime * homingAccel).normalized; // smooth dir change, dont snap to enemy
                    dir.y = 0;
                    transform.rotation = Quaternion.LookRotation(dir);
                    speed = Mathf.Min(speed + homingAccel * Time.deltaTime, maxProjSpeed);
                } else
                {
                    homing = false;
                }
            }
        }
    }

    // returns true if a target found
    private bool checkForTarget()
    {
        Collider[] collidersInRange = Physics.OverlapSphere(transform.position, homingRadius);
        float closestDist = Mathf.Infinity;

        targetEnemy = null;
        foreach (Collider collider in collidersInRange)
        {
            if (collider.CompareTag("Enemy") && collider.isTrigger)
            {
                float dist = Vector3.Distance(transform.position, collider.transform.position);
                if (dist < closestDist)
                {
                    closestDist = dist;
                    targetEnemy = collider.transform;
                }
            }
        }

        return (targetEnemy != null);
    }
}
