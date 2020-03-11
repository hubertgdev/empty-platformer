using System.Collections;

using UnityEngine;

///<summary>
/// 
///</summary>
//[AddComponentMenu("Scripts/Shoot")]
public class Shoot : MonoBehaviour
{

    public enum EShootAim
    {
        Forward,
        AimWithMouse
    }

    [Header("Settings")]

    [SerializeField]
    private EShootAim m_AimingType = EShootAim.Forward;

    [SerializeField]
    private float m_ShootRange = 10f;

    [SerializeField]
    private float m_ShootCooldown = .3f;

    [SerializeField]
    private LayerMask m_ShootableObjectsLayer = ~0;

    [SerializeField]
    private int m_ShootDamages = 1;

    [Header("Events")]

    [SerializeField]
    private ShootInfosEvent m_OnShoot = new ShootInfosEvent();

    [SerializeField]
    private Vector3Event m_UpdateAim = new Vector3Event();

    [SerializeField]
    private HitInfosEvent m_OnHitTarget = new HitInfosEvent();

    private bool m_IsShooting = false;
    private float m_ShootCooldownTimer = 0f;
    private Coroutine m_ShootCooldownCoroutine = null;

    private void UpdateShoot(float _DeltaTime)
    {
        if(m_ShootCooldownTimer < m_ShootCooldown)
        {
            return;
        }

        if(Input.GetButton("Shoot"))
        {
            m_ShootCooldownCoroutine = StartCoroutine(ApplyShootCooldown(m_ShootRange, m_ShootCooldown));
            m_OnShoot.Invoke(new ShootInfos
            {
                origin = transform.position,
                direction = ShootVector,
                range = m_ShootRange,
                cooldown = m_ShootCooldown,
                damages = m_ShootDamages
            });

            if(Physics.Raycast(transform.position, ShootVector, out RaycastHit rayHit, m_ShootRange, m_ShootableObjectsLayer))
            {
                m_OnHitTarget.Invoke(new HitInfos { });
            }
        }
    }

    private IEnumerator ApplyShootCooldown(float _Range, float _Cooldown)
    {
        m_ShootCooldownTimer = 0f;
        while(m_ShootCooldownTimer < m_ShootCooldown)
        {
            m_ShootCooldownTimer += Time.deltaTime;
            yield return null;
        }
        m_ShootCooldownTimer = m_ShootCooldown;

        m_ShootCooldownCoroutine = null;
    }

    public Vector3 ShootVector
    {
        get { return transform.right; }
    }

}