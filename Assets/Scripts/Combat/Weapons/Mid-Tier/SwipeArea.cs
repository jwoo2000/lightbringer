using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeArea : AreaDamage
{
    // swipe damage area object
    protected override void updateScale()
    {
        transform.localScale = new Vector3(currScale, 1.0f, currScale);
    }
}
