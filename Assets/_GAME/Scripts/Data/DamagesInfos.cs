///<summary>
/// Represents the loss of one or more lives.
///</summary>
[System.Serializable]
public struct DamagesInfos
{
    // The number of lives lost when the character took damages.
    public int livesLost;

    // The current number of remaining lives of the character.
    public int remainingLives;
}