# LLMCosineSimilarityVectorDatabase

## Overview
The `LLMCosineSimilarityVectorDatabase` class is designed to manage a collection of vectors and perform similarity queries using cosine similarity. This class fits within a codebase that requires efficient storage and retrieval of vector data, allowing developers to find the most similar vectors to a given query vector. It leverages Unity's MonoBehaviour for integration within Unity projects.

## Variables
- **vectorStore**: A private dictionary that maps integer IDs to their corresponding float arrays (vectors). This serves as the main storage for all vectors added to the database.

## Functions
- **AddVector(int id, float[] vector)**: This public method takes an integer ID and a float array (vector) as parameters. It adds the vector to the `vectorStore` dictionary, associating it with the specified ID. If the ID already exists, it updates the existing vector.

- **GetVector(int id)**: This public method retrieves a vector associated with the given integer ID. If the ID exists in the `vectorStore`, it returns the corresponding vector; otherwise, it returns `null`.

- **QueryNearestNeighbors(float[] query, int topN)**: This public method takes a query vector and an integer `topN` as parameters. It computes the cosine similarity between the query vector and all vectors in the `vectorStore`, returning a list of the IDs of the top `N` nearest neighbors based on similarity scores.

- **CosineSimilarity(float[] a, float[] b)**: This private method calculates the cosine similarity between two float arrays (vectors). It computes the dot product and magnitudes of the vectors, returning the cosine similarity score as a float. This method is called internally by `QueryNearestNeighbors` to perform similarity calculations.