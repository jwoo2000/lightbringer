using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public enum EnemyType{
        Small,
        Medium,
        Large,
        Minion,
        Boss
    }

    public Animator animator;
    public float speed = 1.0f;
    //acceleration only used for minions
    public float acceleration = 1.0f;
    public float knockback = 1.0f;
    public float damageAmount = 20.0f;
    public float startingHealth = 100.0f;
    public EnemyType behaviourType;
    public UnitHealth _enemyHealth;
    public float enemyDmgReduc = 0.0f;
    public float detectionRange = 20.0f;
    public float loseDetectionRange = 40.0f;
    public int expOnDeath = 5;
    public float attackCD = 1.5f;
    public float despawnDistance = 40.0f;

    public Transform target; 
    private float distanceToTarget;
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
    private float minSpawnCD = 5.0f;
    private float maxSpawnCD = 10.0f;
    private float currSpawnCD;

    //wandering variables
    private Vector3 wanderTarget;
    private float wanderValue = 0.0f;
    private bool wandering = false;

    private PlayerStats playerStats;

    [SerializeField]
    private GameObject onHitParticle;
    [SerializeField]
    private GameObject onDeathParticle;

    void Awake()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        animator = GetComponent<Animator>();
        switch (behaviourType)
        {
            case EnemyType.Small:
                {
                    speed = speed * Random.Range(0.9f, 1.1f);
                    break;
                }
            case EnemyType.Medium:
                {
                    speed = speed * Random.Range(0.9f, 1.1f);
                    break;
                }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        animator.SetFloat("Speed", speed);
        _enemyHealth = new UnitHealth(startingHealth, startingHealth);
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        attackCoolDown = 0.0f;
        if (behaviourType == EnemyType.Minion)
        {
            damageCoolDown = false;
        }
        else
        {
            damageCoolDown = true;
        }
        rb = GetComponent<Rigidbody>();
        playerDetected = false;
        if (behaviourType == EnemyType.Large)
        {
            currSpawnCD = Random.Range(minSpawnCD, maxSpawnCD);
        }
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
            OnDeath();
        }
        distanceToTarget = Vector3.Distance(target.position, transform.position);
        if ((distanceToTarget > despawnDistance) && (behaviourType != EnemyType.Boss)) 
        {
            Destroy(gameObject);
        }
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
            case EnemyType.Boss:
            {
                BossEnemyBehaviour();
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

    protected virtual void OnDeath()
    {
        GameObject deathParticle = Instantiate(onDeathParticle, transform.position, Quaternion.identity);
        switch (behaviourType)
        {
            case EnemyType.Medium:
                deathParticle.transform.localScale = Vector3.one * 1.2f;
                break;
            case EnemyType.Large:
                deathParticle.transform.localScale = Vector3.one * 2f;
                break;
            case EnemyType.Boss:
                deathParticle.transform.localScale = Vector3.one * 3f;
                break;
        }
        playerStats.addExp(expOnDeath);
        Destroy(gameObject);
        // Spawn an effect to be played when enemy dies
    }

    private Quaternion rotationTarget(Vector3 target) 
    {
        Quaternion rotTarget = Quaternion.LookRotation(target - this.transform.position);
        return rotTarget;
    }

    public void TakeDamage (float dmg) 
    {
        GameObject hitParticle = Instantiate(onHitParticle, transform.position, Quaternion.identity);
        switch (behaviourType)
        {
            case EnemyType.Medium:
                hitParticle.transform.localScale = Vector3.one * 1.2f;
                break;
            case EnemyType.Large:
                hitParticle.transform.localScale = Vector3.one * 2f;
                break;
            case EnemyType.Boss:
                hitParticle.transform.localScale = Vector3.one * 3f;
                break;
        }
        _enemyHealth.DmgUnit(dmg, enemyDmgReduc);
        animator.SetTrigger("Hit");
        //transform.position = transform.position - (transform.forward * knockback);
        rb.velocity = rb.velocity / 1.5f;
    }

    private void Attack() {
        damageCoolDown = false;
        attackCoolDown = attackCD;
        animator.SetTrigger("Attack");
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
        animator.SetFloat("Speed", speed);

        //Chase Player
        if (playerDetected)
        {
            if (rb.velocity.magnitude < speed && behaviourType == EnemyType.Minion) 
            {
                rb.velocity += speed * transform.forward * Time.deltaTime * acceleration;
            }
            if (behaviourType == EnemyType.Boss)
            {
                transform.position = transform.position + (transform.forward * speed * Time.deltaTime);
            }
            
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
        animator.SetFloat("Speed", speed / 2.0f);
        if (wandering) 
        {
            animator.SetBool("Running", true);
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
            animator.SetBool("Running", false);
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
        if (col.CompareTag(tagToDamage)) 
        {
            
            if (behaviourType == EnemyType.Minion) 
            {
                GameManager.gameManager._playerHealth.DmgUnit(damageAmount, GameManager.gameManager._playerStats.dmgReduction);
                Destroy(gameObject);
            }
            else if (damageCoolDown == false)
            {
                GameManager.gameManager._playerHealth.DmgUnit(damageAmount, GameManager.gameManager._playerStats.dmgReduction);
                //Debug.Log("Health: " + GameManager.gameManager._playerHealth.Health);
                damageCoolDown = true;
            }
        }
    }

    private void SmallEnemyBehaviour()
    {
        if (distanceToTarget < attackTriggerRange ) 
        {
            animator.SetBool("Running", false);
            if (attackCoolDown <= 0)
            {
                Attack();
            } 
            else
            {
                attackCoolDown -= Time.deltaTime;
            }
        } 
        else 
        {
            Chase();
            attackCoolDown -= Time.deltaTime;
            if (attackCoolDown <= 0) {
                damageCoolDown = true;
            }
        }
    }

    private void MediumEnemyBehaviour()
    {
        if (distanceToTarget < attackTriggerRange ) 
        {
            animator.SetBool("Running", false);
            if (attackCoolDown <= 0)
            {
                Attack();
            } 
            else
            {
                attackCoolDown -= Time.deltaTime;
            }
        } 
        else 
        {
            Chase();
            attackCoolDown -= Time.deltaTime;
            if (attackCoolDown <= 0) {
                damageCoolDown = true;
            }
        }
        

    }

    private void LargeEnemyBehaviour()
    {
        int keepAwayDistance = 10;
        int closeInDistance = 15;
        // Stay between 15 - 30 units away from the player
        DetectPlayer();

        if (!playerDetected) 
        {
            Wander();
        }
        else 
        {
            if (currSpawnCD <= 0)
            {
                animator.SetTrigger("Summon");
                SpawnMinions(playerStats.level/10);
                currSpawnCD = Random.Range(minSpawnCD, maxSpawnCD);
            }
            else
            {
                currSpawnCD -= Time.deltaTime;
            }
            if (distanceToTarget < keepAwayDistance) 
            {
                animator.SetBool("Backward", true);
                animator.SetBool("Forward", false);
            }
            else if (distanceToTarget > closeInDistance)
            {
                animator.SetBool("Forward", true);
                animator.SetBool("Backward", false);
            }
            else 
            {
                animator.SetBool("Backward", false);
                animator.SetBool("Forward", false);
            }
        }
    }

    private void SpawnMinions(int spawnCount)
    {
        float spawnOffset = 1.5f;
        float yPos = 0.25f;
        float xOffset = 0.0f;
        Vector3 spawnPos;

        for (int i = 0; i < spawnCount; i++)
        {
            xOffset = (i - ((spawnCount - 1) / 2.0f)) * spawnOffset;
            spawnPos = transform.position + (transform.forward*1.2f) + (xOffset * transform.right);
            spawnPos.y = yPos;
            Instantiate(minion, spawnPos, Quaternion.identity);
        }
    }

    private void MinionEnemyBehaviour()
    {
        damageCoolDown = false;
        Chase();
    }

    private void BossEnemyBehaviour()
    {
        DetectPlayer();
        if (playerDetected)
        {
            animator.SetTrigger("Wakeup");
        }
        
        if (attackCoolDown <= 0)
        {
            if (distanceToTarget < attackTriggerRange)
            {
                Attack();
                animator.SetBool("Running", false);
            }
            else 
            {
                Chase();
            }
        }
        else
        {
            attackCoolDown -= Time.deltaTime;
            if (attackCoolDown <= 0)
            {
                damageCoolDown = true;
            }
        }
        // Hard coding locking the Y value
        transform.position = new Vector3(transform.position.x, 0.0f, transform.position.z);
    }
}
