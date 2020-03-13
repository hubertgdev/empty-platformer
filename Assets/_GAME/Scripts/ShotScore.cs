using UnityEngine;

///<summary>
/// Represents a character that can give score when shot.
///</summary>
public class ShotScore : MonoBehaviour
{

    [SerializeField]
    private int m_ScoreByShot = 100;

    public int ScoreByShot
    {
        get { return m_ScoreByShot; }
    }

}