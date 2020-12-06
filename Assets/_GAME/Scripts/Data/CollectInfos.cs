using UnityEngine;

///<summary>
/// Represents informations about a character collecting an item.
///</summary>
[System.Serializable]
public struct CollectInfos
{
    // The amount of score the collectible offers when collected
    public int score;

    // The position of the collectible when collected
    public Vector3 position;

    // The reference to the object that collected the collectible
    public GameObject collector;
}