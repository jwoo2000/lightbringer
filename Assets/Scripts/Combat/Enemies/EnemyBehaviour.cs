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

    public Animator animator;
    public float speed = 1.0f;
    public float damageAmount = 20.0f;
    public float startingHealth = 100.0f;
    public EnemyType behaviourType;
    public UnitHealth _enemyHealth;
    public float enemyDmgReduc = 0.0f;
    public float detectionRange = 20.0f;
    public float loseDetectionRange = 40.0f;
    public int expOnDeath = 5;
    public float attackCD = 0.5f;

    private Transform target; 
    private float distanceToTarget;
    private float attackWindUp;
    private Vector3 attackTarget;
    private Rigidbody rb;
    private float attackCoolDown; 
    private bool damageCoolDown;
    private string tagToDamage = "Player";
    private bool playerDetected;
    private int rotationSpeed = 500;

    //Medium class specific
    public float attackWaitTime = 0.5f;
    public float lungeSpeed = 7.0f;
    public float attackTriggerRange = 3.0f;

    // Minion prefab for Large enemy
    public GameObject minion;
    private float spawnCooldown = 5.0f;

    //wandering variables
    private Vector3 wanderTarget;
    private float wanderValue = 0.0f;
    private bool wandering = false;

    private PlayerStats playerStats;

    void Awake()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        animator.SetFloat("Speed", speed);
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
            playerStats.addExp(expOnDeath);
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
            //transform.LookAt(target, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationTarget(target.position), rotationSpeed * Time.deltaTime);
            transform.eulerAngles = new Vector3(0,transform.eulerAngles.y,0);
        }
    }

    private Quaternion rotationTarget(Vector3 target) 
    {
        Quaternion rotTarget = Quaternion.LookRotation(target - this.transform.position);
        return rotTarget;
    }

    public void TakeDamage (float dmg) 
    {
        _enemyHealth.DmgUnit(dmg, enemyDmgReduc);
    }

    private void Attack() {
        /*attackTarget.y = 1;
        rb.velocity = lungeSpeed * transform.forward;
        attackWindUp = 0.0f;
        attackCoolDown = attackCD;*/
        damageCoolDown = false;
        
        animator.SetBool("Attack", true);
    }

    private void DetectPlayer()
    {
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
    }

    private void Chase() 
    {
        DetectPlayer();

        //Chase Player
        if (playerDetected)
        {
            animator.SetBool("Running", true);
        }
        else
        {
            animator.SetBool("Running", false);
            Wander();
        }
    }

    private Vector3 pickDirection()
    {
        int deg = Random.Range(0, 360);
        float rad = deg * Mathf.Deg2Rad;
        Vector3 lookDirection = new Vector3(Mathf.Sin(rad), 1, Mathf.Cos(rad));
        return lookDirection;
    }

    // When player is not in range, move in a random direction at 1/10 of the enemies base speed
    private void Wander() 
    {
        if (wandering) 
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationTarget(wanderTarget), 0.5f * rotationSpeed * Time.deltaTime);
            transform.eulerAngles = new Vector3(0,transform.eulerAngles.y,0);
            rb.velocity = 0.1f * speed * transform.forward;
            wanderValue += Time.deltaTime;
            if (wanderValue >= 2) 
            {
                wandering = false;
            }
        }
        else
        {
            wanderValue -= Time.deltaTime;
            if (wanderValue <= 0) 
            {
                wandering = true;
                wanderTarget = transform.position + pickDirection();
            }
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == this.tagToDamage && damageCoolDown == false) 
        {
            GameManager.gameManager._playerHealth.DmgUnit(damageAmount, GameManager.gameManager._playerStats.dmgReduction);
            Debug.Log("Health: " + GameManager.gameManager._playerHealth.Health);
            damageCoolDown = true;
            if (behaviourType == EnemyType.Minion) 
            {
                Destroy(gameObject);
            }
        }
    }

    private void SmallEnemyBehaviour()
    {
        if (attackCoolDown <= 0) 
        {
            damageCoolDown = false;
            Chase();
        } 
        else 
        {
            attackCoolDown -= Time.deltaTime;
        }
    }

    private void MediumEnemyBehaviour()
    {
        
        if (animator.GetBool("Attack")) 
        {
            animator.SetBool("Attack", false);
        }
        if (distanceToTarget < attackTriggerRange) 
        {
            Attack();
        } 
        else 
        {
            Chase();
        }
        

    }

    private void LargeEnemyBehaviour()
    {
        int keepAwayDistance = 15;
        int closeInDistance = 25;
        // Stay between 15 - 30 units away from the player
        DetectPlayer();

        if (!playerDetected) 
        {
            Wander();
        }

        if (distanceToTarget < keepAwayDistance) 
        {
            rb.velocity = -1 * speed * transform.forward;
        }
        else if (distanceToTarget > closeInDistance)
        {
            rb.velocity = speed * transform.forward;
        }

        if (spawnCooldown <= 0)
        {
            SpawnMinions();
            spawnCooldown = 10.0f;
        }
        else
        {
            spawnCooldown -= Time.deltaTime;
        }


    }

    private void SpawnMinions()
    {
        Vector3 spawnPos1 = transform.position + transform.forward - (2 * transform.right);
        spawnPos1.y = .25f;
        Vector3 spawnPos2 = transform.position + transform.forward + (2 * transform.right);
        spawnPos2.y = .25f;
        Instantiate(minion, spawnPos1, Quaternion.identity);
        Instantiate(minion, spawnPos2, Quaternion.identity);
    }

    private void MinionEnemyBehaviour()
    {
        Chase();
    }
}
