# Empty Platformer project - Documentation - `Shootable`

Represents a shootable object (handles Shoot action result).

![`Shootable` component inspector](./images/shootable.png)

## Parameters

- **Score By Shot**: Defines the amount of score that the shooter can earn when this entity is shot. Set this to 0 if this entity is just a destructible element.

## Events

### `On Shot` (`HitInfos`)

Called when this entity is shot.

```cs
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
```

## Public API

### Methods

#### `NotifyHit()`

```cs
public void NotifyHit(HitInfos _HitInfos)
```

Called from [`Shoot` component](./shoot.md) when this entity is shot.

### Accessors

#### `ScoreByShot`

```cs
public int ScoreByShot { get; }
```

Gets the amount of score earned when this entity is shot.

#### `OnShot`

```cs
public HitInfosEvent OnShot { get; }
```

Called when this entity is shot.

---

[<= Back to summary](./README.md)