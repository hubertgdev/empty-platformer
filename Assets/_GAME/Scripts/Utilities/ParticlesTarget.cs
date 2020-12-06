using UnityEngine;

///<summary>
/// Modify particles emission shape to move them smoothly to a given target.
///</summary>
[HelpURL("https://github.com/DaCookie/empty-platformer/blob/master/Docs/particles-target.md")]
public class ParticlesTarget : MonoBehaviour
{

	[SerializeField]
    [Tooltip("The reference to the `Particle System` component affected by this effect")]
    private ParticleSystem m_ParticleSystem = null;

    [SerializeField]
    [Tooltip("The position to which the particles will move")]
    private Transform m_Target = null;

    [SerializeField, Range(0f, 1f)]
    [Tooltip("The linear interpolation amount from the current particle position to the target the particles will move along each frame")]
    private float m_Lerp = 0.1f;

    [SerializeField, Min(0f)]
    [Tooltip("Defines the particles before this component affect the particles position. Before this delay, the particles will ask as they normally do from the Particle System")]
    private float m_AffectParticlesDelay = 0f;

    private void Awake()
    {
        if(m_ParticleSystem == null) { m_ParticleSystem = GetComponent<ParticleSystem>(); }
        if (m_ParticleSystem == null)
            Debug.LogWarning("No Particle System set on this Particles Target component");

        if (m_Target == null)
            Debug.LogWarning("No Target set on this Particles Target component");
    }

    private void Update()
    {
        if (m_ParticleSystem == null || m_Target == null)
            return;

        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[m_ParticleSystem.particleCount];
        int count = m_ParticleSystem.GetParticles(particles);
       
        for (int i = 0; i < count; i++)
        {
            if (particles[i].startLifetime - particles[i].remainingLifetime >= m_AffectParticlesDelay)
                particles[i].position = Vector3.Lerp(particles[i].position, Target, m_Lerp);
        }

        m_ParticleSystem.SetParticles(particles, count);
    }

    private Vector3 Target
    {
        get { return m_Target != null ? m_Target.position : transform.position; }
    }

}