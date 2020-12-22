# Empty Platformer project - Documentation - `Patroller Controller`

Represents a patrolling entity.

This character will go toward its target, then go back, continuously. Note that this controller doesn't use physics at all, and won't be able to detect obstacles.

![`Patroller Controller` component inspector](./images/patroller-controller.png)

## Parameters

- **Speed**: Defines the speed of the object (in units/second).
- **Path Direction**: Defines the first direction to go.
- **Distance**: Defines the length of the path to follow.
- **Collider**: Reference to the characters collider. By default, use this `GameObject`'s collider.
- **Freeze Controller**: If `true`, the Patroller is not updated.

## Events

### `On Change Direction` (`Vector3`)

Called when the patroller changes its direction (so it arrives at the end of its path). Sends the new direction vector of the entity.

### `On Update Move` (`MovementInfos`)

Called each frame this patroller moves.

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

---

[<= Back to summary](./README.md)