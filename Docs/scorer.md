# Empty Platformer project - Documentation - `Scorer`

Represents an entity that can earn score.

![`Scorer` component inspector](./images/scorer.png)

## Parameters

- **Score**: The current score amount of this entity.

## Events

### `On Score Change` (`ScoringInfos`)

Called when the score value changes.

```cs
public struct ScoringInfos
{
    // The amount of score gained
    public int gain;

    // The new score value
    public int score;

    // The score value, before gain calculation
    public int lastScore;
}
```

## Public API

### Methods

#### `GainScore()`

```cs
public void GainScore(int _Amount)
```

Increase the current score value by the given amount.

### Accessors

#### `Score`

```cs
public int Score { get; }
```

Gets the current score value.

#### `OnScoreChange`

```cs
public ScoringInfosEvent OnScoreChange { get; }
```

Called when the score value changes.

---

[<= Back to summary](./README.md)