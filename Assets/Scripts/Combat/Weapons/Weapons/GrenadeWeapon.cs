using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GrenadeWeapon : Weapon
{
    // grenade weapons have a
    // - targetPosition to throw the grenades towards
    // - flightTime for how long the grenades take to land
    [SerializeField]
    protected Vector3 targetPosition;
    [SerializeField]
    protected float flightTime;
}
