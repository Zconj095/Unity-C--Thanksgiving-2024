using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class TemperatureAdjuster
{
    private float currentTemperature = 0.7f;
    private float minTemperature = 0.3f;
    private float maxTemperature = 1.2f;
    private float smoothingRate = 0.05f; // Gradual adjustment rate

    public void IncreaseTemperature(float amount)
    {
        currentTemperature = Mathf.Clamp(currentTemperature + amount, minTemperature, maxTemperature);
    }

    public void DecreaseTemperature(float amount)
    {
        currentTemperature = Mathf.Clamp(currentTemperature - amount, minTemperature, maxTemperature);
    }

    public float GetCurrentTemperature() => currentTemperature;

    public void SmoothAdjustTowards(float targetTemperature)
    {
        currentTemperature = Mathf.Lerp(currentTemperature, targetTemperature, smoothingRate * Time.deltaTime);
    }
}
