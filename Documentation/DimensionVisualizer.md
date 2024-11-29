# DimensionVisualizer

## Overview
The `DimensionVisualizer` script is a Unity component that visually represents different dimensions of a `HyperCogniCortex` object in the 3D scene. It draws colored cubes along the y-axis based on the values of each dimension, allowing developers to quickly assess the state of the dimensions visually. This script is intended to be used in the Unity Editor, where it will render visual aids during the design phase without affecting the runtime performance.

## Variables

- **Cortex**: 
  - Type: `HyperCogniCortex`
  - Description: A reference to the `HyperCogniCortex` object that contains a collection of dimensions. This variable is crucial for accessing the dimensions that will be visualized.

## Functions

- **OnDrawGizmos()**: 
  - Description: This method is called by Unity to draw Gizmos in the scene view. It checks if the `Cortex` and its `Dimensions` are not null. If valid, it iterates over each dimension and draws a cube using Gizmos. The color of the cube is determined by the dimension's value relative to its maximum value, with a gradient from red (low value) to green (high value). The cubes are spaced evenly along the y-axis, providing a clear visual representation of the dimension values.