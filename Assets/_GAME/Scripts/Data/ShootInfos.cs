using UnityEngine;

///<summary>
/// Contains informations about a Shoot action.
///</summary>
[System.Serializable]
public struct ShootInfos
{
	// The start point of the shot.
	public Vector3 origin;

	// The direction of the shot.
	public Vector3 direction;

	// The range of the shot.
	public float range;

	// The shoot action cooldown (in seconds).
	public float cooldown;

	// The amount of damages to inflict when the shot touches a shootable entity.
	public int damages;
}