# RinneTransformation

## Overview
The `RinneTransformation` script is responsible for applying a series of transformations to a point defined in Rinne coordinates (a specific input space). It performs scaling, rotation, and projection transformations to convert the Rinne coordinates into a Cartesian coordinate system. This script is integrated into a Unity project and is designed to visualize the transformed coordinates in the Unity environment. The transformations are applied continuously in the `Update` method, allowing for real-time visualization of the changes.

## Variables

- `rinneCoordinates`: An array of floats representing the initial coordinates in Rinne space. In this example, it is initialized to `{ 2.0f, 1.5f, -0.5f }`.

- `scalingMatrix`: A 2D array (matrix) that defines the scaling transformation factors for each axis. It is initialized with specific scaling factors for the X, Y, and Z axes.

- `rotationMatrix`: A 2D array (matrix) that defines the rotation transformation. In this case, it represents a 90-degree rotation around the Y-axis.

- `projectionMatrix`: A 2D array (matrix) that defines the perspective projection transformation applied to the coordinates.

- `cartesianOutput`: A `Vector3` variable that holds the final transformed coordinates in Cartesian space.

## Functions

- `Start()`: This Unity lifecycle method initializes the Rinne coordinates, scaling matrix, rotation matrix, and projection matrix when the script is first run.

- `Update()`: This Unity lifecycle method is called once per frame. It triggers the application of the Rinne transformation by calling the `ApplyRinneTransformation()` method.

- `ApplyRinneTransformation()`: This method performs the full transformation pipeline. It sequentially applies scaling, rotation, and projection to the Rinne coordinates and converts the result into a `Vector3` for Cartesian representation.

- `MatrixMultiply(float[,] matrix, float[] vector)`: This helper method performs matrix-vector multiplication. It takes a matrix and a vector as input and returns the resulting vector after the multiplication.

- `OnDrawGizmos()`: This Unity method is used for visual debugging. If the application is running, it visualizes the transformed Cartesian output as a red sphere in the scene.

- `OnGUI()`: This Unity method is called for rendering and handling GUI events. It displays the Cartesian output on the screen as a label for easy reference during runtime.