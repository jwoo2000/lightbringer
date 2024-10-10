using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GrenadeObject : MonoBehaviour
{
    // basic grenade object class
    // upon instantiation, objects of this type lobbed at an arc
    // to land at a targetPosition, landing after flightTime,
    // then creating an instance of spawnOnImpact before destroying itself
    // lifetime = flightTime
    public GameObject spawnOnImpact;

    public Vector3 targetPosition;
    public float flightTime = 1.0f;

    public float damage;

    private Vector3 startPosition;
    private Vector3 initialVelocity;
    private float timeAlive = 0.0f;

    protected virtual void Start()
    {
        startPosition = transform.position;
        initialVelocity = CalculateInitialVelocity(startPosition, targetPosition, flightTime);
    }

    protected virtual void Update()
    {
        timeAlive += Time.deltaTime;

        if (timeAlive < flightTime)
        {
            // move in xz axis (no accel)
            Vector3 newPosition = startPosition + initialVelocity * timeAlive;
            // move in y axis
            newPosition.y += 0.5f * Physics.gravity.y * Mathf.Pow(timeAlive, 2);
            transform.position = newPosition;
        }
        else
        {
            // landed
            GameObject spawnedAreaDamage = Instantiate(spawnOnImpact, transform.position, Quaternion.identity);
            spawnedAreaDamage.GetComponent<AreaDamage>().damage = damage;

            Destroy(gameObject);
        }
    }

    protected Vector3 CalculateInitialVelocity(Vector3 start, Vector3 end, float time)
    {
        // calc horizontal velocity required
        Vector3 initVelocity = (end - start) / time;
        // calc initial velocity for y axis given constant accel down
        // rearranged displacement kinematics equation for init velocity
        initVelocity.y = ((end.y - start.y) / time) - 0.5f * Physics.gravity.y * time;
        return initVelocity;
    }
}
