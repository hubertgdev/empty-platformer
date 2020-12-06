using UnityEngine;

///<summary>
/// Represents informations about a character when it begins to jump.
///</summary>
[System.Serializable]
public struct JumpInfos
{
    // The position of the character where it started its jump
    public Vector3 jumpOrigin;

    // Movement axis (between -1 and 1), giving the direction of the character's movement when it triggers Jump action
    public float movement;
}