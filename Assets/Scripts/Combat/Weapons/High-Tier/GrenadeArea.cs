using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeArea : AreaDamage
{
    // grenade impact damage area object
    public GrenadeArea()
    {
        lifetime = 0.1f;
        damageCD = 1.0f;
    }
}
