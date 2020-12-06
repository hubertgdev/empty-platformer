using UnityEngine;

///<summary>
/// Represents an entity that can earn score.
///</summary>
[HelpURL("https://github.com/DaCookie/empty-platformer/blob/master/Docs/scorer.md")]
public class Scorer : MonoBehaviour
{

    [SerializeField]
    [Tooltip("The current score amount of this entity.")]
    private int m_Score = 0;

    [SerializeField]
    [Tooltip("Called when the score value changes.")]
    private ScoringInfosEvent m_OnScoreChange = new ScoringInfosEvent();

    /// <summary>
    /// Increase the current score value by the given amount.
    /// </summary>
    public void GainScore(int _Amount)
    {
        int lastScore = m_Score;

        m_Score += _Amount;
        m_Score = Mathf.Max(0, m_Score);

        m_OnScoreChange.Invoke(new ScoringInfos
        {
            gain = _Amount,
            lastScore = lastScore,
            score = m_Score
        });
    }

    /// <summary>
    /// Gets the current score value.
    /// </summary>
    public int Score
    {
        get { return m_Score; }
    }

    /// <summary>
    /// Called when the score value changes.
    /// </summary>
    public ScoringInfosEvent OnScoreChange
    {
        get { return m_OnScoreChange; }
    }

}