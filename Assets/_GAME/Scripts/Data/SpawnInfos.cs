using UnityEngine;

///<summary>
/// Contains respawn informations.
///</summary>
[System.Serializable]
public struct SpawnInfos
{
	// The position of the character before it respawns.
	public Vector3 lastPosition;

	// The position of the character after it respawns.
	public Vector3 spawnPosition;
}