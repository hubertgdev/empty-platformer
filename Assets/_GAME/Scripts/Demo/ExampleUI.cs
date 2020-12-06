using UnityEngine;
using UnityEngine.UI;

///<summary>
/// This component is placed on the PlayerUI prefab, and displays the player's score and remaining lives.
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

    public void UpdateHealth(int _RemainingLives)
    {
        m_LivesCount.text = LIVES_PREFIX + _RemainingLives;
    }

    public void UpdateScore(ScoringInfos _Infos)
    {
        m_Score.text = SCORE_PREFIX + _Infos.score;
    }

    public void UpdateScore(int _Score)
    {
        m_Score.text = SCORE_PREFIX + _Score;
    }

}