using UnityEngine;

///<summary>
/// Contains informations about a Shoot action.
///</summary>
[System.Serializable]
public struct HitInfos
{
	public GameObject shooter;
	public GameObject target;
    public Vector3 impact;
    public float distance;
	public int damages;
}