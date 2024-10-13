using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AreaDamage : MonoBehaviour
{
    // basic area damage class
    // upon instantiation, objects of this type remain stationary until it dies (default after 2s)
    // they deal damage every damageCD (default 1s)
    public float lifetime = 2.0f;
    public float damageCD = 1.0f;
    public float aoeSize;
    public float timeToAoe;

    [SerializeField]
    protected bool growing;
    [SerializeField]
    protected float currScale;
    [SerializeField]
    private float initScale;

    public float damage;

    private float timeAlive = 0.0f;

    [SerializeField]
    private Collider[] colliders;

    protected virtual void Awake()
    {
        initScale = 0.1f;
        currScale = initScale;
        growing = true;
        transform.localScale = Vector3.one * currScale;
    }

    protected virtual void Update()
    {
        if (!growing)
        {
            timeAlive += Time.deltaTime;
        }
        if (timeAlive >= lifetime)
        {
            Destroy(gameObject);
        }

        updateScale();
    }

    protected virtual void updateScale()
    {
        transform.localScale = Vector3.one * currScale;
    }

    protected virtual void Start()
    {
        StartCoroutine(onInstantiate());
    }

    protected virtual IEnumerator onInstantiate()
    {
        yield return StartCoroutine(growAoe(aoeSize));
        StartCoroutine(damageEnemiesArea());
    }

    protected IEnumerator damageEnemiesArea()
    {
        while (timeAlive < lifetime)
        {
            colliders = Physics.OverlapSphere(transform.position, aoeSize/2.0f);
            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Enemy") && collider.isTrigger)
                {
                    EnemyBehaviour enemy = collider.GetComponent<EnemyBehaviour>();
                    if (enemy != null)
                    {
                        enemy.TakeDamage(damage);
                    }
                }
            }
            yield return new WaitForSeconds(damageCD);
        }
    }

    protected IEnumerator growAoe(float toAoeSize)
    {
        growing = true;
        float timeElapsed = 0.0f;
        while (timeElapsed < timeToAoe)
        {
            currScale = Mathf.Lerp(initScale, toAoeSize, timeElapsed/timeToAoe);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        currScale = toAoeSize;
        growing = false;
    }
}
