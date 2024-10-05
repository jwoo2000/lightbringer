using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCharacter : MonoBehaviour
{
    public new Transform light;
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        light.position = player.position;
        light.position += Vector3.up * 2;
        
    }
}
