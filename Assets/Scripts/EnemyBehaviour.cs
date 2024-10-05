using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public enum EnemyType{
        Small,
        Medium,
        Large
    }

    public float speed = 1.0f;
    public float damageAmount = 20.0f;
    public float startingHealth = 100.0f;
    public EnemyType Behaviourtype;
    public UnitHealth _enemyHealth; 

    //Medium class specific
    public float AttackWaitTime = 0.5f;
    public float LungeSpeed = 7.0f;
    public float AttackTriggerRange = 3.0f;

    private Transform target; 
    private float distanceToTarget;
    private float AttackWindUp;
    private Vector3 AttackTarget;
    private Rigidbody rb;
    private float AttackCoolDown; 
    private bool DamageCoolDown;
    private string tagToDamage = "Player";



    // Start is called before the first frame update
    void Start()
    {
        _enemyHealth = new UnitHealth(startingHealth, startingHealth);
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        AttackWindUp = 0.0f;
        AttackCoolDown = 0.0f;
        DamageCoolDown = false;
        rb = GetComponent<Rigidbody>();
    }

    /*Different Enemy Behaviours
    
    small - deals damage on contact with player, slow speed, freezes temporarily after dealing damage, follows player
    medium - deals damage on contact while lunging, medium speed, follows player
    large - slow speed, keeps distance from player, continuously spawns medium and small enemies, has slow swing attack if player gets close

    */


    // Update is called once per frame
    void Update()
    {
        if (this._enemyHealth.Health <= 0.0f) {
            Destroy(gameObject);
            // Spawn an effect to be played when enemy dies
        }
        distanceToTarget = Vector3.Distance(target.position, transform.position);

        

        switch(Behaviourtype)
        {
            case EnemyType.Small:
            {
                if (AttackCoolDown <= 0) 
                {
                    DamageCoolDown = false;
                    AttackTarget = target.position;
                    rb.velocity = speed * transform.forward;
                } 
                else 
                {
                    AttackCoolDown -= Time.deltaTime;
                    
                }

                break;
            }
            case EnemyType.Medium:
            {
                if (AttackCoolDown > 0) 
                {
                    AttackCoolDown -= Time.deltaTime;
                    AttackTarget = target.position;
                } 
                else if (distanceToTarget < AttackTriggerRange || AttackWindUp > 0) 
                {
                rb.velocity = -1.0f * speed * transform.forward;
                AttackWindUp += Time.deltaTime;
                } 
                else 
                {
                    AttackTarget = target.position;
                    rb.velocity = speed * transform.forward;
                }
                    
                if (AttackWindUp > AttackWaitTime) {
                    Attack();
                }
                break;
            }
            case EnemyType.Large:
            {
                break;
            }
            default: break;
        }
        transform.LookAt(target, Vector3.up);
        transform.eulerAngles = new Vector3(0,transform.eulerAngles.y,0);

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
