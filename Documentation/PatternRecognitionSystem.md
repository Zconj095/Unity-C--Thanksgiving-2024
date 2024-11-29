# PatternRecognitionSystem

## Overview
The `PatternRecognitionSystem` script is designed to generate, classify, and validate patterns using vector representations. It fits within a codebase focused on memory retrieval and pattern recognition, allowing for the classification of memories based on their similarity to generated patterns. The system utilizes cosine similarity to determine the correlation between memory vectors and stored patterns, enabling effective classification and cross-validation of patterns.

## Variables

- `patterns`: A list of `VectorizedPattern` objects that store the generated patterns, each consisting of a vector, tags, and a meta tag.
- `random`: An instance of `System.Random` used to generate random numbers for creating patterns.

### Inner Class
- **VectorizedPattern**
  - `Vector`: A float array representing the pattern in vector form.
  - `Tags`: A list of strings that serve as tags associated with the pattern.
  - `MetaTag`: A string that acts as a higher-level category for the pattern.

## Functions

- **GeneratePatterns(int numPatterns, int vectorSize)**
  - Generates a specified number of random patterns, each with a defined vector size. It creates random vectors and assigns them random tags and meta tags, then stores them in the `patterns` list.

- **CrossCorrelate(float[] vectorA, float[] vectorB)**
  - Calculates the cosine similarity between two vectors, `vectorA` and `vectorB`. This function computes the dot product and magnitudes of both vectors to return a correlation score.

- **ClassifyMemory(string memoryContent, float correlationThreshold = 0.8f)**
  - Converts a given memory content into a vector representation and finds the best matching pattern from the `patterns` list based on cross-correlation. It returns the meta tag of the best match if the correlation score exceeds the specified threshold; otherwise, it returns "Unclassified".

- **PerformCrossValidation(int numFolds = 5)**
  - Conducts cross-validation on the `patterns` list by dividing it into a specified number of folds. It tests the classification accuracy of the patterns by comparing predicted meta tags with actual meta tags across training and testing sets.

- **ConnectToPatternRecognition(string memoryContent)**
  - Initializes a new instance of `PatternRecognitionSystem`, generates patterns, and classifies a provided memory content. It logs the classification result.

- **ClassifyPattern(VectorizedPattern pattern, List<VectorizedPattern> trainingSet)**
  - A helper function that classifies a given `VectorizedPattern` using a provided training set. It finds the best match based on cross-correlation and returns the corresponding meta tag.

- **Start()**
  - The Unity lifecycle method that is called when the script instance is being loaded. It generates random patterns, simulates a memory, classifies it, and performs cross-validation on the patterns.