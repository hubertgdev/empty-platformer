using UnityEngine;

///<summary>
/// 
///</summary>
public class Scorer : MonoBehaviour
{

    [SerializeField]
    private int m_Score = 0;

    [SerializeField]
    private ScoringInfosEvent m_OnScoreChange = new ScoringInfosEvent();

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

    public ScoringInfosEvent OnScoreChange
    {
        get { return m_OnScoreChange; }
    }

    public int Score
    {
        get { return m_Score; }
    }

}