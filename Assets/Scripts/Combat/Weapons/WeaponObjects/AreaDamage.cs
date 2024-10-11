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

    public float damage;

    private float timeAlive = 0.0f;
    private List<EnemyBehaviour> enemiesInArea = new List<EnemyBehaviour>(); // list of enemies in area collider

    protected virtual void Update()
    {
        timeAlive += Time.deltaTime;
        if (timeAlive >= lifetime)
        {
            Destroy(gameObject);
        }
    }

    // add enemies that enter the area to the stuff to damage list
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && other.isTrigger)
        {
            EnemyBehaviour enemy = other.GetComponent<EnemyBehaviour>();
            if (enemy != null)
            {
                enemiesInArea.Add(enemy);
                if (enemiesInArea.Count == 1)
                {
                    StartCoroutine(damageEnemiesInArea());
                }
            }
        }
    }

    // remove enemies that leave the area frmo the stuff to damage list
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy") && other.isTrigger)
        {
            EnemyBehaviour enemy = other.gameObject.GetComponent<EnemyBehaviour>();
            if (enemy != null)
            {
                enemiesInArea.Remove(enemy);
            }
        }
    }

    private IEnumerator damageEnemiesInArea()
    {
        while (enemiesInArea.Count > 0)
        {
            foreach (EnemyBehaviour enemy in enemiesInArea)
            {
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }
            }

            yield return new WaitForSeconds(damageCD);
        }
    }
}
