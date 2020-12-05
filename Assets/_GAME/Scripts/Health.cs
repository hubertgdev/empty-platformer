using UnityEngine;
using UnityEngine.Events;

///<summary>
/// Represents the health (as number of lives) of a character.
/// Adding this component to an object means it can take damages and die.
///</summary>
[HelpURL("https://github.com/DaCookie/empty-platformer/blob/master/Docs/health.md")]
public class Health : MonoBehaviour
{

    #region Subclasses

    [System.Serializable]
    private class HealthEvents
    {
        // Called when the character lose one or more lives
        // Sends informations about the number of lives lost and the remaining lives
        public DamagesInfosEvent OnLoseLives = new DamagesInfosEvent();

        // Called when the character dies (has no remaining lives)
        public UnityEvent OnDie = new UnityEvent();
    }

    #endregion


    #region Properties

    [Header("Settings")]

    [SerializeField]
    private int m_MaxNumberOfLives = 3;

    [SerializeField]
    private int m_NumberOfLivesAtStart = 3;

    [Header("Events")]

    [SerializeField]
    private HealthEvents m_HealthEvents = new HealthEvents();

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


    #region Public API

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
            m_HealthEvents.OnLoseLives.Invoke(new DamagesInfos { livesLost = _Quantity, remainingLives = m_RemainingLives });

            ApplyDeath();
        }
    }

    /// <summary>
    /// Decrease the number of lives using the given Hit Infos.
    /// </summary>
    public void RemoveLives(HitInfos _HitInfos)
    {
        RemoveLives(_HitInfos.damages);
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

    /// <summary>
    /// Called when the character lose one or more lives. Sends informations about the number of lives lost and the remaining lives.
    /// </summary>
    public DamagesInfosEvent OnLoseLives
    {
        get { return m_HealthEvents.OnLoseLives; }
    }

    /// <summary>
    /// Called when the character dies (has no remaining lives).
    /// </summary>
    public UnityEvent OnDie
    {
        get { return m_HealthEvents.OnDie; }
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
            m_HealthEvents.OnDie.Invoke();
        }
    }

    #endregion

}