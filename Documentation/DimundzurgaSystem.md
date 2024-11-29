# DimundzurgaSystem

## Overview
The `DimundzurgaSystem` script is designed to simulate celestial bonding states in a 3D space using Unity. It creates a system of celestial bonds, calculates transmission fields, and updates bond strengths based on a feedback loop. This script integrates with the rest of the codebase by providing a framework for simulating interactions between celestial entities, which could be used in games or simulations that involve spatial relationships and physics.

## Variables

- **CelestialBond**: A private class that represents the state of a celestial bond. It contains:
  - `Position`: A `Vector3` representing the position of the bond in 3D space.
  - `QuantumState`: A `float` representing a random quantum state value between 0.0 and 1.0.
  - `BondStrength`: A `float` representing the strength of the bond, initialized to 0.0.

- **celestialBonds**: A `List<CelestialBond>` that stores all the celestial bonds created in the system.

- **numBonds**: A constant `int` set to 40, which defines the number of celestial bonds to be initialized.

## Functions

- **InitializeCelestialBonds()**: 
  - Initializes the celestial bonds by generating random positions within a specified range and creating `CelestialBond` instances. It populates the `celestialBonds` list and logs the initialization.

- **ZoltzTransmission(Vector3 position, Vector3 target)**:
  - Calculates the transmission value between two points based on their distance using an exponential decay function. It logs the transmission value and the distance between the two points.

- **YuukiTransmissionField(Vector3 origin)**:
  - Computes a transmission field vector based on the celestial bonds and their positions. It sums the contributions of each bond weighted by their transmission values and logs the calculated field vector.

- **CyiaraGate(Vector3 fieldVector, Vector3 target)**:
  - Calculates the flux correlation between the normalized field vector and a target vector using the dot product. It logs the output of the transmission gate.

- **DimundzurgaFeedback(Vector3 stimulus)**:
  - Executes a feedback loop for each celestial bond. It updates the bond strength based on the flux correlation with the stimulus and logs the updated strength for each bond. It also logs when the feedback process is completed.

- **Start()**:
  - The Unity method that is called when the script is first run. It initializes the celestial bonding system and simulates a feedback loop using a predefined stimulus vector.