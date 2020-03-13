using System.Collections;

using UnityEngine;

///<summary>
/// 
///</summary>
//[AddComponentMenu("Scripts/Shoot")]
public class Shoot : MonoBehaviour
{

    #region Properties

    public enum EShootAim
    {
        TransformRight,
        AimWithMouse
    }

    [Header("Settings")]

    [SerializeField, Tooltip("Defines if the player is aiming using the tranform.right vector of the object, or using mouse pointer")]
    private EShootAim m_AimingType = EShootAim.TransformRight;

    [SerializeField, Tooltip("Defines the range of the shoot action")]
    private float m_ShootRange = 10f;

    [SerializeField, Tooltip("Defines the cooldown of the shoot action")]
    private float m_ShootCooldown = .3f;

    [SerializeField, Tooltip("Defines which object can be shot")]
    private LayerMask m_ShootableObjectsLayer = ~0;

    [SerializeField, Tooltip("Defines the number of lives the shoot action inflcts")]
    private int m_ShootDamages = 1;

    [SerializeField, Tooltip("Freezes the Shoot action")]
    private bool m_FreezeShoot = false;

    [Header("Events")]

    // Called when the character shoots (even if no target is hit)
    [SerializeField]
    private ShootInfosEvent m_OnShoot = new ShootInfosEvent();

    // Called when the aiming vector changes
    [SerializeField]
    private Vector3Event m_OnUpdateAim = new Vector3Event();

    // Called when a target is hit
    [SerializeField]
    private HitInfosEvent m_OnHitTarget = new HitInfosEvent();

    #if UNITY_EDITOR

    [Header("Debug")]

    [SerializeField, Tooltip("Defines if debug lines are drawn when the character shoots")]
    private bool m_EnableDebugLines = true;

    [SerializeField, Tooltip("Defines the lifetime of debug lines if they're enabled")]
    private float m_DebugLineDuration = 2f;

    #endif

    // The current cooldown timer
    private float m_ShootCooldownTimer = 0f;

    // The current cooldown coroutine
    private Coroutine m_ShootCooldownCoroutine = null;

    private Scorer m_Scorer = null;

    #endregion


    #region Lifecycle

    /// <summary>
    /// Called when this component is loaded.
    /// </summary>
    private void Awake()
    {
        m_ShootCooldownTimer = m_ShootCooldown;
        if(m_Scorer == null) { m_Scorer = GetComponent<Scorer>(); }
    }

    /// <summary>
    /// Called once per frame.
    /// </summary>
    private void Update()
    {
        UpdateShoot(Time.deltaTime);
    }

    #endregion


    #region Public Methods

    /// <summary>
    /// Returns the aim vector, depending on the selected aim mode.
    /// </summary>
    public Vector3 AimVector
    {
        get { return transform.right; }
    }

    /// <summary>
    /// Checks if this character is shooting (it has a running cooldown).
    /// </summary>
    public bool IsShooting
    {
        get { return (m_ShootCooldownTimer < m_ShootCooldown); }
    }

    /// <summary>
    /// Freezes the shoot action.
    /// </summary>
    public bool FreezeShoot
    {
        get { return m_FreezeShoot; }
        set { m_FreezeShoot = value; }
    }

    #endregion


    #region Private Methods

    /// <summary>
    /// Updates the shoot action.
    /// If the character is currently shooting (its cooldown is running), the action is cancelled.
    /// If the hit object has a Shootable component, triggers OnShot event on that component.
    /// </summary>
    private void UpdateShoot(float _DeltaTime)
    {
        // If the character is already shooting (its cooldown is running) or the action is frozen, cancel update
        if(IsShooting || m_FreezeShoot) { return; }

        if(Input.GetButton("Shoot"))
        {
            // Start the cooldown coroutine
            m_ShootCooldownCoroutine = StartCoroutine(ApplyShootCooldown(m_ShootRange, m_ShootCooldown));
            // Call OnShoot event
            m_OnShoot.Invoke(new ShootInfos
            {
                origin = transform.position,
                direction = AimVector,
                range = m_ShootRange,
                cooldown = m_ShootCooldown,
                damages = m_ShootDamages
            });

            // If the shot hit something
            if(Physics.Raycast(transform.position, AimVector, out RaycastHit rayHit, m_ShootRange, m_ShootableObjectsLayer))
            {
                HitInfos infos = new HitInfos
                {
                    shooter = gameObject,
                    target = rayHit.collider.gameObject,
                    impact = rayHit.point,
                    distance = rayHit.distance,
                    damages = m_ShootDamages
                };

                // Call OnHitTarget event
                m_OnHitTarget.Invoke(infos);

                // Notify the target being hit
                Shootable shootable = rayHit.collider.GetComponent<Shootable>();
                if(shootable != null)
                {
                    shootable.NotifyHit(infos);
                }

                // If this character can gain score
                if(m_Scorer != null)
                {
                    // Get score from the target if possible
                    ShotScore shotScore = rayHit.collider.GetComponent<ShotScore>();
                    if(shotScore != null)
                    {
                        m_Scorer.GainScore(shotScore.ScoreByShot);
                    }
                }

                #if UNITY_EDITOR
                if(m_EnableDebugLines)
                {
                    Debug.DrawLine(transform.position, transform.position + AimVector * rayHit.distance, Color.red, m_DebugLineDuration);
                }
                #endif
            }
            #if UNITY_EDITOR
            else if(m_EnableDebugLines)
            {
                Debug.DrawLine(transform.position, transform.position + AimVector * m_ShootRange, Color.red, m_DebugLineDuration);
            }
            #endif
        }
    }

    /// <summary>
    /// Meant to be used as coroutine.
    /// This updates the Shoot Cooldown Timer until it reaches the cooldown value.
    /// </summary>
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

    #endregion

}