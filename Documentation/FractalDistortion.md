# FractalDistortion

## Overview
The `FractalDistortion` script is designed to create a visual representation of fractal distortion in a 3D space using Unity. It initializes a grid of distortion values and applies a fractal algorithm to modify these values iteratively. The result is visualized by generating spheres at each grid point, where the height of the sphere corresponds to the distortion value. This script can be utilized in a larger codebase for procedural generation of terrain or effects that require complex surface variations.

## Variables

- `alpha` (float): A parameter that influences the quadratic transformation of the distortion values in the grid. Default value is 0.8.
- `beta` (float): A parameter that adds a sinusoidal perturbation to the distortion values. Default value is 0.6.
- `gamma` (float): A constant offset added to the distortion values. Default value is 0.1.
- `gridSize` (int): The size of the distortion grid, defined as 256 x 256.
- `distortionGrid` (float[,]): A 2D array that holds the distortion values for each point in the grid.

## Functions

- `Start()`: This is a Unity lifecycle method that initializes the distortion grid when the script starts. It calls the `GenerateFractalDistortion` method to populate the grid with initial values.

- `GenerateFractalDistortion()`: This method populates the `distortionGrid` with random values and applies a fractal distortion algorithm over 10 iterations. It modifies each value in the grid based on a combination of its previous value, a perturbation calculated using sine and cosine functions, and the parameters `alpha`, `beta`, and `gamma`. After processing, it calls the `VisualizeFractal` method.

- `VisualizeFractal()`: This method creates visual representations of the distortion values by generating spheres at each grid point. The position of each sphere is determined by the grid coordinates and the corresponding distortion value, while the color of each sphere is interpolated between blue and red based on the distortion value. The size of each sphere is set to a uniform scale of 0.1.