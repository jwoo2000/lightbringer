using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraArea : AreaDamage
{
    // aura damage area object

    public Transform auraWeapon;

    [SerializeField] private float minScale;
    [SerializeField] private float maxScale;

    protected override void Update()
    {
        base.Update();
        minScale = aoeSize;
        maxScale = minScale * 1.1f;
        transform.position = auraWeapon.position;
        transform.Rotate(0, 30.0f * Time.deltaTime, 0);
    }

    protected override IEnumerator onInstantiate()
    {
        minScale = aoeSize;
        maxScale = minScale * 1.1f;
        yield return StartCoroutine(growAoe(maxScale));
        StartCoroutine(damageEnemiesArea());
        StartCoroutine(attackScale());
    }

    private IEnumerator attackScale()
    {
        while (!growing)
        {
            float timeToPeak = 0.0f;
            while (timeToPeak < (damageCD / 2.0f))
            {
                currScale = Mathf.Lerp(maxScale, minScale, timeToPeak / (damageCD / 2.0f));
                timeToPeak += Time.deltaTime;
                yield return null;
            }
            timeToPeak = 0.0f;
            while (timeToPeak < (damageCD / 2.0f))
            {
                currScale = Mathf.Lerp(minScale, maxScale, timeToPeak / (damageCD / 2.0f));
                timeToPeak += Time.deltaTime;
                yield return null;
            }
        }
    }
}
