using UnityEngine;

///<summary>
/// Represents informations about the jump state of a character.
///</summary>
[System.Serializable]
public struct JumpUpdateInfos
{
    // The total time (in seconds) the character is jumping.
    public float jumpTime;

    // Ratio of the elasped jump time, and the total jump duration.
    public float jumpRatio;

    // The position of the character where it started its jump.
    public Vector3 jumpOrigin;
}