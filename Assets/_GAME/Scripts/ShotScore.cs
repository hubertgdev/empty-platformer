using UnityEngine;

///<summary>
/// Represents a character that can give score when shot.
///</summary>
[HelpURL("https://github.com/DaCookie/empty-platformer/blob/master/Docs/shot-score.md")]
public class ShotScore : MonoBehaviour
{

    [SerializeField]
    private int m_ScoreByShot = 100;

    public int ScoreByShot
    {
        get { return m_ScoreByShot; }
    }

}