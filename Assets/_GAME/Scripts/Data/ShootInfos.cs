using UnityEngine;

///<summary>
/// Contains informations about a Shoot action.
///</summary>
[System.Serializable]
public struct ShootInfos
{
	public Vector3 origin;
	public Vector3 direction;
	public float range;
	public float cooldown;
	public int damages;
}