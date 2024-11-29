# TemperatureControl Script

## Overview
The `TemperatureControl` script is designed to manage and adjust a temperature value within a specified range in a Unity game environment. It allows for temperature adjustments through a public method and provides a way to retrieve the current temperature. This script can be utilized in various game scenarios where temperature management is essential, such as environmental effects, gameplay mechanics, or simulation features.

## Variables
- `temperature` (float): This private variable holds the current temperature value. It is initialized to `0.7f` and represents the temperature state within the control system.

## Functions
- `AdjustTemperature(float delta)`: This public method takes a float parameter `delta`, which represents the amount to adjust the current temperature. It modifies the `temperature` variable by adding `delta` to it and ensures that the resulting value remains within the range of `0.1f` to `1.5f` using `Mathf.Clamp`.

- `GetTemperature()`: This public method returns the current temperature value as a float. It provides a straightforward way to access the temperature state from other scripts or components.