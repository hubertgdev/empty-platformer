using System.Collections;

using UnityEngine;
using UnityEngine.InputSystem;

///<summary>
/// 
///</summary>
//[AddComponentMenu("Scripts/Shoot")]
public class Shoot : MonoBehaviour
{

    #region Enums & Subclasses

    public enum EShootAim
    {
        TransformRight,
        AimWithMouse
    }

    [System.Serializable]
    private class ShootEvents
    {
        // Called when the character shoots (even if no target is hit)
        public ShootInfosEvent OnShoot = new ShootInfosEvent();

        // Called when the aiming vector changes
        public AimInfosEvent OnUpdateAim = new AimInfosEvent();

        // Called when a target is hit
        public HitInfosEvent OnHitTarget = new HitInfosEvent();
    }

    #endregion


    #region Properties

    private const string DEFAULT_SHOOT_BINDING = "<Mouse>/leftButton";
    private const string DEFAULT_AIM_POSITION_BINDING = "<Mouse>/position";

    [Header("Controls")]

    [SerializeField]
    private InputAction m_ShootAction = new InputAction("Shoot", InputActionType.Button, DEFAULT_SHOOT_BINDING);

    [SerializeField]
    private InputAction m_AimPositionAction = new InputAction("AimPosition", InputActionType.Value, DEFAULT_AIM_POSITION_BINDING, null, null, "Vector2");

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

    [SerializeField, Tooltip("Defines the Z position of the pointer when aiming with mouse (useful for working with 2D)")]
    private float m_AimWithMouseZPosition = 0.5f;

    [SerializeField, Tooltip("Freezes the Shoot action")]
    private bool m_FreezeShoot = false;

    [Header("Events")]

    [SerializeField]
    private ShootEvents m_ShootEvents = new ShootEvents();

    #if UNITY_EDITOR

    [Header("Debug")]

    [SerializeField, Tooltip("Defines if debug lines are drawn when the character shoots")]
    private bool m_EnableDebugLines = true;

    [SerializeField, Tooltip("Defines the lifetime of debug lines if they're enabled")]
    private float m_DebugLineDuration = 2f;

    [SerializeField]
    private bool m_DrawAimVector = true;

    #endif

    // The current cooldown timer
    private float m_ShootCooldownTimer = 0f;

    // The current cooldown coroutine
    private Coroutine m_ShootCooldownCoroutine = null;

    private Vector2 m_AimPosition = Vector2.zero;
    private Vector3 m_LastAimVector = Vector3.zero;

    private Scorer m_Scorer = null;

    #endregion


    #region Lifecycle

    /// <summary>
    /// Called when this component is loaded.
    /// </summary>
    private void Awake()
    {
        if (m_Scorer == null) { m_Scorer = GetComponent<Scorer>(); }
        m_ShootCooldownTimer = m_ShootCooldown;
        m_LastAimVector = AimVector;

    }

    /// <summary>
    /// Called when this object is enabled.
    /// </summary>
    private void OnEnable()
    {
        BindControls(true);
    }

    /// <summary>
    /// Called when this object is disabled.
    /// </summary>
    private void OnDisable()
    {
        BindControls(false);
    }

    /// <summary>
    /// Called once per frame.
    /// </summary>
    private void Update()
    {
        UpdateAim();
    }

    #endregion


    #region Public API

