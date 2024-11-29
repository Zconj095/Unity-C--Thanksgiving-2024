# EmbeddingSimilarity

## Overview
The `EmbeddingSimilarity` class is designed to compute the cosine similarity between vectors and categorize data based on this similarity. It is particularly useful in scenarios where you need to analyze and group embeddings, such as in machine learning or data analysis contexts. This class serves as a utility within the codebase, providing essential functions to compare and categorize data based on their vector representations.

## Variables
- **vectorA**: An array of floats representing the first vector in the cosine similarity calculation.
- **vectorB**: An array of floats representing the second vector in the cosine similarity calculation.
- **embeddings**: A list of float arrays, where each array represents an embedding that needs to be categorized based on similarity.
- **similarityThreshold**: A float value that defines the minimum cosine similarity required for two embeddings to be considered similar.

## Functions
### `ComputeCosineSimilarity(float[] vectorA, float[] vectorB)`
This static function takes two float arrays (vectors) as input and calculates the cosine similarity between them. It computes the dot product of the vectors and their magnitudes, returning a float value that represents how similar the two vectors are. A value of 1 indicates that the vectors are identical, while a value of 0 indicates no similarity.

### `CategorizeData(List<float[]> embeddings, float similarityThreshold)`
This static function takes a list of float arrays (embeddings) and a similarity threshold as input. It categorizes the embeddings into groups based on their cosine similarity. If an embedding is similar enough to an existing category (as determined by the similarity threshold), it is added to that category. If it does not meet the threshold for any existing category, a new category is created for it. The function returns a list of lists, where each inner list contains embeddings that are similar to each other.