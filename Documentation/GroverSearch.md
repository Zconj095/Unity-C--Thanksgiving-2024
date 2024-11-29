# GroverSearch

## Overview
The `GroverSearch` script implements Grover's quantum search algorithm to find the closest match to a given query vector within a provided database of vectors. This script is designed to work within the Unity game engine and integrates quantum search principles to enhance the efficiency of searching through high-dimensional data. The main function, `PerformSearch`, orchestrates the quantum state preparation, applies the oracle for target identification, and performs amplitude amplification through diffusion operations, ultimately returning the index of the closest match.

## Variables
- `databaseVectors`: A two-dimensional array of floats representing the database of vectors to search through. Each inner array is a vector in the database.
- `queryVector`: A one-dimensional array of floats representing the vector to search for within the database.
- `similarityThreshold`: A float value (default is 0.9) that defines the minimum similarity required for a vector to be considered a match.
- `iterations`: An integer (default is 3) that specifies the number of iterations to perform in the quantum search process, influencing the accuracy and efficiency of the search.
- `size`: An integer that stores the number of vectors in the database, derived from the length of `databaseVectors`.
- `quantumState`: An instance of `QuantumSearchState` that holds the state of the quantum search process.
- `oracle`: An instance of `VectorDatabaseOracle` that encapsulates the logic for determining whether a given vector is the target match.

## Functions
- `PerformSearch(float[][] databaseVectors, float[] queryVector, float similarityThreshold = 0.9f, int iterations = 3)`: 
  - This is the main function of the script. It initializes the quantum search state and oracle, applies the oracle and diffusion operations for a specified number of iterations, and finally measures the quantum state to return the index of the closest match in the database.