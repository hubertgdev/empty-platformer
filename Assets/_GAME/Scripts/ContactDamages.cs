using UnityEngine;
using UnityEngine.Events;

///<summary>
/// Represents the capacity of an object to take damages by touching another entity.
///</summary>
public class ContactDamages : MonoBehaviour
{

    #region Properties

    [Header("Settings")]

    [SerializeField]
    private LayerMask m_DamagingLayer = 0;

    [SerializeField, Tooltip("Defines the number of lives to lose when touching a damaging object")]
    private int m_DamagesOnContact = 1;

    [SerializeField, Tooltip("Sets the duration of invincibility after touching a damaging object")]
    private float m_InvincibilityDuration = .4f;

    [SerializeField, Tooltip("By default, gets the Health component on this GameObject")]
    private Health m_Health = null;

    [SerializeField, Tooltip("By default, gets the BoxCollider component on this GameObject")]
    private BoxCollider m_Collider = null;

    [Header("Events")]

    [SerializeField]
    private HitInfosEvent m_OnContact = new HitInfosEvent();

    // Called after the character take damages without damages, and enter in invincible state
    // Sends the invincibility duration
    [SerializeField]
    private FloatEvent m_OnBeginInvincibility = new FloatEvent();

    // Called when the invincible state updates
    // Sends the invincibility timer ratio over the total duration
    [SerializeField]
    private FloatEvent m_OnUpdateInvincibility = new FloatEvent();

    // Called when the character lose its invincible state
    [SerializeField]
    private UnityEvent m_OnStopInvincibility = new UnityEvent();

    private float m_InvincibilityTimer = 0f;

    #endregion


    #region Lifecycle

    /// <summary>
    /// Called when this component is loaded.
    /// </summary>
    private void Awake()
    {
        if (m_Health == null) { m_Health = GetComponent<Health>(); }
        if (m_Collider == null) { m_Collider = GetComponent<BoxCollider>(); }
        m_InvincibilityTimer = m_InvincibilityDuration + 1f;
    }

    /// <summary>
    /// Called once per frame.
    /// </summary>
    private void Update()
    {
        // If the character is already dead, do nothing
        if(m_Health.IsDead) { return; }

        // Update the invincibility timer if needed
        if (m_InvincibilityTimer <= m_InvincibilityDuration)
        {
            m_InvincibilityTimer += Time.deltaTime;
            // If the invincibility state is finished
            if (m_InvincibilityTimer > m_InvincibilityDuration)
            {
                m_OnStopInvincibility.Invoke();
            }
            else
            {
                m_OnUpdateInvincibility.Invoke(InvincibilityRatio);
            }
        }

        // If the character is still invincible, don't check for contact damages
        if (IsInvincible) { return; }

        Collider[] contacts = Physics.OverlapBox(transform.position, Extents, Quaternion.identity, m_DamagingLayer);

        // If the character touches a damaging object
        if (contacts.Length != 0)
        {
            m_OnContact.Invoke(new HitInfos()
            {
                shooter = contacts[0].gameObject,
                target = gameObject,
                distance = 0f,
                damages = m_DamagesOnContact
            });

            // Apply damages
            m_Health.RemoveLives(m_DamagesOnContact);
            // If not dead, begins invincibility state
            if (!m_Health.IsDead)
            {
                m_InvincibilityTimer = 0f;
                m_OnBeginInvincibility.Invoke(m_InvincibilityDuration);
            }
        }
    }

    #endregion


    #region Public Methods

    /// <summary>
    /// Checks if the character is currently invincible.
    /// </summary>
    public bool IsInvincible
    {
        get { return m_InvincibilityTimer <= m_InvincibilityDuration; }
    }

    /// <summary>
    /// Gets the invincibility timer ratio over invincibility duration.
    /// </summary>
    public float InvincibilityRatio
    {
        get { return (m_InvincibilityDuration > 0f) ? Mathf.Clamp01(m_InvincibilityTimer / m_InvincibilityDuration) : 0f; }
    }

    #endregion


    #region Private Methods

    /// <summary>
    /// Gets the collider extents.
    /// If no collider set, returns Vector3.one.
    /// </summary>
    private Vector3 Extents
    {
        get { return (m_Collider != null) ? m_Collider.bounds.extents : Vector3.one; }
    }

    #endregion

}