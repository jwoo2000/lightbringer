using UnityEngine;

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
    private float maxSpeed = 100.0f;

    [SerializeField]
    private bool isDying = false;

    [SerializeField]
    private bool isWep = false;

    [SerializeField]
    private Weapon.Tier tier;

    [SerializeField]
    private int exp = 1;

    private Collider trigger;

    private Vector3 psCenter;

    [SerializeField]
    private float explodeFactor = 1.0f;

    private void Start()
    {
        InitIfNeeded();
        psCenter = particleSystem.transform.position;
        var trigger = particleSystem.trigger;
        trigger.AddCollider(attractor.GetComponent<Collider>());
    }

    void LateUpdate()
    {
        InitIfNeeded();

        if (isDying && (Time.timeScale != 0f))
        {
            int numParticlesAlive = particleSystem.GetParticles(particles);
            for (int i = 0; i < numParticlesAlive; i++)
            {
                // move particles towards player position + (0,0.5,0) the center of player rather than feet
                absorbDir = (((attractor.position + new Vector3(0, 0.5f, 0)) - particles[i].position).normalized);
                particles[i].velocity = Vector3.ClampMagnitude(particles[i].velocity + (absorbDir * absorbSpeed * Time.deltaTime), maxSpeed);
            }
            absorbSpeed = Mathf.Min(absorbSpeed * absorbSpeedMulti, maxSpeed);
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

    // returns direction away from center of particle system
    private Vector3 outwardsDir(Vector3 pos)
    {
        return (pos - psCenter).normalized;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            particleSystem.Stop();
            isDying = true;
            Destroy(GetComponent<Collider>());
            PlayerStats playerStats = other.gameObject.GetComponent<PlayerStats>();
            playerStats.addExp(exp);
            if (isWep)
            {
                playerStats.weaponController.absorbedWepFF(tier);
                int numParticlesAlive = particleSystem.GetParticles(particles);
                for (int i = 0; i < numParticlesAlive; i++)
                {
                    particles[i].velocity = Vector3.ClampMagnitude(particles[i].velocity + (outwardsDir(particles[i].position) * explodeFactor), maxSpeed);
                    if (particles[i].velocity.magnitude >= maxSpeed)
                    {
                        Debug.Log("firefly particle too fast! "+ particles[i].velocity.magnitude);
                    }
                }
                particleSystem.SetParticles(particles, numParticlesAlive);
            }
        }
    }
}
