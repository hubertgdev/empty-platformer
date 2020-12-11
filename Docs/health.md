# Empty Platformer project - Documentation - `Health`

Represents the health (as number of lives) of a character. Adding this component to an object means it can take damages and die.

![`Health` component inspector](./images/health.png)

## Parameters

- **Max Number Of Lives**: Defines the maximum number of lives for this character.
- **Number Of Lives At Start**: Defines the number of lives of this character when the game starts or its health is reset.

## Events

### `On Lose Lives` (`DamagesInfos`)

Called when the character lose one or more lives. Sends informations about the number of lives lost and the remaining lives.

```cs
public struct DamagesInfos
{
    // The number of lives lost when the character took damages.
    public int livesLost;

    // The current number of remaining lives of the character.
    public int remainingLives;
}
```

### `On Remaining Lives Change` (`int`)

Called when the number of remaining lives changes. Note that this event is called even when that number is increased or decreased. Sends the current number of remaining lives.

### `OnDie` (`DeathInfos`)

Called when the character dies (has no remaining lives).

```cs
public struct DeathInfos
{
    // The entity that has just died.
    public GameObject dead;

    // The position where the entity died.
    public Vector3 position;
}
```

## Public API

### Methods

#### `RemoveLives()`

```cs
public void RemoveLives(HitInfos _HitInfos);
public void RemoveLives(int _Quantity);
```

Decrease the number of lives by the given amount.

#### `GainLives()`

```cs
public void GainLives(int _Quantity)
```

Increase the number of lives by the given amount.

#### `ResetHealth()`

```cs
public void ResetHealth()
```

Resets the number of lives to its original value (defined with the *`Number Of Lives At Start`* parameter).

### Accessors

#### `IsDead`

```cs
public bool IsDead { get; }
```

Checks if this character is dead (has no remaining lives).

#### `RemainingLives`

```cs
public int RemainingLives { get; set; }
```

Gets/sets the number of remaining lives.

#### `MaxNumberOfLives`

```cs
public int MaxNumberOfLives { get; }
```

Gets the maximum number of lives this character can have.

#### `OnLoseLives`

```cs
public DamagesInfosEvent OnLoseLives { get; }
```

Called when the character lose one or more lives. Sends informations about the number of lives lost and the remaining lives.

#### `OnRemainingLivesChange`

```cs
public IntEvent OnRemainingLivesChange { get; }
```

Called when the number of remaining lives changes. Note that this event is called even when that number is increased or decreased. Sends the current number of remaining lives.

#### `OnDie`

```cs
public UnityEvent OnDie { get; }
```

Called when the character dies (has no remaining lives).

---

[<= Back to summary](./README.md)