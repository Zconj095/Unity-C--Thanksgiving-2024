# CorticalActivity

## Overview
The `CorticalActivity` script is designed to simulate and visualize neural activity in a grid format within a Unity environment. It creates a 10x10 grid representing cortical activity, initializes it with random values, and provides methods to retrieve and update this activity. The script also includes a visual representation of the neural activity using Unity's Gizmos, allowing developers to see how the activity varies across the grid.

## Variables

- `gridSize` (int): Defines the dimensions of the grid, set to 10 for a 10x10 grid.
- `neuralActivity` (float[,]): A 2D array that stores the neural activity values for each cell in the grid.

## Functions

- `Start()`: This Unity lifecycle method initializes the `neuralActivity` array with the specified `gridSize` and calls the `InitializeActivity()` function to populate it with random values.

- `InitializeActivity()`: Populates the `neuralActivity` array with random float values ranging from 0.1 to 1.0. This simulates the initial state of cortical activity.

- `GetActivity()`: Returns the current state of the `neuralActivity` array, allowing other parts of the code to access the neural activity data.

- `UpdateActivity(float[,] newActivity)`: Updates the `neuralActivity` array with a new set of activity values provided as a parameter.

- `OnDrawGizmos()`: This method is called by Unity to allow for custom visualization in the editor. It draws cubes in the scene view based on the values in the `neuralActivity` array, using color interpolation between black and white to represent the intensity of activity. If `neuralActivity` is null, it exits without drawing anything.