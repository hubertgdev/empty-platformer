# Empty Platformer project - Documentation - `Example UI`

This component is placed on the *PlayerUI* prefab, and displays score and remaining lives.

## Public API

### Methods

#### `UpdateHealth()`

```cs
public void UpdateHealth(DamagesInfos _Infos);
public void UpdateHealth(Health _Health);
public void UpdateHealth(int _RemainingLives);
```

Updates the remaining lives UI.

#### `UpdateScore()`

```cs
public void UpdateScore(ScoringInfos _Infos);
public void UpdateScore(int _Score);
```

Updates the score UI.

---

[<= Back to summary](./README.md)