    /// <summary>
    /// Makes the character shoot.
    /// If the character is currently shooting (its cooldown is running), the action is cancelled.
    /// If the hit object has a Shootable component, triggers OnShot event on that component.
    /// </summary>
    public void DoShoot()
    {
        // If the character is already shooting (its cooldown is running) or the action is frozen, cancel update
        if (IsShooting || m_FreezeShoot) { return; }

        Vector3 aimVector = AimVector.normalized;

        // Start the cooldown coroutine
        m_ShootCooldownCoroutine = StartCoroutine(ApplyShootCooldown(m_ShootRange, m_ShootCooldown));
        // Call OnShoot event
        m_ShootEvents.OnShoot.Invoke(new ShootInfos
        {
            origin = transform.position,
            direction = aimVector,
            range = m_ShootRange,
            cooldown = m_ShootCooldown,
            damages = m_ShootDamages
        });

        // If the shot hit something
        if(Physics.Raycast(transform.position, aimVector, out RaycastHit rayHit, m_ShootRange, m_ShootableObjectsLayer))
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
            m_ShootEvents.OnHitTarget.Invoke(infos);

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
                Debug.DrawLine(transform.position, transform.position + aimVector * rayHit.distance, Color.red, m_DebugLineDuration);
            }
            #endif
        }
        #if UNITY_EDITOR
        else if(m_EnableDebugLines)
        {
            Debug.DrawLine(transform.position, transform.position + aimVector * m_ShootRange, Color.red, m_DebugLineDuration);
        }
        #endif

        m_LastAimVector = aimVector;
    }

    /// <summary>
    /// Returns the aim vector, depending on the selected aim mode.
    /// </summary>
    public Vector3 AimVector
    {
        get
        {
            if(m_AimingType == EShootAim.TransformRight)
            {
                return transform.right;
            }
            else
            {
                Vector3 point = AimPosition;
                point.z = m_AimWithMouseZPosition;
                return point - transform.position;
            }
        }
    }

    /// <summary>
    /// Gets the aim position. If in edit mode, returns the character direction * shoot range.
    /// </summary>
    public Vector3 AimPosition
    {
        get
        {
#if UNITY_EDITOR
            if (!UnityEditor.EditorApplication.isPlaying)
                return transform.position + transform.right * m_ShootRange;
#endif
            return m_AimPosition;
        }
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

    /// <summary>
    /// Called when the character shoots (even if no target is hit)
    /// </summary>
    public ShootInfosEvent OnShoot
    {
        get { return m_ShootEvents.OnShoot; }
    }

    /// <summary>
    /// Called when the aiming vector changes
    /// </summary>
    public AimInfosEvent OnUpdateAim
    {
        get { return m_ShootEvents.OnUpdateAim; }
    }

    /// <summary>
    /// Called when a target is hit
    /// </summary>
    public HitInfosEvent OnHitTarget
    {
        get { return m_ShootEvents.OnHitTarget; }
    }

    #endregion


    #region Private Methods

    /// <summary>
    /// Binds actions to the user inputs.
    /// </summary>
    /// <param name="_Bind">If false, unbind actions.</param>
    private void BindControls(bool _Bind = true)
    {
        if (_Bind)
        {
            m_ShootAction.performed += DoShoot;
            m_AimPositionAction.performed += SetAimPosition;

            m_ShootAction.Enable();
            m_AimPositionAction.Enable();
        }
        else
        {
            m_ShootAction.performed -= DoShoot;
            m_AimPositionAction.performed -= SetAimPosition;

            m_ShootAction.Disable();
            m_AimPositionAction.Disable();
        }
    }

    /// <summary>
    /// CUpdates the aim vector.
    /// Triggers OnUpdateAim if the vector changes
    /// </summary>
    private void UpdateAim()
    {
        Vector3 aimVector = AimVector.normalized;
        if(aimVector != m_LastAimVector)
        {
            m_ShootEvents.OnUpdateAim.Invoke(new AimInfos
            {
                origin = transform.position,
                direction = aimVector,
                range = m_ShootRange
            });
        }

        m_LastAimVector = aimVector;
    }

    /// <summary>
    /// Makes the character shoot.
    /// If the character is currently shooting (its cooldown is running), the action is cancelled.
    /// If the hit object has a Shootable component, triggers OnShot event on that component.
    /// </summary>
    /// <param name="_Context">The current input context, from the InputAction delegate binding.</param>
    private void DoShoot(InputAction.CallbackContext _Context)
    {
        DoShoot();
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

    /// <summary>
    /// Sets the aim position, using the input context value.
    /// </summary>
    /// <param name="_Context">The current input context, from the InputAction delegate binding.</param>
    private void SetAimPosition(InputAction.CallbackContext _Context)
    {
        m_AimPosition = Camera.main.ScreenToWorldPoint(_Context.ReadValue<Vector2>());
    }

    #endregion


    #region Debug

    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if(!m_DrawAimVector) { return; }

        Gizmos.color = Color.yellow;

        // Draw mouse pointer position if "aim with mouse" mode enabled
        if (m_AimingType == EShootAim.AimWithMouse)
        {
            Vector3 point = AimPosition;
            point.z = m_AimWithMouseZPosition;

            Gizmos.DrawWireSphere(point, .5f);
        }
        // Draw aim vector
        Gizmos.DrawLine(transform.position, transform.position + AimVector.normalized * m_ShootRange);
    }
    #endif

    #endregion

}