# TemperatureAdjuster

## Overview
The `TemperatureAdjuster` script is a Unity MonoBehaviour that manages the adjustment of a temperature value within a specified range. It allows for both increasing and decreasing the temperature while ensuring that the temperature remains within defined minimum and maximum limits. Additionally, it provides a method for smoothly transitioning the temperature towards a target value over time. This script can be useful in scenarios where temperature control is necessary, such as in simulation or game mechanics involving environmental effects.

## Variables

- `currentTemperature`: A float representing the current temperature value. It is initialized to 0.7 and can be adjusted within the defined limits.
- `minTemperature`: A float indicating the minimum allowable temperature. It is set to 0.3.
- `maxTemperature`: A float indicating the maximum allowable temperature. It is set to 1.2.
- `smoothingRate`: A float that defines the rate at which temperature adjustments occur. It is initialized to 0.05, allowing for gradual changes.

## Functions

- `IncreaseTemperature(float amount)`: This method takes a float parameter `amount` and increases the `currentTemperature` by that amount. It uses `Mathf.Clamp` to ensure that the new temperature does not exceed `minTemperature` or `maxTemperature`.

- `DecreaseTemperature(float amount)`: Similar to `IncreaseTemperature`, this method decreases the `currentTemperature` by a specified `amount`, also clamping the value to remain within the defined limits.

- `GetCurrentTemperature()`: This method returns the current temperature value as a float.

- `SmoothAdjustTowards(float targetTemperature)`: This method takes a float parameter `targetTemperature` and adjusts the `currentTemperature` towards this target value smoothly over time. It uses `Mathf.Lerp` to interpolate between the current and target temperatures based on the `smoothingRate` multiplied by `Time.deltaTime`, allowing for frame-rate independent adjustments.