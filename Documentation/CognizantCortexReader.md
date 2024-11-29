# CognizantCortexReader

## Overview
The `CognizantCortexReader` script is a Unity component designed to manage and analyze memory states using two algorithms: K-Means Clustering and Grover's Algorithm. It serves as a bridge between the memory states collected and the algorithms that process these states. By clustering memory states and searching for optimal solutions, this script contributes to the overall functionality of a cognitive simulation within the codebase.

## Variables
- `kmeans`: An instance of `CognizantCortexReaderKMeansClustering` that handles the clustering of memory states into defined groups.
- `grovers`: An instance of `CognizantCortexReaderGroversAlgorithm` that utilizes Grover's search algorithm to identify interesting or optimal states from the memory.
- `memoryStates`: A list of float arrays that stores the memory states added by the user. Each float array represents a distinct memory state.

## Functions
- `Start()`: This is a Unity lifecycle method that initializes the script. It sets up the K-Means clustering with a specified number of clusters and initializes Grover's Algorithm with a defined search space size. It also adds example memory states and performs clustering on them, logging the results. Additionally, it marks a state for Grover's Algorithm and finds the best state, logging the outcome.
  
- `AddMemoryState(float[] state)`: This public method allows the addition of a new memory state to the `memoryStates` list. It also updates the K-Means clustering algorithm by adding the new data point for further processing.