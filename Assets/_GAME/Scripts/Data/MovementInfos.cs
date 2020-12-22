using UnityEngine;

///<summary>
/// Represents informations about a movement.
///</summary>
[System.Serializable]
public struct MovementInfos
{
	// The entity that has just moved.
	public GameObject entity;

	// The current speed of the character.
	public float speed;

	// The position of the character before it has moved.
	public Vector3 lastPosition;

	// The position of the character after it has moved.
	public Vector3 currentPosition;

	// The orientation of the character when it has moved.
	public Vector3 orientation;
}