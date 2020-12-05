using UnityEngine;

///<summary>
/// Represents an object that can respawn after dying.
///</summary>
[HelpURL("https://github.com/DaCookie/empty-platformer/blob/master/Docs/respawner.md")]
public class Respawner : MonoBehaviour
{

	[SerializeField, Tooltip("By default, use this GameObject's Transform component")]
    private Transform m_SpawnPosition = null;

	[SerializeField, Tooltip("Stores all possible positions. Use RespawnRandom() to respawn on a default spawn position.")]
    private Transform[] m_SpawnPositions = null;

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
        RespawnAt(m_SpawnPosition.position);
    }

    ///<summary>
    /// Makes this object respawn at the given position.
    /// NOTE: The object is simply moved to that position.
    ///</summary>
    public void RespawnAt(Vector3 _Position)
    {
        Vector3 lastPosition = transform.position;
        transform.position = _Position;

        m_OnRespawn.Invoke(new SpawnInfos { lastPosition = lastPosition, spawnPosition = _Position });
    }

    /// <summary>
    /// Make this object respawns to its Spawn Position after the given delay.
    /// NOTE: The object is simply moved to that position.
    /// </summary>
    public void RespawnDelayed(float _Delay)
    {
        Invoke(nameof(this.Respawn), _Delay);
    }

    /// <summary>
    /// Make this object respawns at a random position.
    /// NOTE: The object is simply moved to that position.
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
    /// NOTE: The object is simply moved to that position.
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