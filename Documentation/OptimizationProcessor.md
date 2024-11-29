# OptimizationProcessor

## Overview
The `OptimizationProcessor` script is designed to optimize GameObjects in a Unity scene based on a specified tag. When attached to a GameObject, it searches for all other GameObjects that share the same tag and applies optimization techniques to their components. This script is particularly useful for managing performance by ensuring that only relevant objects are processed, thus aiding in resource optimization.

## Variables

- **optimizationTag (string)**: This public variable allows the user to specify a tag that filters which GameObjects should be optimized. The default value is "Optimizable". If this tag is not set, an error message is logged.

## Functions

- **Start()**: This is a Unity lifecycle method that is called on the frame when the script is enabled. It checks if the `optimizationTag` is set. If not, it logs an error and stops the execution. If the tag is valid, it initiates the optimization process by calling `OptimizeObjects()`.

- **OptimizeObjects(string tag)**: This method takes a string parameter representing the tag. It finds all GameObjects that have the specified tag. If no objects are found, it logs a warning. For each found object, it logs the object's name and calls `ProcessOptimization()`.

- **ProcessOptimization(GameObject obj)**: This method analyzes all components attached to a given GameObject. It retrieves the components and logs each component's type. It then calls `OptimizeComponent()` for each component to apply optimization logic.

- **OptimizeComponent(Component component)**: This method accepts a component and performs two main tasks: 
  1. It identifies public, writable properties of the component that can be optimized, based on specific criteria defined in `IsOptimizable()`. It attempts to get the current value of each property, optimizes it using `GetOptimizedValue()`, and sets the new value back to the property. Any exceptions during this process are logged as warnings.
  2. It also identifies and invokes methods of the component that start with "Optimize", logging each invocation and any exceptions that occur.

- **IsOptimizable(PropertyInfo property)**: This helper method determines if a property can be optimized based on its type. Currently, it considers properties of type `float` and `int` as optimizable.

- **GetOptimizedValue(PropertyInfo property, object currentValue)**: This method applies simple optimization logic to clamp numerical values within a specified range (0 to 100 for both `float` and `int`). If the property type is not optimizable, it returns the original value unchanged.