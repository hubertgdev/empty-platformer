# Empty Platformer project - Documentation - `Animation Curve Extension`

Extensions for UnityEngine.AnimationCurve instances.

Note that these methods comes from the [Muffin Dev Core library](https://gitlab.com/muffin-dev/unity/open-source/core-library).

## Usage example

These methods are C# extensions, and adds methods to the `AnimationCurve` API.

```cs
using UnityEngine;
public class AnimationCurveExtensionDemo : MonoBehaviour
{
    public AnimationCurve curve = new AnimationCurve();

    private void Start()
    {
        Debug.Log("Curve duration: " + curve.ComputeDuration() + "s");
    }
}
```

## Public API

### Constants

#### `DEFAULT_KEYFRAME`

```cs
public static readonly Keyframe DEFAULT_KEYFRAME = new Keyframe(0f, 0f);
```

Default Keyframe value.

### Methods

#### `ComputeDuration()`

```cs
public static float ComputeDuration(this AnimationCurve _Curve)
```

Calculates the duration of an `AnimationCurve`, using its first and last keyframes on X axis.

Returns the calculated duration of the `AnimationCurve`. Note that it will return 0 if the `AnimationCurve` counts less than 2 keyframes.

#### `ComputeRange()`

```cs
public static float ComputeRange(this AnimationCurve _Curve)
```

Calculates the value range of an `AnimationCurve`, using its lowest and highest keyframes on Y axis.

Returns the calculated value range of the `AnimationCurve`. Note that it will return 0 if the `AnimationCurve` counts less than 2 keyframes.

#### `GetFirstKeyframe()`

```cs
public static Keyframe GetFirstKeyframe(this AnimationCurve _Curve)
```

Gets the first keyframe on X axis of the given `AnimationCurve`.

Returns the found keyframe, or the default one if there's no keyframe on the given curve.

#### `GetMinTime()`

```cs
public static float GetMinTime(this AnimationCurve _Curve)
```

Gets the minimum time of this curve.

Returns the found minimum time, or default keyframe's if there's no keyframe on the given curve.

#### `GetLastKeyframe()`

```cs
public static Keyframe GetLastKeyframe(this AnimationCurve _Curve)
```

Gets the last keyframe on X axis of the given `AnimationCurve`.

Returns the found keyframe, or the default one if there's no keyframe on the given curve.

#### `GetMaxTime()`

```cs
public static float GetMaxTime(this AnimationCurve _Curve)
```

Gets the maximum time of this curve.

Returns the found maximum time, or default keyframe's if there's no keyframe on the given curve.

#### `GetMinKeyframe()`

```cs
public static Keyframe GetMinKeyframe(this AnimationCurve _Curve)
```

Gets the lowest keyframe on Y axis of the given `AnimationCurve`.

Returns the found keyframe, or the default one if there's no keyframe on the given curve.

#### `GetMinValue()`

```cs
public static float GetMinValue(this AnimationCurve _Curve)
```

Gets the minimum value of this curve.

Returns the found minimum value, or default keyframe's if there's no keyframe on the given curve.

#### `GetMaxKeyframe()`

```cs
public static Keyframe GetMaxKeyframe(this AnimationCurve _Curve)
```

Gets the highest keyframe on Y axis of the given `AnimationCurve`.

Returns the found keyframe, or the default one if there's no keyframe on the given curve.

#### `GetMaxKeyframe()`

```cs
public static float GetMaxValue(this AnimationCurve _Curve)
```

Gets the maximum value of this curve.

Returns the found maximum value, or default keyframe's if there's no keyframe on the given curve.

---

[<= Back to summary](./README.md)