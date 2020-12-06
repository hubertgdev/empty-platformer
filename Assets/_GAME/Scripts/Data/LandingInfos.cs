using UnityEngine;

///<summary>
/// Represents informations about landing after a character has fallen.
///</summary>
[System.Serializable]
public struct LandingInfos
{
    // Time (in seconds) from the character was falling.
    public float fallingTime;

    // The position of the character when it lands.
    public Vector3 landingPosition;
}