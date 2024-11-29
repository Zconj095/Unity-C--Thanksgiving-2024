# MemoryCorrelationSystem

## Overview
The `MemoryCorrelationSystem` is a Unity script designed to simulate a memory system that can store, correlate, and retrieve memory states based on spatial stimuli. It creates a collection of memory states, builds a correlation matrix to measure the similarity between these memories, calculates an ambient memory flux, and allows for the retrieval of memories based on a given stimulus vector. This script serves as a core component for any application that requires memory simulation, such as games or virtual environments where memory-based interactions are relevant.

## Variables

- **MemoryState**: A private class that represents a single memory state.
  - `Position`: A `Vector3` representing the spatial position of the memory.
  - `Intensity`: A `float` indicating the strength or significance of the memory.
  - `Tags`: A `string` for classifying the memory with specific tags.

- **memoryStates**: A `List<MemoryState>` that holds all the initialized memory states.

- **memorySize**: A `const int` set to 30, defining the maximum number of memory states to be created.

- **correlationMatrix**: A two-dimensional array (`float[,]`) that stores the correlation values between different memory states.

- **fluxAmbience**: A `float` representing the calculated ambient memory flux, which influences the intensity of memory states.

## Functions

- **InitializeMemory()**: This private method initializes the memory states by populating the `memoryStates` list with random positions, intensities, and tags. It logs the number of initialized memories.

- **BuildCorrelationMatrix()**: This private method constructs the correlation matrix based on the positions of the memory states. It calculates the similarity between each memory's position using the dot product of their normalized vectors and logs the completion of the matrix build.

- **CalculateFluxAmbience()**: This private method computes the ambient memory flux by aggregating the correlation values between memory states weighted by their intensities. It normalizes the result and logs the calculated flux ambience.

- **RetrieveMemory(Vector3 stimulus)**: This private method retrieves the most relevant memory state based on a provided stimulus vector. It calculates the correlation between the stimulus and each memory's position and returns the memory with the highest correlation, logging the details of the retrieved memory.

- **FeedbackMemory(MemoryState memory)**: This private method applies feedback to the memory states based on the retrieved memory. It adjusts the intensity of each memory state based on their correlation with the retrieved memory and the ambient flux, and logs the application of memory feedback.

- **Start()**: This Unity method is called at the start of the script execution. It orchestrates the initialization of memory states, building the correlation matrix, calculating flux ambience, retrieving a memory based on a predefined stimulus, and applying feedback to the retrieved memory.