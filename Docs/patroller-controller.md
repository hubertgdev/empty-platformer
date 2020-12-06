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

---

[<= Back to summary](./README.md)