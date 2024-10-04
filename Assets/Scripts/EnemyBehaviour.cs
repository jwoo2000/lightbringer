using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private Transform target; 
    public float speed = 1.0f;
    private float distanceToTarget;
    public float AttackWaitTime = 0.5f;
    private float AttackWindUp;
    public float LungeSpeed = 7.0f;
    private Vector3 AttackTarget;
    private Rigidbody rb;
    private float AttackCoolDown; 
    private bool DamageCoolDown;
    public float AttackTriggerRange = 3.0f;

    public float damageAmount = 20.0f;

    private string tagToDamage = "Player";

    public UnitHealth _enemyHealth = new UnitHealth(100.0f, 100.0f);


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        AttackWindUp = 0.0f;
        AttackCoolDown = 0.0f;
        DamageCoolDown = false;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this._enemyHealth.Health <= 0.0f) {
            Destroy(gameObject);
            // Spawn an effect to be played when enemy dies
        }
        distanceToTarget = Vector3.Distance(target.position, transform.position);

        if (AttackCoolDown > 0) {
            AttackCoolDown -= Time.deltaTime;
            AttackTarget = target.position;
        } else if (distanceToTarget < AttackTriggerRange || AttackWindUp > 0) {
            rb.velocity = -1.0f * speed * transform.forward;
            AttackWindUp += Time.deltaTime;

            
            if (AttackWindUp > AttackWaitTime) {
                Attack();
                
            }
        } else {
            AttackTarget = target.position;
            rb.velocity = speed * transform.forward;
            //transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        transform.LookAt(target);

    }

    public void TakeDamage (float dmg) 
    {
        _enemyHealth.DmgUnit(dmg);
    }

    private void Attack() {
        AttackTarget.y = 1;
        rb.velocity = LungeSpeed * transform.forward;
        AttackWindUp = 0.0f;
        AttackCoolDown = 0.5f;
        DamageCoolDown = false;
        //  Replace this with collision detection with player
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == this.tagToDamage && DamageCoolDown == false) 
        {
            GameManager.gameManager._playerHealth.DmgUnit(damageAmount);
            Debug.Log("Health: " + GameManager.gameManager._playerHealth.Health);
            DamageCoolDown = true;
        }
    }
}
