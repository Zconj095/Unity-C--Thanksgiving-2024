# SapphireSystem

## Overview
The `SapphireSystem` script is designed to simulate and visualize the interaction of sinusoidal states influenced by chaos and harmony within a Unity environment. It utilizes various states such as Lapis Lazuli and Finnonian to transform the Sapphire states, creating a dynamic representation of oscillations and transformations. This script is likely part of a larger codebase focused on visual effects or simulations, where the interplay between different states can create engaging visual experiences.

## Variables

- `sapphireSinusoids`: An array of floats representing the current sinusoidal states of the Sapphire system, initialized with three periodic oscillation values.
  
- `chaosPerturbations`: An array of floats representing random influences that perturb the sinusoidal states, initialized with three chaos values.
  
- `harmonyFactor`: A public float that balances the influence of chaos on the sinusoidal states, defaulting to 0.8.
  
- `lapisLazuliState`: An array of floats representing the state of Lapis Lazuli, initialized with three values that may affect the transformation of the Sapphire states.
  
- `finnonianState`: A float representing the rebirth factor, initialized to 1.0, which transforms the Sapphire states.
  
- `zerahRefreshment`: A float representing the transformative influence, initialized to 1.5, which also affects the transformation of the Sapphire states.
  
- `time`: A float that tracks the elapsed time to generate oscillations over frames.

## Functions

- `void Start()`: Initializes the variables at the start of the game or simulation. Sets up the initial values for the sinusoidal states, chaos perturbations, Lapis Lazuli states, Finnonian state, and Zerah refreshment.

- `void Update()`: Called once per frame, this function increments the time variable and updates the Sapphire sinusoidal states, synergizes harmony and chaos, and applies the Finnonian rebirth transformation.

- `private void UpdateSapphireSinusoids()`: Generates and updates the sinusoidal oscillations based on the elapsed time, modifying each value in the `sapphireSinusoids` array using the sine function.

- `private void SynergizeHarmonyChaos()`: Blends the chaos perturbations with the current sinusoidal states using the harmony factor, modifying each value in the `sapphireSinusoids` array.

- `private void ApplyFinnonianRebirth()`: Transforms the Sapphire states by applying influences from the Lapis Lazuli states and Zerah refreshment, modifying each value in the `sapphireSinusoids` array.

- `void OnDrawGizmos()`: Visualizes the current states of the Sapphire system in the Unity editor as colored spheres in a 3D space, providing a visual representation of the sinusoidal states, the Finnonian rebirth effect, and the Zerah refreshment influence.

- `void OnGUI()`: Displays the current values of the Sapphire states, Finnonian state, and Zerah refreshment on the screen, allowing for real-time monitoring of the system's state during gameplay.