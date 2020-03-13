using UnityEngine;

///<summary>
/// Contains informations about a score that has changed.
///</summary>
[System.Serializable]
public struct ScoringInfos
{
    // The amount of score gained
    public int gain;
    // The new score value
    public int score;
    // The score value, before gain calculation
    public int lastScore;
}