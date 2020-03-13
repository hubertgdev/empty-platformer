using UnityEngine;
using UnityEngine.Events;

///<summary>
/// Represents a collectioble item.
///</summary>
public class Collectible : MonoBehaviour
{

    [SerializeField, Tooltip("Defines the amount of score to get when collecting this object")]
	private int m_Score = 100;

    // Called when a character collects this item.
    [SerializeField]
    private CollectInfosEvent m_OnCollect = new CollectInfosEvent();

    // Defines if this collectible has been collected
    private bool m_Collected = false;
    
    ///<summary>
    /// Checks if this collectible has been collected.
    ///</summary>
    public bool IsCollected
    {
        get { return m_Collected; }
    }

    ///<summary>
    /// Gets the amount of score to get when collecting this object.
    ///</summary>
    public int Score
    {
        get { return m_Score; }
    }
    
    ///<summary>
    /// Resets this collectible state (it can be collected).
    ///</summary>
    public void ResetState()
    {
        m_Collected = false;
    }

    ///<summary>
    /// Called when an object enters in this object's trigger.
    ///</summary>
    private void OnTriggerEnter(Collider _Other)
    {
        if(m_Collected) { return; }

        Scorer scorer = _Other.GetComponent<Scorer>();
        // If the entering entity can score
        if (scorer != null)
        {
            scorer.GainScore(m_Score);
        }

        m_Collected = true;
        m_OnCollect.Invoke(new CollectInfos()
        {
            score = m_Score,
            position = transform.position,
            collector = _Other.gameObject
        });
    }

}