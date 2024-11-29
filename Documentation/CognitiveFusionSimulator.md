# CognitiveFusionSimulator

## Overview
The `CognitiveFusionSimulator` script is designed to simulate the fusion of various cognitive states and visualize the result in a Unity environment. It takes a list of cognitive states, each with an intensity and a color, and combines them into a single fused state. This fused state is represented visually by changing the color and scale of a primitive object (a sphere) in the scene, allowing for a dynamic representation of cognitive emotional states.

## Variables

- **CognitiveState**: A class that defines individual cognitive states with the following properties:
  - `Name`: A string representing the name of the cognitive state.
  - `Intensity`: A float value ranging from 0.0 to 1.0 indicating the strength of the cognitive state.
  - `StateColor`: A `Color` object that visually represents the cognitive state.

- **FusedState**: A class that defines the result of the fusion of cognitive states with the following properties:
  - `FusedName`: A string representing the name of the fused cognitive state.
  - `CombinedIntensity`: A float value representing the average intensity of the fused cognitive states.
  - `BlendedColor`: A `Color` object representing the blended color of the cognitive states.

- **CognitiveStates**: A public list that holds multiple `CognitiveState` objects to be fused.

- **FusedCognitiveState**: A public `FusedState` object that stores the result of the fusion process.

- **visualRepresentation**: A private `MeshRenderer` object that is used to visually represent the fused cognitive state in the Unity scene.

## Functions

- **Start()**: This function initializes the script. It creates a primitive sphere in the Unity scene and assigns its `MeshRenderer` component to `visualRepresentation`, which will be used to display the fused cognitive state.

- **Update()**: This function is called once per frame. It continuously calls the `SimulateFusion()` method to calculate the new fused cognitive state and then updates the visual representation of that state by calling `UpdateVisualRepresentation()`.

- **SimulateFusion()**: This private method calculates the total intensity and blended color of the cognitive states in the `CognitiveStates` list. It sums the intensities and blends the colors based on their respective intensities. The results are stored in the `FusedCognitiveState`.

- **UpdateVisualRepresentation()**: This private method updates the visual representation of the fused cognitive state in the Unity scene. It changes the color of the sphere to match the `BlendedColor` of the `FusedCognitiveState` and scales the sphere based on the `CombinedIntensity`.