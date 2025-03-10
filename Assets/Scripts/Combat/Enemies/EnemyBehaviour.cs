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
    private float wepUpgradeDropChance = 0.2f;
    public float keepAwayDistance = 8.0f;
    public float closeInDistance = 10.0f;

    //Sword for large enemies
    [SerializeField] private GameObject sword;

    //wandering variables
    private Vector3 wanderTarget;
    private float wanderValue = 0.0f;
    private bool wandering = false;

    protected PlayerStats playerStats;
    private DropWepManager dropWepManager;

    [SerializeField]
    private GameObject onHitParticle;
    [SerializeField]
    private GameObject onDeathParticle;
    [SerializeField]
    private GameObject onDeathDropParticle;

    [SerializeField] private AudioClip takeDamageSound;
    [SerializeField] private AudioClip deathSound;

    private bool isAttacking = false;

    protected virtual void Awake()
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
            case EnemyType.Large:
                {
                    dropWepManager = GameObject.FindGameObjectWithTag("DropWepManager").GetComponent<DropWepManager>();
                    break;
                }
        }
        if (sword != null) {
            sword.SetActive(false);
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

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") || animator.GetCurrentAnimatorStateInfo(0).IsName("Summon") || animator.GetCurrentAnimatorStateInfo(0).IsName("Sword"))
        {
            isAttacking = true;
        } else
        {
            isAttacking = false;
        }

        if (playerDetected && !isAttacking) 
        {
            //transform.LookAt(target, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationTarget(target.position), rotationSpeed * Time.deltaTime);
            transform.eulerAngles = new Vector3(0,transform.eulerAngles.y,0);
        }
    }

    protected virtual void OnDeath()
    {
        GameObject deathParticle = Instantiate(onDeathParticle, transform.position, Quaternion.identity);
        SoundManager.instance.playSound(SoundManager.AudioType.SFX, deathSound, transform.position, 1.0f);
        switch (behaviourType)
        {
            case EnemyType.Medium:
                deathParticle.transform.localScale = Vector3.one * 1.2f;
                break;
            case EnemyType.Large:
                deathParticle.transform.localScale = Vector3.one * 2f;
                if (Random.Range(0.0f, 1.0f) <= wepUpgradeDropChance)
                {
                    GameObject dropDeathParticle = Instantiate(onDeathDropParticle, transform.position, Quaternion.identity);
                    dropDeathParticle.transform.localScale = Vector3.one * 2f;
                    dropDeathParticle.transform.GetChild(0).localScale = Vector3.one * 2f;
                    dropWepManager.triggerDrop(transform.position);
                }
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
        if (!GameManager.gameWin)
        {
            SoundManager.instance.playSound(SoundManager.AudioType.SFX, takeDamageSound, transform.position, 0.9f);
        }
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
        rb.velocity = rb.velocity * 0.8f;
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
                if (GameManager.playerAlive && !GameManager.gameWin)
                {
                    // only play hit sound if player is alive
                    col.gameObject.GetComponent<PlayerSounds>().takeDamage();
                    col.gameObject.GetComponent<PlayerShaderController>().TriggerDamagePulse();
                }

                GameManager.gameManager._playerHealth.DmgUnit(damageAmount, GameManager.gameManager._playerStats.dmgReduction);
                Destroy(gameObject);
            }
            else if (damageCoolDown == false)
            {
                if (GameManager.playerAlive && !GameManager.gameWin)
                {
                    // only play hit sound if player is alive
                    col.gameObject.GetComponent<PlayerSounds>().takeDamage();
                    col.gameObject.GetComponent<PlayerShaderController>().TriggerDamagePulse();
                }

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
                SpawnMinions(Mathf.Min(Mathf.Max(1, playerStats.level/10), 4));
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
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Sword")) 
        {
            sword.SetActive(true);
        }
        else {
            sword.SetActive(false);
        }
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
            attackCoolDown -= Time.deltaTime;
            if (attackCoolDown <= 0) {
                damageCoolDown = true;
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
