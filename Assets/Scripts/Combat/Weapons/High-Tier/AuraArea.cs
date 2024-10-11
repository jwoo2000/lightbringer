using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraArea : AreaDamage
{
    // aura damage area object

    public Transform auraWeapon;

    protected override void Update()
    {
        base.Update();
        transform.position = auraWeapon.position;
        transform.Rotate(0, 30.0f * Time.deltaTime, 0);
    }
}
