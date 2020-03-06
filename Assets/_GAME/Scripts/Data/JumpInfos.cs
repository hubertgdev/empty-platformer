using UnityEngine;

///<summary>
/// Represents informations about a character when it begins to jump.
///</summary>
[System.Serializable]
public struct JumpInfos
{
    public Vector3 jumpOrigin;
    // Movement axis giving the direction of the character's movement when it triggers Jump action
    public float movement;
}