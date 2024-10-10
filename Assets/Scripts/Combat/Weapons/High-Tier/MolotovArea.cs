using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MolotovArea : AreaDamage
{
    // molotov impact damage area object
    private void Awake()
    {
        lifetime = 2.0f;
        damageCD = 1.0f;
    }
}
