using UnityEngine;
using UnityEngine.Events;

///<summary>
/// Represents a shootable object (catches Shoot action result).
///</summary>
[HelpURL("https://github.com/DaCookie/empty-platformer/blob/master/Docs/shootable.md")]
public class Shootable : MonoBehaviour
{

    [SerializeField]
    private HitInfosEvent m_OnShot = new HitInfosEvent();

    public void NotifyHit(HitInfos _HitInfos)
    {
        m_OnShot.Invoke(_HitInfos);
    }

    public HitInfosEvent OnShot
    {
        get { return m_OnShot; }
    }

}