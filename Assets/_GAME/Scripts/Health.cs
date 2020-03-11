using UnityEngine;
using UnityEngine.Events;

///<summary>
/// Represents the health (as number of lives) of a character.
/// Adding this component to an object means it can take damages and die.
///</summary>
public class Health : MonoBehaviour
{

    #region Properties

    [Header("Settings")]

    [SerializeField]
    private bool m_IsInvincible = false;

    [SerializeField]
    private int m_MaxNumberOfLives = 3;

    [SerializeField]
    private int m_NumberOfLivesAtStart = 3;

    [Header("Events")]

    // Called when the character lose one or more lives
    // Sends informations about the number of lives lost and the remaining lives
    [SerializeField]
    private DamagesInfosEvent m_OnLoseLives = new DamagesInfosEvent();

    // Called when the character dies (has no remaining lives)
    [SerializeField]
    private UnityEvent m_OnDie = new UnityEvent();

    // Number of current remaining lives
    private int m_RemainingLives = 0;

    #endregion

    #region Lifecycle

    /// <summary>
    /// Called when this component is loaded.
    /// </summary>
    private void Awake()
    {
        m_RemainingLives = m_NumberOfLivesAtStart;
    }

    /// <summary>
    /// Called when the scene is loaded.
    /// </summary>
    private void Start()
    {
        ApplyDeath();
    }

    #endregion


    #region Public Methods

    /// <summary>
    /// Decrease the number of lives by the given amount.
    /// </summary>
    public void RemoveLives(int _Quantity)
    {
        // If the character is not dead already
        if (!IsDead)
        {
            // Decrease remaining lives, but ensures the new value is not less than 0 lives
            m_RemainingLives = Mathf.Max(0, m_RemainingLives - _Quantity);
            m_OnLoseLives.Invoke(new DamagesInfos { livesLost = _Quantity, remainingLives = m_RemainingLives });

            ApplyDeath();
        }
    }

    public void ResetHealth()
    {
        m_RemainingLives = m_NumberOfLivesAtStart;
        ApplyDeath();
    }

    /// <summary>
    /// Checks if this character is dead (has no remaining lives).
    /// </summary>
    public bool IsDead
    {
        get { return m_RemainingLives <= 0; }
    }

    /// <summary>
    /// Gets the number of remaining lives.
    /// </summary>
    public int RemainingLives
    {
        get { return m_RemainingLives; }
    }

    /// <summary>
    /// Gets the maximum number of lives this character can have.
    /// </summary>
    public int MaxNumberOfLives
    {
        get { return m_MaxNumberOfLives; }
    }

    #endregion


    #region Internal Methods

    /// <summary>
    /// If remaining lives are less or equal to 0, call OnDie event.
    /// </summary>
    private void ApplyDeath()
    {
        if(m_RemainingLives <= 0)
        {
            m_RemainingLives = 0;
            m_OnDie.Invoke();
        }
    }

    #endregion

}