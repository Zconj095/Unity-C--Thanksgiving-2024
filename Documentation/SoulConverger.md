# SoulConverger

## Overview
The `SoulConverger` script is designed to merge multiple soul entities into a single, resultant soul. It takes a list of soul entities, calculates their combined properties (such as position, color, and intensity), and generates a new soul entity that represents the convergence of these souls. The script also provides visual feedback during the merging process through a particle effect and a visual representation of the resultant soul. This functionality fits within a larger codebase that likely involves game mechanics related to souls or entities, enhancing the gameplay experience by allowing players to merge souls for various effects.

## Variables

- **InputSouls**: A `List<SoulEntity>` that holds the souls to be merged.
- **ConvergedSoul**: A `SoulEntity` that represents the result of the merging process.
- **ConvergenceEffect**: A `ParticleSystem` that displays visual effects during the merging of souls.
- **visualRepresentation**: A `MeshRenderer` used to visually represent the converged soul in the game world.

## Functions

- **Start()**: Initializes the visual representation of the converged soul by creating a sphere in the game world and assigning it a standard material.

- **ConvergeSouls()**: Merges the souls in the `InputSouls` list. It calculates the combined position, blended color, and total intensity of the souls. If no souls are available to merge, it logs a warning. After calculating the properties, it creates a new `ConvergedSoul` and updates its visual representation and triggers the convergence effect.

- **UpdateVisualRepresentation()**: Updates the visual representation of the converged soul by changing its color, position, and scale based on the properties of the `ConvergedSoul`.

- **TriggerConvergenceEffect()**: Plays the particle effect associated with the convergence process. It sets the position and start color of the particle effect based on the properties of the `ConvergedSoul`.

- **AddSoul(SoulEntity soul)**: Adds a new soul to the `InputSouls` list and logs a message indicating that the soul has been added.

- **RemoveSoul(SoulEntity soul)**: Removes a specified soul from the `InputSouls` list if it exists and logs a message indicating that the soul has been removed.