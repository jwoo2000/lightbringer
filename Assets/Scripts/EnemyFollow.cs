using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    private Transform target; 
    public float speed;
    private float distanceToTarget;
    public float AttackWaitTime;
    private float AttackWindUp;
    public float LungeSpeed;
    private Vector3 AttackTarget;
    private Rigidbody rb;
    private float AttackWindDown; 
    public float AttackTriggerRange = 3.0f;


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        AttackWindUp = 0.0f;
        AttackWindDown = 0.0f;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        distanceToTarget = Vector3.Distance(target.position, transform.position);

        if (AttackWindDown > 0) {
            AttackWindDown -= Time.deltaTime;
            AttackTarget = target.position;
        } else if (distanceToTarget < AttackTriggerRange || AttackWindUp > 0) {
            rb.velocity = -1.0f * speed * transform.forward;
            AttackWindUp += Time.deltaTime;

            
            if (AttackWindUp > AttackWaitTime) {
                Attack();
                AttackWindUp = 0.0f;
                AttackWindDown = 0.5f;
            }
        } else {
            AttackTarget = target.position;
            rb.velocity = speed * transform.forward;
            //transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        transform.LookAt(target);

    }

    private void Attack() {
        AttackTarget.y = 1;
        rb.velocity = LungeSpeed * transform.forward;
        //transform.position = Vector3.MoveTowards(transform.position, AttackTarget, LungeDistance);
    }
}
