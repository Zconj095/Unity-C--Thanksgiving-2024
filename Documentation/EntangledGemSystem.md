# EntangledGemSystem

## Overview
The `EntangledGemSystem` script manages the quantum states of two gems, Opal and Emerald, and simulates their entanglement through a mathematical process. This script initializes the quantum states of both gems, applies an entanglement matrix to combine their states into a shared state, and updates their individual states based on the shared state during each frame of the game. It also provides visual representations of the states in the Unity environment and displays their values in the GUI.

## Variables

- `opalState`: A float array representing the quantum state of Opal as a 3D vector.
- `emeraldState`: A float array representing the quantum state of Emerald as a 3D vector.
- `entanglementMatrix`: A 2D float array that defines the relationship between Opal and Emerald, facilitating their entanglement.
- `sharedState`: A float array that holds the combined quantum state resulting from the entanglement process.

## Functions

- `Start()`: Initializes the quantum states of Opal and Emerald, sets up the entanglement matrix, and initializes the shared state.
  
- `Update()`: Called once per frame; it triggers the entanglement process by calling `PerformEntanglement()`.

- `PerformEntanglement()`: Combines the quantum states of Opal and Emerald into a shared state, applies the entanglement matrix to this combined state, and updates the individual states of Opal and Emerald based on the shared state.

- `MatrixMultiply(float[,] matrix, float[] vector)`: A utility function that performs matrix-vector multiplication. It takes a 2D matrix and a 1D vector as input and returns the resulting vector after the multiplication.

- `OnDrawGizmos()`: Visualizes the states of Opal, Emerald, and the shared state in the Unity editor using colored spheres when the application is playing.

- `OnGUI()`: Displays the current quantum states of Opal, Emerald, and the shared state as labels in the Unity GUI.