# Empty Platformer project - Documentation - `Shoot`

Makes a playable character able to shoot things.

![`Shoot` component inspector](./images/shoot.png)

## Parameters

- **Shoot Action**: Defines the user inputs (using [the new *Input System*](https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0)) to make the player shoot.
- **Aim Position Action**: Defines the user inputs (using [the new *Input System*](https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0)) to let the player aim toward a position. Used only if *`Aiming Type`* parameter is set to *Aim With Mouse*.
- **Aiming Type**: Defines if the player is aiming using the `tranform.right` vector of the object, or using mouse pointer.
- **Shoot Range**: Defines the range of the shoot action.
- **Shoot Cooldown**: Defines the cooldown of the shoot action.
- **Shootable Objects Layer**: Defines the physics layer of objects that can be shot.
- **Shoot Damages**: Defines the number of lives the shoot action inflcts.
- **Aim With Mouse Z Position**: Defines the Z position of the pointer when aiming with mouse.
- **Freeze Shoot**: If `true`, disables the Shoot action.

### Debug parameters

- **Enable Debug Lines**: Defines if debug lines are drawn when the character shoots.
- **Debug Line Duration**: Defines the lifetime of debug lines if they're enabled.
- **Draw Aim Vector**: If true, draws a gizmo line that represents the aim vector.

## Events

### `On Shoot` (`ShootInfos`)

Called when the character shoots (even if no target is hit).

```cs
public struct ShootInfos
{
	// The start point of the shot.
	public Vector3 origin;

	// The direction of the shot.
	public Vector3 direction;

	// The range of the shot.
	public float range;

	// The shoot action cooldown (in seconds).
	public float cooldown;

	// The amount of damages to inflict when the shot touches a shootable entity.
	public int damages;
}
```

### `On Update Aim` (`AimInfos`)

Called when the aiming vector changes.

```cs
public struct AimInfos
{
    // The start point of the shot.
    public Vector3 origin;

    // The current aim vector.
    public Vector3 direction;

    // The range of the shot.
    public float range;
}
```

### `On Hit Target` (`HitInfos`)

Called when a target is hit.

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

#### `DoShoot()`

```cs
public void DoShoot()
```

Makes the character shoot. If the character is currently shooting (its cooldown is running), the action is cancelled. If the hit object has a Shootable component, triggers OnShot event on that component.

### Accessors

#### `AimVector`

```cs
public Vector3 AimVector { get; }
```

Returns the aim vector, depending on the selected aim mode.

#### `AimPosition`

```cs
public Vector3 AimPosition { get; }
```

Gets the aim position. If in edit mode, returns the character direction * shoot range.

#### `IsShooting`

```cs
public bool IsShooting { get; }
```

Checks if this character is shooting (it has a running cooldown).

#### `FreezeShoot`

```cs
public bool FreezeShoot { get; }
```

Freezes the shoot action.

#### `OnShoot`

```cs
public ShootInfosEvent OnShoot { get; }
```

Called when the character shoots (even if no target is hit).

#### `OnUpdateAim`

```cs
public AimInfosEvent OnUpdateAim { get; }
```

Called when the aiming vector changes.

#### `OnUpdateAim`

```cs
public HitInfosEvent OnHitTarget { get; }
```

Called when a target is hit.

---

[<= Back to summary](./README.md)