using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.Rendering.VirtualTexturing;

public class AbsorbFireflies : MonoBehaviour
{
    [SerializeField]
    public Transform attractor;

    private new ParticleSystem particleSystem;
    private ParticleSystem.Particle[] particles;
    private Vector3 absorbDir;

    [SerializeField]
    private float absorbSpeed = 3.0f;
    [SerializeField]
    private float absorbSpeedMulti = 1.03f;

    [SerializeField]
    private bool isDying = false;

    [SerializeField]
    private int exp = 1;

    private Collider trigger;

    private void Start()
    {
        InitIfNeeded();
        var trigger = particleSystem.trigger;
        trigger.AddCollider(attractor.GetComponent<Collider>());
    }

    void LateUpdate()
    {
        InitIfNeeded();

        if (isDying)
        {
            int numParticlesAlive = particleSystem.GetParticles(particles);
            for (int i = 0; i < numParticlesAlive; i++)
            {
                // move particles towards player position + (0,0.5,0) the center of player rather than feet
                absorbDir = (((attractor.position + new Vector3(0, 0.5f, 0)) - particles[i].position).normalized);
                particles[i].velocity += absorbDir * absorbSpeed * Time.deltaTime;
            }
            absorbSpeed *= absorbSpeedMulti;
            particleSystem.SetParticles(particles, numParticlesAlive);
        }
    }
    void InitIfNeeded()
    {
        if (particleSystem == null)
            particleSystem = GetComponent<ParticleSystem>();

        if (particles == null || particles.Length < particleSystem.main.maxParticles)
            particles = new ParticleSystem.Particle[particleSystem.main.maxParticles];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerStats>().addExp(exp);
            isDying = true;
            Destroy(GetComponent<Collider>());
            particleSystem.Stop();
        }
    }
}
