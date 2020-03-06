using UnityEngine;

///<summary>
/// Represents informations about landing after a character has fallen.
///</summary>
[System.Serializable]
public struct LandingInfos
{
    // Time (in seconds) from the character was falling
    public float fallingTime;
    public Vector3 landingPosition;
}