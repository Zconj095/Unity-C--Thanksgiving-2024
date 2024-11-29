# HyperCogniCortex

## Overview
The `HyperCogniCortex` script is a Unity MonoBehaviour that manages a collection of hyperdimensional properties, referred to as "Dimensions". Each dimension can be tuned dynamically during the game based on user input. The script initializes several dimensions with specific parameters and allows the user to adjust the "Cognition Intensity" dimension while also providing a status report of all dimensions when requested.

## Variables
- **Dimensions**: A list of `HyperDimension` objects that represent various dimensions such as Time, Space, Energy, and Cognition Intensity. This list is initialized in the `Start` method with specific values for each dimension.

## Functions
- **Start()**: This method is called when the script is first run. It initializes the `Dimensions` list with four predefined `HyperDimension` instances, each with a name, initial value, minimum value, and maximum value.

- **Update()**: This method is called once per frame. It checks for user input:
  - If the 'T' key is pressed, it tunes the "Cognition Intensity" dimension by a specified delta value.
  - If the 'D' key is pressed, it logs the current status of all dimensions to the console.

- **TuneDimension(string dimensionName, float delta)**: This public method takes a dimension name and a delta value as parameters. It searches for the specified dimension within the `Dimensions` list and adjusts its value by the delta. If the dimension is found, it logs the new value to the console.

- **GetDimensionStatus()**: This public method returns a string representation of all dimensions in the `Dimensions` list, formatted for easy reading. It is used to provide the current status of all dimensions when requested.