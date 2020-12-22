# Empty Platformer project - Documentation - `Platformer Controller`

A platformer controller for a playable character in a side-scrolling platformer.

![`Platformer Controller` component inspector](./images/platformer-controller.png)

## Parameters

- **Move X Action**: Defines the user inputs (using [the new *Input System*](https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0)) to make the player move along the X axis.
- **Jump Action**: Defines the user inputs (using [the new *Input System*](https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0)) to make the player jump.
- **Speed**: Maximum speed of the character (in units/second).
- **Movement Obstacles Detection Layer**: Defines the physics layers that can block player movement.
- **Collider**: Reference to the character's collider. By default, use the Collider on this GameObject.
- **Jump Curve**: Defines how the jump should behave, where X represents the duration of the jump and Y axis represents the height.
- **Gravity Scale**: Defines the falling speed of the character. Tip: in arcade-like games, the gravity scale is usually 3x the "real" gravity or more, which makes the gameplay more dynamic.
- **Hold Input Mode**: If false, press the Jump button once to run the jump curve completely. If true, the jump curve is read until the Jump button is released or until the curve is complete (Super Mario-like jump).
- **Min Jump Duration**: Used only if "*Hold Input Mode*" is set to true. Defines the minimum jump duration when the jump input is pressed for one frame only.
- **Jump Obstacles Detection Layer**: Defines the physics layers that can block player jump.
- **Freeze Movement**: If true, disables movement action.
- **Freeze Jump**: If true, disables jump action.

## Events

### `On Begin Move` (`MovementInfos`)

Called when the character starts moving.

```cs
public struct MovementInfos
{
	// The entity that has just moved.
	public GameObject entity;

	// The current speed of the character.
	public float speed;

	// The position of the character before it has moved.
	public Vector3 lastPosition;

	// The position of the character after it has moved.
	public Vector3 currentPosition;

	// The orientation of the character when it has moved.
	public Vector3 orientation;
}
```

### `On Update Move` (`MovementInfos`)

Called each frame while the character is moving (even if there's an obstacle in front of it).

```cs
public struct MovementInfos
{
	// The entity that has just moved.
	public GameObject entity;

	// The current speed of the character.
	public float speed;

	// The position of the character before it has moved.
	public Vector3 lastPosition;

	// The position of the character after it has moved.
	public Vector3 currentPosition;

	// The orientation of the character when it has moved.
	public Vector3 orientation;
}
```

### `On Stop Move`

Called when the character stops moving.

### `On Stop Move` (`Vector3`)

Called when the character changes its movment direction. Sends the new direction vector.

### `On Begin Jump` (`JumpInfos`)

Called when the player press the Jump button and the Jump action begins to apply.

```cs
public struct JumpInfos
{
    // The position of the character where it started its jump
    public Vector3 jumpOrigin;

    // Movement axis (between -1 and 1), giving the direction of the character's movement when it triggers Jump action
    public float movement;
}
```

### `On Update Jump` (`JumpUpdateInfos`)

Called each frame while the character is ascending after a Jump.

```cs
public class JumpUpdateInfos
{
    // The total time (in seconds) the character is jumping.
    public float jumpTime;

    // Ratio of the elasped jump time, and the total jump duration.
    public float jumpRatio;

    // The position of the character where it started its jump.
    public Vector3 jumpOrigin;
}
```

### `On Stop Jump`

Called when the character stops jumping by releasing the Jump button (if Hold Input Mode enabled), by encountering an obstacle above him, or by completing the Jump curve.

### `On Land` (`LandingInfos`)

Called when the character lands on the floor after falling down.

```cs
public struct LandingInfos
{
    // Time (in seconds) from the character was falling.
    public float fallingTime;

    // The position of the character when it lands.
    public Vector3 landingPosition;
}
```

### `On Hit Ceiling` (`Vector3`)

Called when the character hit something above him. Sends the hit position.

### `On Fall` (`float`)

Called when the character is falling down.

## Public API

### Methods

#### `ResetController()`

```cs
public void ResetController()
```

Resets the controller state (cancels jump and unfreeze actions).

### Accessors

#### `IsMoving`

```cs
public bool IsMoving { get; }
```

Checks if the character is moving.

#### `MovementDirection`

```cs
public Vector3 MovementDirection { get; }
```

Gets the current movement direction vector of the character.

#### `IsJumping`

```cs
public bool IsJumping { get; }
```

Checks if the character is juming.

#### `IsOnFloor`

```cs
public bool IsOnFloor { get; }
```

Checks if the character is on the floor.

#### `IsFalling`

```cs
public bool IsFalling { get; }
```

Checks if the character is falling (not jumping and not on the floor).

#### `JumpRatio`

```cs
public float JumpRatio { get; }
```

Gets the jump timer ratio, over the jump total duration.

#### `YVelocity`

```cs
public float YVelocity { get; }
```

Gets the Y velocity of the character.

#### `LastMovementAxis`

```cs
public float LastMovementAxis { get; }
```

Gets the last X movement input axis.

#### `FreezeController`

```cs
public bool FreezeController { get; set; }
```

Freezes/unfreezes both jump and movement.

#### `FreezeMovement`

```cs
public bool FreezeMovement { get; set; }
```

Freezes/unfreezes movement.

#### `FreezeJump`

```cs
public bool FreezeJump { get; set; }
```

Freezes/unfreezes jump.

#### `OnBeginMove`

```cs
public MovementInfosEvent OnBeginMove { get; }
```

Called when the character starts moving.

#### `OnUpdateMove`

```cs
public MovementInfosEvent OnUpdateMove { get; }
```

Called each frame the character is moving (even if there's an obstacle in front of it).

#### `OnStopMove`

```cs
public UnityEvent OnStopMove { get; }
```

Called when the character stops moving.

#### `OnChangeOrientation`

```cs
public Vector3Event OnChangeOrientation { get; }
```

Called when the character changes its movment direction.

#### `OnBeginJump`

```cs
public JumpInfosEvent OnBeginJump { get; }
```

Called when the player press the Jump button and the Jump action begins to apply.

#### `OnUpdateJump`

```cs
public JumpUpdateInfosEvent OnUpdateJump { get; }
```

Called each frame the character is ascending after a Jump.

#### `OnStopJump`

```cs
public UnityEvent OnStopJump { get; }
```

Called when the character stops jumping by releasing the Jump button (if Hold Input Mode enabled), by encountering an obstacle above him, or by completing the Jump curve.

#### `OnLand`

```cs
public LandingInfosEvent OnLand { get; }
```

Called when the character lands on the floor after falling down.

#### `OnHitCeiling`

```cs
public Vector3Event OnHitCeiling { get; }
```

Called when the character hit something above him. Sends the hit position.

#### `OnFall`

```cs
public FloatEvent OnFall { get; }
```

Called when the character is falling down.

---

[<= Back to summary](./README.md)