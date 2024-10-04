using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelletController : MonoBehaviour
{
    [SerializeField] private ParticleSystem collisionParticles;
    [SerializeField] private int damageAmount = 50; 
    [SerializeField] private string tagToDamage;
    private EnemyBehaviour enemy;
    private float timeLeftAlive = 0.5f;
    private GameObject player;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeftAlive <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            timeLeftAlive -= Time.deltaTime;
            transform.position = player.transform.position + player.transform.forward;
        }
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
