# HebbianLearningSimulation

## Overview
The `HebbianLearningSimulation` script is designed to simulate a Hebbian learning process within a neural network model using Unity. This simulation involves the manipulation of weights based on the interaction between cortical and synaptic states, following the principles of Hebbian learning which states that "cells that fire together, wire together." The script initializes Hebbian weights, simulates the learning process over a specified number of time steps, and outputs the final weights to the console. This simulation can be integrated into larger neural network frameworks or used for educational purposes to understand Hebbian learning dynamics.

## Variables

- `NumQubits`: An integer constant representing the number of dimensions in the representation (5).
- `LearningRate`: A float constant that determines the rate at which learning occurs (0.1).
- `RetentionRate`: A float constant that specifies how much prior learning is retained (0.9).
- `AttentionThreshold`: A float constant that sets the threshold for the attention mechanism (0.6).
- `InstructionWeight`: A float constant that represents the extra weight applied to instruction-following states (1.5).
- `TimeSteps`: An integer constant that defines the number of time steps for the simulation (50).
- `hebbianWeights`: An array of floats that holds the current Hebbian weights for each qubit.

## Functions

- `void Start()`: This is the Unity lifecycle method that initializes the Hebbian weights and runs the simulation for the specified number of time steps. It also logs the final state of the weights to the console.

- `private void SimulateTimeStep(int timeStep)`: This function simulates a single time step of the Hebbian learning process. It generates random cortical and synaptic states, normalizes them, calculates the delta cosine tangent intermittance, applies the attention mechanism to update the Hebbian weights, and logs the progress.

- `private float[] GenerateRandomState(int size, System.Random random)`: This function creates a random state represented as an array of floats, with each element being a random value between 0 and 1.

- `private void Normalize(float[] state)`: This function normalizes the given state array by dividing each element by the vector's norm, ensuring that the state has a unit length.

- `private float[] CalculateDeltaIntermit(float[] corticalState, float[] synapticState)`: This function computes the delta intermittance based on the cosine similarity between the cortical and synaptic states. It returns an array of floats representing the calculated values for each qubit.