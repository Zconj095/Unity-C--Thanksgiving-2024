# SynapticRelaySimulation

## Overview
The `SynapticRelaySimulation` script simulates a model of synaptic relay weights using concepts from quantum computing and Hebbian learning. It initializes a set of synaptic weights, iteratively simulates their evolution over a series of time steps, and logs the results. This script is intended for use in a Unity environment, allowing for visual representation and interaction with the simulation.

## Variables
- `NumQubits`: A constant integer representing the number of qubits (5). This is the dimensionality of the states being simulated.
- `InterjectionThreshold`: A constant float (0.3) that defines the threshold for significant changes in synaptic weights based on interjections.
- `LearningRate`: A constant float (0.1) that determines the rate at which the synaptic weights are updated during the simulation.
- `TimeSteps`: A constant integer (50) that specifies the number of temporal steps the simulation will run.
- `synapticRelayWeights`: An array of floats that holds the current weights of the synaptic relay, initialized to 1 for each qubit.

## Functions
- `Start()`: Unity's built-in method that is called before the first frame update. It initializes the synaptic relay weights and runs the simulation for the specified number of time steps. It also logs the final weights after the simulation completes.

- `SimulateTimeStep(int t)`: Simulates a single time step of the relay. It generates random cortical and synaptic states, normalizes them, calculates their similarities, determines interjections, and updates the synaptic weights accordingly. It logs the cosine similarity, delta tangent similarity, and updated weights.

- `GenerateRandomState(int size, System.Random random)`: Generates an array of random float values between 0 and 1 of the specified size, representing the cortical or synaptic state.

- `Normalize(float[] state)`: Normalizes the input array of floats so that the sum of the squares of its elements equals 1. This ensures that the states are unit vectors.

- `CalculateCosineSimilarity(float[] stateA, float[] stateB)`: Computes the cosine similarity between two input states. It returns a float value that indicates how similar the two states are based on their directional alignment.

- `CalculateDeltaTangentSimilarity(float cosSim)`: Calculates the delta tangent similarity based on the cosine similarity. It returns the tangent of the arccosine of the cosine similarity.

- `CalculateInterjection(float cosSim, float deltaTangentSim)`: Computes an array of interjection values based on the difference between cosine similarity and delta tangent similarity for each qubit. This array is used to determine which synaptic weights should be updated.