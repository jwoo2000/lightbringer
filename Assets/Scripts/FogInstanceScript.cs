using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogInstanceScript : MonoBehaviour
{
    [SerializeField]
    public Transform player; // maintain relative pos from player

    [SerializeField]
    private float creepSpeed = 0.1f; // units per second, speed at which fog moves towards player

    private void Update()
    {
        var step = creepSpeed * Time.deltaTime;
        // step towards player, maintaining fog y position
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.position.x, transform.position.y, player.position.z), step);
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "FogDestroyer")
        {
            Debug.Log("Destroying fog!");
            Destroy(gameObject);
        }
    }

}
