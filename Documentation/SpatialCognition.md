# SpatialCognition

## Overview
The `SpatialCognition` script is designed to create and manage a two-dimensional spatial map within a Unity environment. This spatial map is a grid of values that represent different spatial features, which can be utilized for various applications such as AI navigation, environmental interactions, or game mechanics. The script initializes this grid with random values and provides a method to retrieve the influence of specific grid positions. Additionally, it visualizes the spatial map in the Unity editor using Gizmos.

## Variables
- **gridSize (int)**: This public variable defines the dimensions of the spatial map, determining how many rows and columns the grid will have. The default value is set to 10.
- **spatialMap (float[,])**: This private two-dimensional array holds the spatial feature values for each position in the grid. It is initialized in the `Start` method and filled with random values.

## Functions
- **Start()**: This Unity lifecycle method is called before the first frame update. It initializes the `spatialMap` array based on the specified `gridSize` and calls the `InitializeSpatialMap` method to populate it with random values.

- **InitializeSpatialMap()**: This private method populates the `spatialMap` array with random float values between 0.0 and 1.0. It iterates through each cell in the grid and assigns a random value to represent spatial features.

- **GetSpatialInfluence(int x, int y)**: This public method retrieves the spatial influence value at the specified grid coordinates (x, y). It checks if the provided coordinates are within the bounds of the grid. If they are, it returns the corresponding value from the `spatialMap`; if not, it returns 0.0 to indicate an out-of-bounds request.

- **OnDrawGizmos()**: This Unity method is called to draw Gizmos in the scene view. It visualizes the spatial map by drawing cubes for each grid position. The color of each cube is determined by the corresponding value in the `spatialMap`, with colors interpolating between black and white based on the value. This provides a visual representation of the spatial features in the editor.