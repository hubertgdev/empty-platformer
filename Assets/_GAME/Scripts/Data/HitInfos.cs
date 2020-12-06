using UnityEngine;

///<summary>
/// Contains informations about a Shoot action.
///</summary>
[System.Serializable]
public struct HitInfos
{
	// The GameObject of the entity that shot the target
	public GameObject shooter;

	// The GameObject of the entity that has just been shot
	public GameObject target;

	// The start position of the shot
	public Vector3 origin;

	// The position of the shot impact
    public Vector3 impact;

	// The distance from the shot origin to the impact point
    public float distance;

	// The damages amount to inflict to the target
	public int damages;
}