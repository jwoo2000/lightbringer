using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackBehaviour : MonoBehaviour
{
    public GameObject player;
    [SerializeField] private GameObject attackObject;

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Instantiate(attackObject, AttackPosition(), Quaternion.identity);
        }
    }

    private Vector3 AttackPosition()
    {
        Vector3 position = player.transform.position;
        position += player.transform.forward;
        position.y = 1;
        return position;
    }
}
