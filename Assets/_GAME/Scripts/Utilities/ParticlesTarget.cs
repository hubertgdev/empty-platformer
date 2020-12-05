using UnityEngine;

///<summary>
/// 
///</summary>
[HelpURL("https://github.com/DaCookie/empty-platformer/blob/master/Docs/particles-target.md")]
public class ParticlesTarget : MonoBehaviour
{

	[SerializeField]
    private ParticleSystem m_ParticleSystem = null;

    [SerializeField]
    private Transform m_Target = null;

    [SerializeField, Range(0f, 1f)]
    private float m_Lerp = 0.1f;

    private void Awake()
    {
        if(m_ParticleSystem == null) { m_ParticleSystem = GetComponent<ParticleSystem>(); }
    }

    private void Update()
    {
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[m_ParticleSystem.particleCount];
        int count = m_ParticleSystem.GetParticles(particles);
       
        for (int i = 0; i < count; i++)
        {
            float distance = Vector3.Distance(Target, particles[i].position);
           
            if (distance > 0.1f)
            {
                particles[i].position = Vector3.Lerp(particles[i].position, Target, m_Lerp);
            }
        }

        m_ParticleSystem.SetParticles(particles, count);
    }

    private Vector3 Target
    {
        get { return m_Target != null ? m_Target.position : Vector3.zero; }
    }

}