using System;
using UnityEngine;

public class LerpValue
{
    public float Current { get; private set; }
    public float Speed 
    {
        get => speed;
        set => speed = Mathf.Max(0f, value);
    }

    private float last = 0f;
    private float target = 0f;
    private float interpolationValue = 0f;
    private float speed = 0f;
    private readonly InterpolationMethod interpolationMethod = InterpolationMethod.Linear;
    private readonly Func<LerpValue, float> setTarget;

    public LerpValue(Func<LerpValue, float> setTarget)
    {
        this.setTarget = setTarget;
        Init();
    }

    public LerpValue(Func<LerpValue, float> setTarget, float startValue)
        : this(setTarget)
    {
        last = startValue;
    }

    public LerpValue(Func<LerpValue, float> setTarget, InterpolationMethod interpolationMethod) 
        : this(setTarget)
    {
        this.interpolationMethod = interpolationMethod;
    }

    public LerpValue(Func<LerpValue, float> setTarget, float startValue, InterpolationMethod interpolationMethod)
        : this(setTarget, startValue)
    {
        this.interpolationMethod = interpolationMethod;
    }

    private void Init()
    {
        last = Current;
        target = setTarget(this);
        interpolationValue = 0f;
    }

    public void Update()
    {
        // Update the interpolationValue
        interpolationValue = Mathf.Clamp01(interpolationValue + Time.deltaTime * speed);

        // Update the current value based on the interpolationValue
        float t = interpolationValue;

        switch (interpolationMethod)
        {
            case InterpolationMethod.Cosine:
                t = (1 - Mathf.Cos(t * Mathf.PI)) / 2;
                break;
        }

        Current = Mathf.Lerp(last, target, t);

        // Re-Initialize LerpValue if the interpolationValue has reached its target
        if (interpolationValue == 1)
        {
            Init();
        }
    }
}
