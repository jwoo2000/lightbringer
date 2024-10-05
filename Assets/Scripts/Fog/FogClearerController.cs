using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogClearerController : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    public void updateScale(float newScale)
    {
        transform.localScale = new Vector3(newScale, newScale, newScale);
    }

    void LateUpdate()
    {
        transform.position = player.position + new Vector3(0, 1.0f, 0);
    }
}
