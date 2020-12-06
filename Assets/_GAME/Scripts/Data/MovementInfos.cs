using UnityEngine;

///<summary>
/// Represents informations about a movement.
///</summary>
[System.Serializable]
public struct MovementInfos
{
	// The current speed of the character.
	public float speed;

	// The position of the character before it has moved.
	public Vector3 lastPosition;

	// The position of the character after it has moved.
	public Vector3 currentPosition;
}