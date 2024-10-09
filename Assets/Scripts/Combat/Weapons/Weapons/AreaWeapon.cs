using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AreaWeapon : Weapon
{
    // area weapons have a duration of their damaging area and how fast it deals damage
    [SerializeField]
    protected float areaDuration; // default 1.0 s
    [SerializeField]
    protected float damageCD; // default 1.0 s
}
