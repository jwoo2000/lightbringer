using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private MovementController movementController;

    [SerializeField]
    private float initMovespeed = 1.0f;

    [SerializeField]
    private float initSprintMulti = 1.5f;

    private void Awake()
    {
        movementController.setMovespeed(initMovespeed);
        movementController.setSprintMulti(initSprintMulti);
    }

    // Start is called before the first frame update
    void Start()
    {
        //movementController.setMovespeed(5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
