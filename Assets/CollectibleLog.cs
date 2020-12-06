using UnityEngine;
public class CollectibleLog : MonoBehaviour
{
    public Collectible collectible;

    public void OnCollectListener(CollectInfos infos)
    {
        Debug.Log("Collectible has been collected at " + infos.position + ", and offered " + infos.score + " points.");
    }
}