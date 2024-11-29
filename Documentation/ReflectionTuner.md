# ReflectionTuner

## Overview
The `ReflectionTuner` script is designed to interact with a `HyperCogniCortex` object in Unity. Its main function is to dynamically reflect on the properties of the `Cortex` and adjust specific dimensions based on user input. This script listens for a key press event (specifically the "R" key) and, when detected, it attempts to tune a specified dimension of the `Cortex` by a defined amount. This dynamic tuning capability allows for flexible adjustments during runtime, enhancing the interactivity of the application.

## Variables
- **Cortex**: A public reference to an instance of the `HyperCogniCortex` class. This variable must be assigned in the Unity Inspector and is essential for the script to operate correctly.

## Functions
- **Start()**: This is a Unity lifecycle method that is called before the first frame update. It checks if the `Cortex` variable has been assigned. If not, it logs an error message to the console and stops further execution of the script.

- **Update()**: This Unity lifecycle method is called once per frame. It checks for user input, specifically if the "R" key has been pressed. If the key is detected, it calls the `ReflectAndTune` method with predefined parameters.

- **ReflectAndTune(string listName, string dimensionName, float delta)**: This public method uses reflection to access a field from the `Cortex` object. It takes three parameters:
  - `listName`: The name of the field in the `Cortex` that contains a list of dimensions.
  - `dimensionName`: The name of the specific dimension to be tuned.
  - `delta`: A float value that indicates how much to adjust the dimension.
  
  The method retrieves the specified list of dimensions, searches for the dimension with the given name, and calls its `Tune` method to apply the adjustment. If successful, it logs a message indicating the new value of the dimension after tuning.