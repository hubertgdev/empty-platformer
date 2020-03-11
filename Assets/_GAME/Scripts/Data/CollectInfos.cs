using UnityEngine;

///<summary>
/// Represents informations about a character collecting an item.
///</summary>
[System.Serializable]
public struct CollectInfos
{
    public int score;
    public Vector3 position;
    public GameObject collector;
}