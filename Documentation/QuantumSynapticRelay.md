# QuantumSynapticRelay

## Overview
The `QuantumSynapticRelay` script simulates a quantum state relay system within a Unity environment. It models the probabilities of three different states: relay, delay, and cortical influences over a series of time steps. The script initializes these probabilities and simulates their evolution based on defined factors that influence synaptic flux and cortical relapse. The results of the simulation are logged to the console, providing insights into the dynamics of the system.

## Variables
- `SynapticFluxFactor`: A constant float representing the probability of the relay state being activated. It influences how likely the relay probability is to increase or decrease.
- `CorticalRelapseFactor`: A constant float that represents the level of cortical inhibition affecting the probabilities. It influences the adjustment of the cortical state.
- `TimeSteps`: A constant integer that defines how many temporal steps the simulation will run.
- `probabilities`: An array of floats that holds the current probabilities of the three states: relay (index 0), delay (index 1), and cortical (index 2).

## Functions
- `Start()`: This function is called when the script instance is being loaded. It initializes the probabilities of the relay, delay, and cortical states, then runs the simulation for the defined number of time steps by calling `SimulateTimeStep(t)` for each time step. Finally, it logs the computed probabilities to the console.

- `SimulateTimeStep(int t)`: This function simulates the dynamics of the quantum circuit at a given time step. It adjusts the relay probability based on a random value compared to the `SynapticFluxFactor`, modifies the cortical probability based on the `CorticalRelapseFactor`, and updates the delay probability based on the current relay and cortical states. It also ensures that the probabilities remain within valid bounds by calling `NormalizeProbabilities()` at the end.

- `NormalizeProbabilities()`: This function normalizes the probabilities in the `probabilities` array so that their sum equals 1. It calculates the total sum of the probabilities and divides each probability by this sum to ensure they are proportional and valid as probabilities.