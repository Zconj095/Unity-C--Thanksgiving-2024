using System;
using UnityEngine; // Add this namespace for Mathf and other Unity-specific utilities

[Serializable]
public class HyperDimension
{
    public string Name;
    public float Value; // Current value of the dimension
    public float MinValue; // Minimum bound
    public float MaxValue; // Maximum bound

    public HyperDimension(string name, float value, float minValue, float maxValue)
    {
        Name = name;
        Value = Mathf.Clamp(value, minValue, maxValue); // Mathf now works
        MinValue = minValue;
        MaxValue = maxValue;
    }

    public void Tune(float delta)
    {
        Value = Mathf.Clamp(Value + delta, MinValue, MaxValue); // Mathf now works
    }

    public override string ToString()
    {
        return $"{Name}: {Value}";
    }
}
