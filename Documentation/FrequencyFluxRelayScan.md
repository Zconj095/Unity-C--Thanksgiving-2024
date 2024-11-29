# FrequencyFluxRelayScan

## Overview
The `FrequencyFluxRelayScan` script is designed to create a grid of cells in a 3D space and simulate a scanning process that evaluates a "flux" value for each cell based on its position. The script utilizes Unity's game object instantiation and rendering capabilities to visually represent the flux values through color changes of the cells. This functionality can be useful in scenarios involving simulations, visualizations, or game mechanics where spatial data representation is required.

## Variables
- `gridSize`: An integer that defines the dimensions of the grid (both width and height).
- `scanRadius`: A float that determines the scaling factor for how the flux value decreases with distance from the center of the grid.
- `frequencyAmplitude`: A float that represents the maximum possible flux value at the center of the grid.
- `cellPrefab`: A GameObject that serves as the template for each cell in the grid.
- `grid`: A two-dimensional array of GameObjects that holds references to the instantiated cells.

## Functions
- `void Start()`: This is a Unity lifecycle method that is called before the first frame update. It initializes the grid and starts the scanning process.
  
- `void InitializeGrid()`: This function creates a grid of cell GameObjects based on the specified `gridSize`. It positions each cell in a 3D space relative to the center of the grid.

- `void PerformScan()`: This function iterates over each cell in the grid, simulates a flux value for its position, and updates the visual representation of the cell based on that value.

- `float SimulateFluxValue(int x, int y)`: This function calculates the flux value for a cell based on its distance from the center of the grid. It uses the `frequencyAmplitude` and `scanRadius` to determine the resulting flux.

- `void UpdateCellVisual(GameObject cell, float fluxValue)`: This function updates the visual appearance of a cell by changing its color based on the calculated flux value. The color is determined by the intensity of the flux relative to the `frequencyAmplitude`.