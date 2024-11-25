using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class TemperatureControl
{
    private float temperature = 0.7f;

    public void AdjustTemperature(float delta)
    {
        temperature = Mathf.Clamp(temperature + delta, 0.1f, 1.5f);
    }

    public float GetTemperature() => temperature;
}
