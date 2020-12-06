# Empty Platformer project - Documentation - `Contact Damages`

Represents the capacity of an object to take damages by touching another entity.

![`Contact Damages` component inspector](./images/contact-damages.png)

## Parameters

- **Health**: By default, gets the Health component on this GameObject.
- **Collider**: By default, gets the BoxCollider component on this GameObject.
- **Damages Layer**: Defines the layer of the objects that can damage this entity. This should be the enemies layers.
- **Damage On Contact**: Defines the number of lives to lose when touching a damaging object.
- **Invincibility Duration**: Sets the duration of invincibility after touching a damaging object.

## Events

See the [*How to use events, and react to what happens in the game?*](./howto-events.md) guide to learn how to use `UnityEvent`s.

### `On Contact` (`HitInfos`)

Called when the character takes damages.

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

### `On Begin Invincibility` (`float`)

Called after the character took damages, and enter in invincible state. Sends the invincibility duration.

### `On Update Invincibility` (`float`)

Called when the invincible state updates. Sends the invincibility timer ratio over the total duration.

### `On Stop Invincibility`

Called when the character lose its invincible state.

## Public API

### Accessors

#### `IsInvincible`

```cs
public bool IsInvincible { get; }
```

Checks if this collectible has been collected.

#### `InvincibilityRatio`

```cs
public float InvincibilityRatio { get; }
```

Gets the invincibility timer ratio over invincibility duration.

#### `OnContact`

```cs
public HitInfosEvent OnContact { get; }
```

Called when the character takes damages.

#### `OnBeginInvincibility`

```cs
public FloatEvent OnBeginInvincibility { get; }
```

Called after the character took damages, and enter in invincible state. Sends the invincibility duration.

#### `OnUpdateInvincibility`

```cs
public FloatEvent OnUpdateInvincibility { get; }
```

Called when the invincible state updates. Sends the invincibility timer ratio over the total duration.

#### `OnStopInvincibility`

```cs
public UnityEvent OnStopInvincibility { get; }
```

Called when the character lose its invincible state.

---

[<= Back to summary](./README.md)