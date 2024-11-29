# CozniCortexSystem

## Overview
The `CozniCortexSystem` script is designed to simulate the behavior of a cognitive feedback system using celestial vectors in a 3D space within a Unity environment. It initializes an array of celestial vectors, calculates their positions, and generates cognitive feedback based on a given stimulus. This script is integral to the codebase, providing functionality that models cognitive responses and interactions with celestial data structures.

## Variables

- **celestialArray**: A list that holds instances of the `CelestialVector` class. Each `CelestialVector` represents a position in 3D space and its corresponding activation level.
  
- **arraySize**: A constant integer that defines the number of celestial vectors to be initialized in the `celestialArray`. It is set to 50.

## Functions

- **InitializeCelestialArray()**: 
  - Initializes the `celestialArray` with `arraySize` number of `CelestialVector` instances. Each vector is assigned a random position within a range of -20 to 20 on all three axes (x, y, z). A debug message is logged to confirm initialization.

- **ZelliaPermutation(Vector3 start, int depth, float scale)**: 
  - Implements a recursive algorithm to generate permutations of vectors based on a starting point, a specified depth, and a scaling factor. The function returns a list of `Vector3` positions that represent the permutations.

- **TransformVectorMeans()**: 
  - Calculates the mean position of all vectors in the `celestialArray`. It sums the positions and divides by the count of vectors to find the average position. A debug message is logged with the transformed mean vector.

- **CalculateHypercorrelation(Vector3 stimulus, Vector3 response)**: 
  - Computes the hypercorrelation between a stimulus vector and a response vector using the dot product and distance between the two vectors. It returns a float value representing the hypercorrelation and logs the result.

- **GenerateCognitiveFeedback(Vector3 stimulus)**: 
  - Generates cognitive feedback based on a given stimulus. It first transforms the vector means, then iterates through each vector in the `celestialArray` to calculate its response to the stimulus using hypercorrelation. It updates the activation level of each vector and logs the activation levels.

- **Start()**: 
  - The entry point of the script. It initializes the celestial array, generates Zellia permutations starting from the origin, and simulates the cognitive feedback process using a defined stimulus vector. Debug messages are logged to indicate the completion of these processes.