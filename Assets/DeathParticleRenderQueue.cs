using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathParticleRenderQueue : MonoBehaviour
{
    void Start()
    {
        Material thisMat = GetComponent<Renderer>().material;
        thisMat.renderQueue = 3200;
    }
}
