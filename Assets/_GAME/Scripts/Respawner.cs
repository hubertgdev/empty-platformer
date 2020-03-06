using UnityEngine;

///<summary>
/// Represents an object that can respawn after dying.
///</summary>
public class Respawner : MonoBehaviour
{

	[SerializeField, Tooltip("By default, use this GameObject's Transform component")]
    private Transform m_SpawnPosition = null;

    // Called when this character respawns.
    [SerializeField]
    private SpawnInfosEvent m_OnRespawn = new SpawnInfosEvent();

    ///<summary>
    /// Called when this component is loaded.
    ///</summary>
    private void Awake()
    {
        if(m_SpawnPosition == null) { m_SpawnPosition = GetComponent<Transform>(); }
    }

    ///<summary>
    /// Makes this object respawn to its Spawn Position.
    /// NOTE: The object is simply moved to that position.
    ///</summary>
    public void Respawn()
    {
        Vector3 lastPosition = transform.position;
        transform.position = m_SpawnPosition.position;

        m_OnRespawn.Invoke(new SpawnInfos { lastPosition = lastPosition, spawnPosition = m_SpawnPosition.position });
    }

}