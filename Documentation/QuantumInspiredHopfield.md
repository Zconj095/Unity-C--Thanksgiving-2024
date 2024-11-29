# QuantumInspiredHopfield

## Overview
The `QuantumInspiredHopfield` script is a Unity component that simulates a quantum-inspired Hopfield network. This network is designed to mimic certain aspects of neural processing and learning through a series of interconnected networks. The script initializes multiple Hopfield networks, runs a simulation over a defined number of time steps, and synchronizes the networks at specified intervals. The core functionality revolves around updating memory states based on learned patterns and computing similarities between cortical and synaptic states.

## Variables

- `numQubits`: A constant integer representing the number of qubits (or units) in each Hopfield network.
- `numNetworks`: A constant integer specifying the total number of Hopfield networks to be simulated.
- `interjectionThreshold`: A constant float that determines the threshold for detecting significant changes (interjections) between cortical and synaptic states.
- `learningRate`: A constant float that controls the rate at which the network learns from interjections.
- `retentionRate`: A constant float that indicates how much of the previous memory state is retained during updates.
- `synchronizationRate`: A constant float that defines the rate at which networks synchronize their memories.
- `timeSteps`: A constant integer indicating the total number of time steps for the simulation.
- `syncInterval`: A constant integer that specifies how often (in time steps) the networks should synchronize.
- `hopfieldMemory`: A list of 2D float arrays that holds the memory states of each Hopfield network.
- `synapticRelayWeights`: A list of float arrays that stores the synaptic weights for each network.
- `bayesianPrior`: A 2D float array representing the Bayesian prior probabilities used in the memory updates.

## Functions

- `Start()`: Unity's built-in method that is called on the frame when the script is enabled. It initializes the network and starts the simulation.

- `Initialize()`: Sets up the initial state of the Hopfield networks, including creating the memory arrays, initializing synaptic weights, and setting up the Bayesian prior.

- `NormalizeBayesianPrior()`: Normalizes the values in the `bayesianPrior` array so that they sum to 1, ensuring they can be interpreted as probabilities.

- `RunSimulation()`: Executes the main simulation loop for a defined number of time steps. It initializes cortical and synaptic states, computes similarities, detects interjections, updates memory states, and manages synchronization.

- `SynchronizeNetworks()`: A method that averages the memory states across all networks to achieve synchronization. It updates each network's memory based on the synchronized state.

- `PrintFinalStates()`: Outputs the final memory states of each Hopfield network to the debug console for inspection.

- `RandomNormalizedVector(int size)`: Generates a random vector of a specified size, normalizing it to unit length.

- `CosineSimilarity(float[] a, float[] b)`: Computes the cosine similarity between two vectors, returning a float value that indicates how similar the two vectors are.

- `DeltaTangentSimilarity(float[] a, float[] b)`: Calculates the delta tangent similarity between two vectors, which is based on the cosine similarity but transformed through the tangent function.