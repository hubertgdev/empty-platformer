using UnityEngine;

///<summary>
/// Contains informations about an entity that has just died.
///</summary>
[System.Serializable]
public struct DeathInfos
{
    // The entity that has just died.
    public GameObject dead;

    // The position where the entity died.
    public Vector3 position;
}