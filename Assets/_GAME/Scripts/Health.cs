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
        // Called when the character lose one or more lives. Sends informations about the number of lives lost and the remaining lives.
        public DamagesInfosEvent OnLoseLives = new DamagesInfosEvent();

        // Called when the number of remaining lives changes. Note that this event is called even when that number is increased or
        // decreased. Sends the current number of remaining lives.
        public IntEvent OnRemainingLivesChange = new IntEvent();

        // Called when the character dies (has no remaining lives).
        public UnityEvent OnDie = new UnityEvent();
    }

    #endregion


    #region Properties

    [Header("Settings")]

    [SerializeField]
    [Tooltip("Defines the maximum number of lives for this character")]
    private int m_MaxNumberOfLives = 3;

    [SerializeField]
    [Tooltip("Defines the number of lives of this character when the game starts or its health is reset")]
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
    /// Decrease the number of lives using the given Hit Infos.
    /// </summary>
    public void RemoveLives(HitInfos _HitInfos)
    {
        RemoveLives(_HitInfos.damages);
    }

    /// <summary>
    /// Decrease the number of lives by the given amount.
    /// </summary>
    public void RemoveLives(int _Quantity)
    {
        // If the character is not dead already
        if (!IsDead)
        {
            RemainingLives -= _Quantity;
            m_HealthEvents.OnLoseLives.Invoke(new DamagesInfos { livesLost = _Quantity, remainingLives = RemainingLives });
        }
    }

    /// <summary>
    /// Increase the number of lives by the given amount.
    /// </summary>
    public void GainLives(int _Quantity)
    {
        RemainingLives += _Quantity;
    }

    /// <summary>
    /// Resets the number of lives to its original value (defined with the Number Of Lives At Start parameter).
    /// </summary>
    public void ResetHealth()
    {
        RemainingLives = m_NumberOfLivesAtStart;
    }

    /// <summary>
    /// Checks if this character is dead (has no remaining lives).
    /// </summary>
    public bool IsDead
    {
        get { return m_RemainingLives <= 0; }
    }

    /// <summary>
    /// Gets/sets the number of remaining lives.
    /// </summary>
    public int RemainingLives
    {
        get { return m_RemainingLives; }
        set
        {
            m_RemainingLives = Mathf.Clamp(value, 0, m_MaxNumberOfLives);
            OnRemainingLivesChange.Invoke(m_RemainingLives);
            ApplyDeath();
        }
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
    /// Called when the number of remaining lives changes. Note that this event is called even when that number is increased or decreased.
    /// Sends the current number of remaining lives.
    /// </summary>
    public IntEvent OnRemainingLivesChange
    {
        get { return m_HealthEvents.OnRemainingLivesChange; }
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