using UnityEngine;

///<summary>
/// Represents an object that can respawn after dying. Note that in this project "respawn" just mean teleport the object to a defined
/// position.
///</summary>
[HelpURL("https://github.com/DaCookie/empty-platformer/blob/master/Docs/respawner.md")]
public class Respawner : MonoBehaviour
{

	[SerializeField, Tooltip("Defines the position to go when respawned. By default, uses this GameObject's Transform component, or the first Transform of the Spawn Positions list if not empty.")]
    private Transform m_SpawnPosition = null;

	[SerializeField, Tooltip("Defines all possible respawn positions. Use RespawnRandom() to respawn on a default spawn position.")]
    private Transform[] m_SpawnPositions = null;

    // Called when this character respawns.
    [SerializeField]
    private SpawnInfosEvent m_OnRespawn = new SpawnInfosEvent();

    ///<summary>
    /// Called when this component is loaded.
    ///</summary>
    private void Awake()
    {
        if (m_SpawnPosition == null)
            m_SpawnPosition = m_SpawnPositions.Length > 0 && m_SpawnPositions[0] != null ? m_SpawnPositions[0] : transform;
    }

    ///<summary>
    /// Makes this object respawn to its Spawn Position.
    ///</summary>
    public void Respawn()
    {
        RespawnAt(m_SpawnPosition.position);
    }

    ///<summary>
    /// Makes this object respawn at the given position.
    ///</summary>
    public void RespawnAt(Vector3 _Position)
    {
        Vector3 lastPosition = transform.position;
        transform.position = _Position;

        m_OnRespawn.Invoke(new SpawnInfos { lastPosition = lastPosition, spawnPosition = _Position, target = gameObject });
    }

    /// <summary>
    /// Make this object respawns to its Spawn Position after the given delay.
    /// </summary>
    public void RespawnDelayed(float _Delay)
    {
        Invoke(nameof(this.Respawn), _Delay);
    }

    /// <summary>
    /// Make this object respawns at a random position.
    /// </summary>
    public void RespawnRandom()
    {
        if(m_SpawnPositions.Length > 0)
        {
            int randomIndex = Random.Range(0, m_SpawnPositions.Length);
            RespawnAt(m_SpawnPositions[randomIndex].position);
        }
        else
        {
            Respawn();
        }
    }

    /// <summary>
    /// Make this object respawns at a random position after the given delay.
    /// </summary>
    public void RespawnRandomDelayed(float _Delay)
    {
        Invoke(nameof(this.RespawnRandom), _Delay);
    }

    /// <summary>
    /// Called when this character respawns.
    /// </summary>
    public SpawnInfosEvent OnRespawn
    {
        get { return m_OnRespawn; }
    }

}