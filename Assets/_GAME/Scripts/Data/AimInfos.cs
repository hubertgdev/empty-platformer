using UnityEngine;

///<summary>
/// Represents informations about aiming.
///</summary>
[System.Serializable]
public struct AimInfos
{
    // The start point of the shot.
    public Vector3 origin;

    // The current aim vector.
    public Vector3 direction;

    // The range of the shot.
    public float range;
}