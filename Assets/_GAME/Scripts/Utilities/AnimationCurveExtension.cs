using UnityEngine;

///<summary>
/// Extensions for UnityEngine.Rect struct.
///</summary>
[HelpURL("https://github.com/DaCookie/empty-platformer/blob/master/Docs/animation-curve-extension.md")]
public static class AnimationCurveExtension
{

    #region Properties

    // Default Keyframe value
    public static readonly Keyframe DEFAULT_KEYFRAME = new Keyframe(0f, 0f);

    #endregion


    #region Public Methods

    /// <summary>
    /// Calculates the duration of an Animation Curve, using its first and last keyframes on X axis.
    /// </summary>
    /// <returns>Returns the calculated duration of the Animation Curve. Note that it will return 0 if the Animation Curve counts less
    /// than 2 keyframes.</returns>
    public static float ComputeDuration(this AnimationCurve _Curve)
    {
        float minTime = GetFirstKeyframe(_Curve).time;
        float maxTime = GetLastKeyframe(_Curve).time;
        return maxTime - minTime;
    }

    /// <summary>
    /// Calculates the value range of an Animation Curve, using its lowest and highest keyframes on Y axis.
    /// </summary>
    /// <returns>Returns the calculated value range of the Animation Curve. Note that it will return 0 if the Animation Curve counts
    /// less than 2 keyframes.</returns>
    public static float ComputeRange(this AnimationCurve _Curve)
    {
        float minRange = GetMinKeyframe(_Curve).value;
        float maxRange = GetMaxKeyframe(_Curve).value;
        return maxRange - minRange;
    }

    /// <summary>
    /// Gets the first keyframe on X axis of the given Animation Curve.
    /// </summary>
    /// <returns>Returns the found keyframe, or the default one if there's no keyframe on the given curve.</returns>
    public static Keyframe GetFirstKeyframe(this AnimationCurve _Curve)
    {
        int count = _Curve.keys.Length;
        if (count > 0)
        {
            int minTimeIndex = 0;
            for (int i = 1; i < count; i++)
            {
                if (_Curve.keys[i].time < _Curve.keys[minTimeIndex].time)
                {
                    minTimeIndex = i;
                }
            }
            return _Curve.keys[minTimeIndex];
        }
        return DEFAULT_KEYFRAME;
    }

    /// <summary>
    /// Gets the minimum time of this curve.
    /// </summary>
    /// <returns>Returns the found minimum time, or default keyframe's if there's no keyframe on the given curve.</returns>
    public static float GetMinTime(this AnimationCurve _Curve)
    {
        return _Curve.GetFirstKeyframe().time;
    }

    /// <summary>
    /// Gets the last keyframe on X axis of the given Animation Curve.
    /// </summary>
    /// <returns>Returns the found keyframe, or the default one if there's no keyframe on the given curve.</returns>
    public static Keyframe GetLastKeyframe(this AnimationCurve _Curve)
    {
        int count = _Curve.keys.Length;
        if (count > 0)
        {
            int maxTimeIndex = 0;
            for (int i = 1; i < count; i++)
            {
                if (_Curve.keys[i].time > _Curve.keys[maxTimeIndex].time)
                {
                    maxTimeIndex = i;
                }
            }
            return _Curve.keys[maxTimeIndex];
        }
        return DEFAULT_KEYFRAME;
    }

    /// <summary>
    /// Gets the maximum time of this curve.
    /// </summary>
    /// <returns>Returns the found maximum time, or default keyframe's if there's no keyframe on the given curve.</returns>
    public static float GetMaxTime(this AnimationCurve _Curve)
    {
        return _Curve.GetLastKeyframe().time;
    }

    /// <summary>
    /// Gets the lowest keyframe on Y axis of the given Animation Curve.
    /// </summary>
    /// <returns>Returns the found keyframe, or the default one if there's no keyframe on the given curve.</returns>
    public static Keyframe GetMinKeyframe(this AnimationCurve _Curve)
    {
        int count = _Curve.keys.Length;
        if (count > 0)
        {
            int minValueIndex = 0;
            for (int i = 1; i < count; i++)
            {
                if (_Curve.keys[i].value < _Curve.keys[minValueIndex].value)
                {
                    minValueIndex = i;
                }
            }
            return _Curve.keys[minValueIndex];
        }
        return DEFAULT_KEYFRAME;
    }

    /// <summary>
    /// Gets the minimum value of this curve.
    /// </summary>
    /// <returns>Returns the found minimum value, or default keyframe's if there's no keyframe on the given curve.</returns>
    public static float GetMinValue(this AnimationCurve _Curve)
    {
        return _Curve.GetMinKeyframe().value;
    }

    /// <summary>
    /// Gets the highest keyframe on Y axis of the given Animation Curve.
    /// </summary>
    /// <returns>Returns the found keyframe, or the default one if there's no keyframe on the given curve.</returns>
    public static Keyframe GetMaxKeyframe(this AnimationCurve _Curve)
    {
        int count = _Curve.keys.Length;
        if (count > 0)
        {
            int maxValueIndex = 0;
            for (int i = 1; i < count; i++)
            {
                if (_Curve.keys[i].value > _Curve.keys[maxValueIndex].value)
                {
                    maxValueIndex = i;
                }
            }
            return _Curve.keys[maxValueIndex];
        }
        return DEFAULT_KEYFRAME;
    }

    /// <summary>
    /// Gets the maximum value of this curve.
    /// </summary>
    /// <returns>Returns the found maximum value, or default keyframe's if there's no keyframe on the given curve.</returns>
    public static float GetMaxValue(this AnimationCurve _Curve)
    {
        return _Curve.GetMaxKeyframe().value;
    }

    #endregion

}
