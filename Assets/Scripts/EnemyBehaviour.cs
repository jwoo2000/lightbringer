using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public enum EnemyType{
        Small,
        Medium,
        Large,
        Minion
    }

    public float speed = 1.0f;
    public float damageAmount = 20.0f;
    public float startingHealth = 100.0f;
    public EnemyType behaviourType;
    public UnitHealth _enemyHealth; 
    public float detectionRange = 20.0f;
    public float loseDetectionRange = 40.0f;

    //Medium class specific
    public float attackWaitTime = 0.5f;
    public float lungeSpeed = 7.0f;
    public float attackTriggerRange = 3.0f;

    private Transform target; 
    private float distanceToTarget;
    private float attackWindUp;
    private Vector3 attackTarget;
    private Rigidbody rb;
    private float attackCoolDown; 
    private bool damageCoolDown;
    private string tagToDamage = "Player";
    private bool playerDetected;



    // Start is called before the first frame update
    void Start()
    {
        _enemyHealth = new UnitHealth(startingHealth, startingHealth);
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        attackWindUp = 0.0f;
        attackCoolDown = 0.0f;
        damageCoolDown = false;
        rb = GetComponent<Rigidbody>();
        playerDetected = false;
    }

    /*Different Enemy Behaviours
    
    small - deals damage on contact with player, slow speed, freezes temporarily after dealing damage, follows player
    medium - deals damage on contact while lunging, medium speed, follows player
    large - slow speed, keeps distance from player, continuously spawns minions, has slow swing attack if player gets close

    */


    // Update is called once per frame
    void Update()
    {
        if (this._enemyHealth.Health <= 0.0f) {
            Destroy(gameObject);
            // Spawn an effect to be played when enemy dies
        }
        distanceToTarget = Vector3.Distance(target.position, transform.position);

        switch(behaviourType)
        {
            case EnemyType.Small:
            {
                SmallEnemyBehaviour();
                break;
            }
            case EnemyType.Medium:
            {
                MediumEnemyBehaviour();
                break;
            }
            case EnemyType.Large:
            {
                LargeEnemyBehaviour();
                break;
            }
            case EnemyType.Minion:
            {
                SmallEnemyBehaviour();
                break;
            }
            default: break;
        }
        if (playerDetected) 
        {
            transform.LookAt(target, Vector3.up);
            transform.eulerAngles = new Vector3(0,transform.eulerAngles.y,0);
        }
    }

    public void TakeDamage (float dmg) 
    {
        _enemyHealth.DmgUnit(dmg);
    }

    private void Attack() {
        attackTarget.y = 1;
        rb.velocity = lungeSpeed * transform.forward;
        attackWindUp = 0.0f;
        attackCoolDown = 0.5f;
        damageCoolDown = false;
    }

    private void Move() {
        //Detect player 
        if (distanceToTarget < detectionRange)
        {
            playerDetected = true;
        }

        //Lose sight of player
        if (distanceToTarget > loseDetectionRange) 
        {
            playerDetected = false;
        }

        //Chase Player
        if (playerDetected)
        {
            attackTarget = target.position;
            rb.velocity = speed * transform.forward;
        }
        else
        {
            //wander
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == this.tagToDamage && damageCoolDown == false) 
        {
            GameManager.gameManager._playerHealth.DmgUnit(damageAmount);
            Debug.Log("Health: " + GameManager.gameManager._playerHealth.Health);
            damageCoolDown = true;
        }
    }

    private void SmallEnemyBehaviour()
    {
        if (attackCoolDown <= 0) 
        {
            damageCoolDown = false;
            Move();
        } 
        else 
        {
            attackCoolDown -= Time.deltaTime;
            
        }
    }

    private void MediumEnemyBehaviour()
    {
        if (attackCoolDown > 0) 
        {
            attackCoolDown -= Time.deltaTime;
            attackTarget = target.position;
        } 
        else if (distanceToTarget < attackTriggerRange || attackWindUp > 0) 
        {
        rb.velocity = -1.0f * speed * transform.forward;
        attackWindUp += Time.deltaTime;
        } 
        else 
        {
            Move();
        }
            
        if (attackWindUp > attackWaitTime) {
            Attack();
        }
    }

    private void LargeEnemyBehaviour()
    {

    }

    private void MinionEnemyBehaviour()
    {

    }
}
