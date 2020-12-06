# Empty Platformer project - Documentation - `Collectible`

Represents a collectioble item.

![`Collectible` component inspector](./images/collectible.png)

## Parameters

- **Score**: Defines the amount of score to get when collecting this object.

## Events

See the [*How to use events, and react to what happens in the game?*](./howto-events.md) guide to learn how to use `UnityEvent`s.

### `On Collect` (`CollectInfos`)

Triggered when the object is collected.

```cs
public struct CollectInfos
{
    // The amount of score the collectible offers when collected
    public int score;

    // The position of the collectible when collected
    public Vector3 position;

    // The reference to the object that collected the collectible
    public GameObject collector;
}
```

## Public API

### Methods

#### `ResetState()`

```cs
public void ResetState()
```

Resets this collectible state (it can be collected).

### Accessors

#### `IsCollected`

```cs
public bool IsCollected { get; }
```

Checks if this collectible has been collected.

#### `Score`

```cs
public int Score { get; }
```

Gets the amount of score to get when collecting this object.

#### `OnCollect`

```cs
public CollectInfosEvent OnCollect { get; }
```

Called when a character collects this item.

---

[<= Back to summary](./README.md)