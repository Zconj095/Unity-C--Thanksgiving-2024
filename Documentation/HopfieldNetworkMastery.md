# HopfieldNetworkMastery

## Overview
The `HopfieldNetworkMastery` script implements a simulation of multiple Hopfield networks in Unity. It focuses on the learning and synchronization of memories across these networks. Each network operates in a high-dimensional space, represented by qubits, and learns patterns over a series of time steps. The script manages memory retention, interjections, and synchronization across networks, providing a framework for exploring the dynamics of Hopfield networks and their mastery of learned information.

## Variables

- `NumQubits`: (int) The number of qubits representing the high-dimensional space for each network.
- `NumNetworks`: (int) The total number of Hopfield networks being simulated.
- `InterjectionThreshold`: (float) A threshold value used to determine significant interjections in the network's learning process.
- `LearningRate`: (float) The rate at which the network adjusts its weights during learning.
- `RetentionRate`: (float) The rate at which the network retains its learned memories.
- `SynchronizationRate`: (float) The influence of wisdom synchronization across networks.
- `TimeSteps`: (int) The number of temporal steps for which the simulation will run.
- `SyncInterval`: (int) The interval at which wisdom synchronization occurs across networks.
- `MasteryScaleFactor`: (float) The rate at which mastery of learned information improves.
- `hopfieldMemory`: (float[][,]) A multi-dimensional array storing the memory matrices of each Hopfield network.
- `synapticRelayWeights`: (float[][]) An array of arrays that holds the synaptic relay weights for each network.
- `masteryLevels`: (float[][,]) A multi-dimensional array that tracks mastery levels for each network.
- `bayesianPrior`: (float[,]) A matrix generated to serve as a Bayesian prior for updating memory matrices.

## Functions

- `Start()`: Initializes the network structures, populates them with random values, and runs the simulation for the defined number of time steps. It also outputs the final Hopfield memory matrices to the console.
  
- `SimulateTimeStep(int timeStep)`: Simulates a single time step for all networks, generating random cortical and synaptic states, calculating similarities, detecting interjections, and updating synaptic relay weights and Hopfield memory.

- `UpdateHopfieldMemory(int net, float cosSim)`: Updates the memory matrix of a specified network based on the calculated cosine similarity and mastery improvements.

- `SynchronizeWisdom()`: Synchronizes wisdom across all networks by updating their memory matrices with the maximum values found in the collective memory of all networks.

- `GenerateRandomMatrix(int rows, int cols)`: Creates and returns a random matrix of specified dimensions.

- `NormalizeMatrix(float[,] matrix)`: Normalizes the values in the provided matrix so that they sum to 1.

- `GenerateRandomVector(int size, System.Random random)`: Generates and returns a random vector of specified size.

- `Normalize(float[] vector)`: Normalizes the provided vector to have a unit norm.

- `CalculateCosineSimilarity(float[] vectorA, float[] vectorB)`: Computes and returns the cosine similarity between two vectors.

- `CalculateDeltaTangentSimilarity(float cosSim)`: Calculates and returns the delta tangent similarity based on the cosine similarity.

- `PrintMatrix(float[,] matrix)`: Outputs the contents of the provided matrix to the console.

- `GetRow(float[,] matrix, int row)`: Retrieves and returns a specific row from the provided matrix.