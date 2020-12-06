using UnityEngine;

///<summary>
/// Represents a shootable object (handles Shoot action result).
///</summary>
[HelpURL("https://github.com/DaCookie/empty-platformer/blob/master/Docs/shootable.md")]
public class Shootable : MonoBehaviour
{

    [SerializeField]
    [Tooltip("Defines the amount of score that the shooter can earn when this entity is shot. Set this to 0 if this entity is just a destructible element")]
    private int m_ScoreByShot = 100;

    [SerializeField]
    [Tooltip("Called when this entity is shot.")]
    private HitInfosEvent m_OnShot = new HitInfosEvent();

    /// <summary>
    /// Called when this entity is shot.
    /// </summary>
    public void NotifyHit(HitInfos _HitInfos)
    {
        m_OnShot.Invoke(_HitInfos);
    }

    /// <summary>
    /// Gets the amount of score earned when this entity is shot.
    /// </summary>
    public int ScoreByShot
    {
        get { return m_ScoreByShot; }
    }

    /// <summary>
    /// Called when this entity is shot.
    /// </summary>
    public HitInfosEvent OnShot
    {
        get { return m_OnShot; }
    }

}