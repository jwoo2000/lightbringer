using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicWeaponController : MonoBehaviour
{
    //[SerializeField]
    //private float degPerSec = 30.0f;

    [SerializeField]
    private Transform player;

    // Update is called once per frame
    //void Update()
    //{
    //    transform.Rotate(new Vector3(0, degPerSec * Time.deltaTime, 0));
    //}

    void LateUpdate()
    {
        transform.position = player.position + new Vector3(0, 1.0f, 0);
    }
}
