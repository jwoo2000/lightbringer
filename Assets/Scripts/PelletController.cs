using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelletController : MonoBehaviour
{
    [SerializeField] private ParticleSystem collisionParticles;
    [SerializeField] private int damageAmount = 50; 
    [SerializeField] private string tagToDamage;
    private EnemyBehaviour enemy;

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == this.tagToDamage) 
        {
            enemy = col.gameObject.GetComponent<EnemyBehaviour>();
            enemy.TakeDamage(damageAmount);
            Destroy(gameObject);
        }
    }
}
