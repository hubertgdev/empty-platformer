using UnityEngine;

///<summary>
/// Represents informations about a movement.
///</summary>
[System.Serializable]
public struct MovementInfos
{
	public float speed;

	public Vector3 lastPosition;
	public Vector3 currentPosition;
}