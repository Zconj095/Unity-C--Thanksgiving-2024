# AttentionMechanism

## Overview
The `AttentionMechanism` class is designed to compute attention weights and outputs based on a given query, keys, and values. This class is typically part of a larger codebase that implements attention mechanisms, commonly used in machine learning models, particularly in natural language processing and computer vision. The main function, `ComputeAttention`, calculates the attention output by applying the attention weights to the provided values, allowing the model to focus on different parts of the input data based on the query.

## Variables
- `scores`: An array of floats that stores the computed attention scores for each key relative to the query. It is initialized to the length of the keys array.
- `attentionWeights`: An array of floats that contains the normalized attention weights obtained from applying the softmax function to the scores.
- `output`: An array of floats that will hold the final output of the attention mechanism, initialized to the length of the first value array.
- `expScores`: An array of floats used to store the exponentiated scores during the softmax calculation.
- `sum`: A float that accumulates the total of the exponentiated scores for normalization in the softmax function.

## Functions
- `ComputeAttention(float[] query, float[][] keys, float[][] values)`: 
  This is the main function of the class. It computes the attention output by first calculating the dot products between the query and each key to generate scores. It then applies the softmax function to these scores to obtain the attention weights. Finally, it combines the values using these weights to produce the output.

- `DotProduct(float[] a, float[] b)`: 
  This private helper function calculates the dot product of two float arrays. It iterates through each element of the arrays, multiplying corresponding elements and summing the results to return a single float value.

- `Softmax(float[] scores)`: 
  This private helper function computes the softmax of an array of scores. It exponentiates each score, sums them up, and then normalizes each exponentiated score by dividing it by the total sum, resulting in an array of probabilities that sum to one.