# SpecialHopfieldNetworkSimulation

## Overview
The `SpecialHopfieldNetworkSimulation` script simulates a specialized Hopfield neural network within a Unity environment. It employs high-dimensional representations (qubits) to model memory and learning processes. The script initializes network parameters, runs a time-based simulation, and updates memory based on interjection detection and similarity calculations. This simulation is part of a broader codebase likely focused on neural network modeling and cognitive simulations.

## Variables

- `NumQubits`: A constant integer representing the number of qubits (or dimensions) in the high-dimensional representation of the network.
- `InterjectionThreshold`: A constant float that sets the threshold for detecting interjections during the simulation.
- `LearningRate`: A float that defines the rate at which the network learns through Hebbian learning principles.
- `RetentionRate`: A float indicating the rate at which memories are retained in the Hopfield network.
- `TimeSteps`: A constant integer that specifies the number of temporal steps for the simulation.
- `hopfieldMemory`: A two-dimensional float array that stores the memory states of the Hopfield network.
- `synapticRelayWeights`: A one-dimensional float array that holds the weights for synaptic relay connections.
- `bayesianPrior`: A two-dimensional float array representing a Bayesian prior matrix used in the memory updating process.

## Functions

- `Start()`: Initializes the memory, synaptic weights, and Bayesian prior matrix. It normalizes the prior and runs the simulation for a set number of time steps, logging the final memory matrix.

- `SimulateTimeStep(int timeStep)`: Executes a single time step of the simulation. It generates random cortical and synaptic states, normalizes them, calculates their similarities, detects interjections, updates synaptic weights and Hopfield memory, and logs the results.

- `UpdateHopfieldMemory(float cosSim)`: Updates the Hopfield memory matrix based on the cosine similarity calculated during the simulation time step.

- `GenerateRandomMatrix(int rows, int cols)`: Creates and returns a two-dimensional array filled with random float values.

- `NormalizeMatrix(float[,] matrix)`: Normalizes the values in a given matrix so that their sum equals one.

- `GenerateRandomVector(int size, System.Random random)`: Produces and returns a one-dimensional array filled with random float values.

- `Normalize(float[] vector)`: Normalizes a vector so that its length (or norm) equals one.

- `CalculateCosineSimilarity(float[] vectorA, float[] vectorB)`: Computes and returns the cosine similarity between two vectors.

- `CalculateDeltaTangentSimilarity(float cosSim)`: Calculates and returns the delta tangent similarity based on the cosine similarity.

- `GetRow(float[,] matrix, int row)`: Retrieves and returns a specific row from a two-dimensional matrix as a one-dimensional array.