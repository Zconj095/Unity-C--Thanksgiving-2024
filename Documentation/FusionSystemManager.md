# FusionSystemManager

## Overview
The `FusionSystemManager` script is designed to manage the fusion of two states, represented by Russel and Zoe, and map the resulting fused state into a Cartesian space. It initializes various parameters, performs the fusion operation in the `Update` loop, and visualizes the results in the Unity environment. This script fits into a larger codebase where state fusion and spatial representation are required, such as in simulations or games involving multiple entities or data points.

## Variables
- `private Vector3[] cartesianPoints`: An array of points in Cartesian space, initialized to represent a grid of three points.
- `private float[] stateRussel`: An array representing the state of Russel, initialized with three float values.
- `private float[] stateZoe`: An array representing the state of Zoe, initialized with three float values.
- `private float[,] fusionMatrix`: A 3x3 matrix used for non-linear transformations during the fusion process.
- `private float[] fusedState`: An array to hold the resulting state after the fusion of Russel and Zoe's states.
- `private Vector3 outputPoint`: A `Vector3` that represents the output point in Cartesian space after mapping the fused state.

## Functions
- `void Start()`: Initializes the Cartesian points, states for Russel and Zoe, the fusion matrix, and the fused state at the start of the script.
  
- `void Update()`: Called once per frame, this function performs the fusion of states and maps the result to Cartesian space.

- `private void PerformFusion()`: This function calculates the fused state by applying the fusion matrix to the sum of Russel and Zoe's states.

- `private void MapToCartesianSpace()`: Maps the fused state into Cartesian space for visualization by normalizing the values and scaling the output point.

- `void OnDrawGizmos()`: Visualizes the Cartesian points and the fused output point in the Unity editor when the application is running.

- `void OnGUI()`: Displays the fused state and the corresponding output point on the screen using Unity's GUI system.