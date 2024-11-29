# VectorDatabaseOracle

## Overview
The `VectorDatabaseOracle` class is designed to evaluate the similarity of vectors using cosine similarity. It serves as a utility within a larger codebase that likely deals with vector representations, such as in machine learning or game development scenarios. The primary function of this class is to determine whether a given vector (from a stored set of vectors) is similar enough to a specified query vector based on a defined similarity threshold.

## Variables
- `private float[][] vectors;`  
  An array that stores multiple vectors. Each vector is represented as an array of floats.

- `private float[] queryVector;`  
  A single vector represented as an array of floats that is used for comparison against the stored vectors.

- `private float similarityThreshold;`  
  A float value that sets the minimum cosine similarity required for a vector to be considered similar to the query vector. The default value is set to 0.9.

## Functions
- `public VectorDatabaseOracle(float[][] vectors, float[] queryVector, float similarityThreshold = 0.9f)`  
  Constructor for the `VectorDatabaseOracle` class. It initializes the instance with a set of vectors, a query vector, and an optional similarity threshold.

- `public bool IsTargetState(int index)`  
  Checks if the vector at the specified index in the `vectors` array has a cosine similarity with the `queryVector` that meets or exceeds the `similarityThreshold`. Returns `true` if it does, otherwise returns `false`.

- `private float ComputeCosineSimilarity(float[] vec1, float[] vec2)`  
  Computes the cosine similarity between two vectors. It calculates the dot product of the vectors and their magnitudes, returning the cosine similarity value as a float.