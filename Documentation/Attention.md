# Attention Script Documentation

## Overview
The `Attention` class is designed to implement attention mechanisms commonly used in machine learning models, particularly in natural language processing and computer vision. The main function of this script, `ComputeAttention`, calculates attention scores based on provided query, keys, and values, and outputs a weighted sum of the values. This class serves as a utility within a larger codebase that may involve neural network architectures, enabling the model to focus on specific parts of the input data based on learned attention weights.

## Variables
- **query (float[])**: An array representing the input query vector that is used to compute attention scores against the keys.
- **keys (float[][])**: A two-dimensional array where each sub-array is a key vector. These vectors are compared against the query to determine attention scores.
- **values (float[][])**: A two-dimensional array where each sub-array contains the value vectors corresponding to the keys. These values are weighted and summed based on the attention scores.
- **scores (float[])**: An array that holds the dot product scores computed between the query and each key.
- **attentionWeights (float[])**: An array that contains the softmax-normalized attention weights derived from the scores.
- **output (float[])**: An array that accumulates the weighted sum of the value vectors, producing the final output of the attention mechanism.
- **result (float)**: A variable used to store the result of the dot product calculation.
- **expScores (float[])**: An array that holds the exponential values of the scores for the softmax calculation.
- **sum (double)**: A variable that accumulates the total of the exponential scores to normalize the attention weights.

## Functions
- **ComputeAttention(float[] query, float[][] keys, float[][] values)**: 
  This static method computes the attention output by first calculating the dot product scores between the query and each key, applying the softmax function to these scores to obtain attention weights, and finally producing a weighted sum of the corresponding values based on these weights.

- **DotProduct(float[] a, float[] b)**: 
  This private static method calculates the dot product of two vectors `a` and `b`. It iterates through each element of the vectors, multiplying corresponding elements and summing the results to obtain a single scalar value.

- **Softmax(float[] scores)**: 
  This private static method takes an array of scores and applies the softmax function to convert them into a probability distribution. It computes the exponentials of the scores, sums them, and then normalizes each score by dividing by this sum.

- **FeedForward(float[] input, float[,] weights, float[] biases)**: 
  This static method implements a simple feedforward operation for a layer in a neural network. It computes the output by applying the weights and biases to the input vector, resulting in an output vector that represents the activations of the next layer.