# HopfieldNetworkSimulation

## Overview
The `HopfieldNetworkSimulation` script simulates a series of Hopfield networks, which are a form of recurrent neural network used for associative memory. This simulation is designed to explore the dynamics of memory storage and retrieval through various parameters such as learning rates, retention rates, and synchronization among multiple networks. The script initializes multiple Hopfield networks, runs a simulation over a defined number of time steps, and outputs the final memory matrices of each network.

## Variables

- **NumQubits**: The number of qubits (or dimensions) representing the state of each network.
- **NumNetworks**: The total number of Hopfield networks to be simulated.
- **InterjectionThreshold**: A threshold value used to determine when to update synaptic weights based on interjection.
- **LearningRate**: The rate at which learning occurs in the network, following Hebbian principles.
- **RetentionRate**: The rate at which memory is retained during updates, based on Bayesian updating.
- **SynchronizationRate**: The rate at which data synchronization occurs between networks.
- **DecayRate**: The rate of synaptic pruning applied to the memory.
- **ImportanceThreshold**: A threshold that determines when neuroplasticity adjustments should be applied.
- **TimeSteps**: The total number of time steps for which the simulation runs.
- **SyncInterval**: The interval at which networks are synchronized.

- **hopfieldMemory**: A 3D array that stores the memory matrices for each Hopfield network.
- **synapticRelayWeights**: A 2D array that holds the synaptic weights for each qubit in the networks.
- **usageTracker**: A 3D array that tracks the usage of each synaptic connection over time.

## Functions

- **Start()**: Initializes the networks and begins the simulation by calling `InitializeNetworks()` and `RunSimulation()`.

- **InitializeNetworks()**: Sets up the Hopfield networks by initializing the memory matrices, synaptic weights, and usage trackers for each network.

- **RunSimulation()**: The core function of the script that runs the simulation over the defined number of time steps. It generates random states, normalizes them, calculates similarities, updates memory, and synchronizes networks at specified intervals.

- **GenerateRandomState(int size, System.Random random)**: Creates and returns a random state array of the specified size.

- **Normalize(float[] state)**: Normalizes the input state array to unit length.

- **CalculateCosineSimilarity(float[] a, float[] b)**: Computes and returns the cosine similarity between two input state arrays.

- **CalculateDeltaTangentSimilarity(float cosSim)**: Calculates and returns the tangent of the arccosine of the cosine similarity value.

- **UpdateHopfieldMemory(int net, float cosSim, int t)**: Updates the memory of the specified network based on the cosine similarity and the current time step, applying retention and decay.

- **ApplyNeuroplasticity(int net, float cosSim, int t)**: Adjusts the memory of the specified network based on the importance threshold and the cosine similarity.

- **SynchronizeNetworks()**: Synchronizes the memory matrices of all networks by averaging their states.

- **OutputResults()**: Outputs the final memory matrices of each network to the console for inspection.