# CelestialFusionSystem

## Overview
The `CelestialFusionSystem` script is designed to simulate a celestial fusion process within a Unity game environment. It combines attributes from two symbolic elements, Tiger's Eye and Lapis Lazuli, to create a dynamic quantum state. This fusion is visualized in the game, allowing developers to see how the variables interact and change over time. The script integrates with Unity's lifecycle methods, such as `Start`, `Update`, and `OnDrawGizmos`, to initialize values, perform calculations, and render visual representations of the fusion process.

## Variables

- **tigerEyeVector** (`Vector3`): A vector representing the attributes of Tiger's Eye, symbolizing celestial alignments. It is initialized in the `Start` method with specific values.

- **lapisLazuliState** (`float[]`): An array that holds the complex states of Lapis Lazuli. This array is initialized in the `Start` method with three float values.

- **quantumMatrix** (`float[,]`): A 2D array representing a dynamic transformation matrix used for quantum fusion. It is initialized with a 3x3 matrix in the `Start` method.

- **fusedQuantumState** (`float[]`): An array that stores the resulting quantum state after the fusion process. It is initialized in the `Start` method to hold three float values.

## Functions

- **Start()**: This method is called when the script instance is being loaded. It initializes the `tigerEyeVector`, `lapisLazuliState`, `quantumMatrix`, and `fusedQuantumState` with predefined values.

- **Update()**: This method is called once per frame. It triggers the celestial fusion process by calling the `TigerEyeLapisFusion` method.

- **TigerEyeLapisFusion()**: This private method combines the `tigerEyeVector` and `lapisLazuliState` into a new array called `combinedState`. It then applies the quantum fusion transformation using the `quantumMatrix`, storing the result in `fusedQuantumState`.

- **MatrixMultiply(float[,] matrix, float[] vector)**: This private method performs matrix-vector multiplication. It takes a 2D matrix and a 1D vector as input and returns a new vector as the result of the multiplication.

- **OnDrawGizmos()**: This method is called to draw Gizmos in the scene view. It visualizes the `tigerEyeVector` as a yellow directional line, the `lapisLazuliState` as blue spheres at specific points, and the `fusedQuantumState` as a magenta sphere, allowing developers to see the current state of the fusion in the Unity editor.

- **OnGUI()**: This method is called for rendering and handling GUI events. It displays the current `fusedQuantumState` in the Game View, providing real-time feedback on the fusion results to the player or developer.