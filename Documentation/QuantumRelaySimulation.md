# QuantumRelaySimulation

## Overview
The `QuantumRelaySimulation` script is designed to simulate a quantum-like relay system using probabilities to represent different states: relay, delay, and cortical influence. This simulation runs for a specified number of time steps, adjusting the probabilities based on defined factors that mimic synaptic and cortical behaviors. The script is integrated within a Unity environment, allowing for real-time visualization and interaction.

## Variables
- `SynapticFluxFactor` (float): A constant representing the probability of the relay state being activated. It influences how likely the system is to relay information.
- `CorticalRelapseFactor` (float): A constant that represents the level of cortical inhibition affecting the system's probabilities.
- `TimeSteps` (int): A constant that defines how many temporal steps the simulation will run.
- `NumQubits` (int): A constant that indicates the number of states in the system (relay, delay, cortical).
- `probabilities` (float[]): An array that holds the current probabilities for the three qubit states: relay, delay, and cortical influence.

## Functions
- `Start()`: This is a Unity lifecycle method that initializes the probabilities for the relay, delay, and cortical states. It also runs the simulation over the defined number of time steps and logs the final probabilities to the console.

- `SimulateTimeStep(int t)`: This method simulates the behavior of the system at a given time step. It includes:
  - Adjusting the relay probability based on a random chance influenced by the `SynapticFluxFactor`.
  - Modifying the cortical influence probability using the `CorticalRelapseFactor`.
  - Adjusting the delay probability based on the current state of the relay and cortical probabilities.
  - Normalizing the probabilities to ensure they sum to 1, simulating quantum state vector behavior.

- `NormalizeProbabilities()`: This method normalizes the probabilities in the `probabilities` array so that their total equals 1, ensuring that the simulation adheres to the principles of quantum mechanics where probabilities must be valid (i.e., between 0 and 1).