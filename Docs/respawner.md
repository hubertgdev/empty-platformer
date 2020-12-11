# Empty Platformer project - Documentation - `Respawner`

Represents an object that can respawn after dying. Note that in this project "respawn" just mean teleport the object to a defined position.

![`Respawner` component inspector](./images/respawner.png)

## Parameters

- **Spawn Position**: Defines the position to go when respawned. By default, uses this `GameObject`'s `Transform` component, or the first `Transform` of the *`Spawn Positions`* list if not empty.
- **Spawn Positions**: Defines all possible respawn positions. Use RespawnRandom() to respawn on a default spawn position.

## Events

### `On respawn` (`SpawnInfos`) 

Called when this character respawns.

```cs
public struct SpawnInfos
{
	// The object that has just respawned.
	public GameObject target;

	// The position of the character before it respawns.
	public Vector3 lastPosition;

	// The position of the character after it respawns.
	public Vector3 spawnPosition;
}
```

## Public API

### Methods

#### `Respawn()`

```cs
public void Respawn()
```

Makes this object respawn to its Spawn Position.

#### `RespawnAt()`

```cs
public void RespawnAt(Vector3 _Position)
```

Makes this object respawn at the given position.

#### `RespawnDelayed()`

```cs
public void RespawnDelayed(float _Delay)
```

Make this object respawns to its Spawn Position after the given delay.

#### `RespawnRandom()`

```cs
 public void RespawnRandom()
```

Make this object respawns at a random position.

#### `RespawnRandomDelayed()`

```cs
public void RespawnRandomDelayed(float _Delay)
```

Make this object respawns at a random position after the given delay.

### Accessors

#### `OnRespawn`

```cs
public SpawnInfosEvent OnRespawn { get; }
```

Called when this character respawns.

---

[<= Back to summary](./README.md)