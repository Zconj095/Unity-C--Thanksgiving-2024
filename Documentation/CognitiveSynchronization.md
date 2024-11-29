# CognitiveSynchronization

## Overview
The `CognitiveSynchronization` script simulates a simple neural network where neurons can synchronize their phases over time. It initializes a specified number of neurons with random phases and firing rates, establishes connections between them with random strengths, and updates their states at regular intervals based on synchronization dynamics. This script is likely part of a larger codebase focused on simulating neural networks or cognitive processes within a Unity game or application.

## Variables

- **numberOfNeurons**: An integer representing the total number of neurons in the network.
- **couplingStrength**: A float that determines the strength of the coupling between neurons during phase updates.
- **learningRate**: A float that specifies the rate for Hebbian updates, although it is not currently used in the script.
- **phaseUpdateInterval**: A float indicating the time interval (in seconds) at which the neuron states will be updated.
- **neurons**: A list of `Neuron` objects representing the individual neurons in the network.
- **connectionWeights**: A 2D array of floats that holds the connection strengths between each pair of neurons.

## Functions

- **Start()**: Initializes the neurons and their connections, and sets up a repeating invocation of the `UpdateNeuronStates` method based on the defined `phaseUpdateInterval`.

- **InitializeNeurons()**: Clears the existing list of neurons and populates it with new `Neuron` instances, each initialized with a random phase and firing rate. It logs the number of neurons initialized.

- **InitializeConnections()**: Creates a 2D array for connection weights and populates it with random values representing the strength of connections between different neurons. Self-connections are set to zero. It logs when the connections have been initialized.

- **UpdateNeuronStates()**: Calculates the new phases for each neuron based on the synchronization dynamics influenced by their connections. It updates the neurons' phases and calls `DebugNeuronStates` to log their current states.

- **DebugNeuronStates()**: Logs the current phase and firing rate of each neuron to the console for debugging purposes.

- **NormalizePhase(float phase)**: Takes a phase value and normalizes it to ensure it stays within the range of [0, 2π]. This is important for maintaining the correct representation of phases in the simulation.