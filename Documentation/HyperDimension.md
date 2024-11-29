# HyperDimension

## Overview
The `HyperDimension` class represents a conceptual dimension with a name and a value that is bounded within a specified range. This class is designed to be used within a Unity project, leveraging Unity's `Mathf` utility to ensure that the value remains within defined limits (minimum and maximum). The class can be utilized in various gameplay mechanics where dimensions or attributes need to be dynamically adjusted while maintaining constraints.

## Variables
- `string Name`: The name of the hyperdimension, used for identification.
- `float Value`: The current value of the hyperdimension, which is the main focus of this class.
- `float MinValue`: The minimum allowable value for the hyperdimension.
- `float MaxValue`: The maximum allowable value for the hyperdimension.

## Functions
- **HyperDimension(string name, float value, float minValue, float maxValue)**: Constructor that initializes a new instance of the `HyperDimension` class with the specified name, value, minimum value, and maximum value. The `Value` is clamped to ensure it falls within the specified bounds.

- **void Tune(float delta)**: Adjusts the `Value` of the hyperdimension by a specified `delta`. The new value is clamped to ensure it remains within the defined minimum and maximum bounds.

- **override string ToString()**: Returns a string representation of the `HyperDimension`, displaying its name and current value in the format "Name: Value". This is useful for debugging and logging purposes.