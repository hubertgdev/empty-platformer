using UnityEngine;
using UnityEngine.UI;

///<summary>
/// 
///</summary>
[HelpURL("https://github.com/DaCookie/empty-platformer/blob/master/Docs/exampe-ui.md")]
public class ExampleUI : MonoBehaviour
{

    private const string LIVES_PREFIX = "Remaining lives: ";
    private const string SCORE_PREFIX = "Score: ";

    [SerializeField]
    private Text m_LivesCount = null;

    [SerializeField]
    private Text m_Score = null;

    public void UpdateHealth(DamagesInfos _Infos)
    {
        m_LivesCount.text = LIVES_PREFIX + _Infos.remainingLives;
    }

    public void UpdateHealth(Health _Health)
    {
        m_LivesCount.text = LIVES_PREFIX + _Health.RemainingLives;
    }

    public void UpdateScore(ScoringInfos _Infos)
    {
        m_Score.text = SCORE_PREFIX + _Infos.score;
    }

}