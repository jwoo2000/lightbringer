using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjWeapon : Weapon
{
    // projectile weapons have a projspeed
    [SerializeField]
    protected float projSpeed = 1.0f; // default 1.0f unit/s
}
