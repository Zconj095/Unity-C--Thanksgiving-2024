# TSNE

## Overview
The `TSNE` class implements the t-Distributed Stochastic Neighbor Embedding (t-SNE) algorithm specifically for Unity. t-SNE is a popular technique for dimensionality reduction, often used in the field of machine learning to visualize high-dimensional data in a lower-dimensional space, typically 2D. This class provides functionality to transform high-dimensional data (in the form of `Vector3` arrays) into a 2D representation (`Vector2` arrays), making it easier to visualize complex datasets within Unity.

## Variables
- **perplexity**: A private double variable that defines the perplexity parameter for the t-SNE algorithm. The default value is set to 50. Perplexity is a measure of how many neighbors each point has and affects the balance between local and global aspects of the data.
  
- **theta**: A private double variable that represents the theta parameter for t-SNE. The default value is set to 0.5. Theta controls the trade-off between speed and accuracy in the optimization process of the t-SNE algorithm.

## Properties
- **Perplexity**: A public property that allows getting and setting the perplexity value. It provides an interface for other parts of the codebase to modify the perplexity used in the t-SNE transformation.

- **Theta**: A public property that allows getting and setting the theta value. Similar to perplexity, it provides an interface for managing the theta parameter used in the t-SNE process.

## Functions
- **Transform(Vector3[] input)**: This public method takes an array of high-dimensional data represented as `Vector3` objects and applies the t-SNE transformation to reduce it to a lower-dimensional space (2D). The method performs the following steps:
  1. Converts the input `Vector3` data into a double array format for computation.
  2. Prepares a container for the output data.
  3. Calls the `RunTSNE` method to perform the actual t-SNE algorithm.
  4. Converts the resulting output back into `Vector2` format for Unity compatibility and returns it.

- **RunTSNE(double[][] input, double[][] output, double perplexity, double theta)**: This private method is responsible for executing the core t-SNE algorithm logic. It takes the following parameters:
  - `input`: A double array representing high-dimensional input data.
  - `output`: A double array that will store the resulting low-dimensional data.
  - `perplexity`: The perplexity value to be used in the t-SNE calculation.
  - `theta`: The theta value to be used in the t-SNE calculation.
  
  The method includes placeholder comments for implementing the t-SNE algorithm, including input validation, initialization, normalization, and optimization logic. It ensures that the perplexity is appropriate for the number of data points provided.