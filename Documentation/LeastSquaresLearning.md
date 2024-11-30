# LeastSquaresLearning

## Overview
The `LeastSquaresLearning` class is a generic implementation of a least squares learning algorithm, designed to facilitate machine learning tasks within the EdgeLoreMachineLearning namespace. It utilizes kernel methods to predict outputs based on given inputs and their corresponding outputs. This class is intended for use in scenarios where a predictive model is needed, such as regression tasks. It fits within a broader codebase that likely includes various machine learning algorithms and utilities for data handling and processing.

## Variables
- `kernel`: An instance of the kernel type `TKernel`, which defines the kernel function used for calculations.
- `tolerance`: A float value representing the threshold for convergence in the conjugate gradient method. Default is set to `1e-6f`.
- `diagonal`: An array of floats that holds the diagonal elements used in the conjugate gradient method.
- `weights`: An array of floats representing the learned weights for the model.
- `threshold`: A float value that serves as the bias term for the model predictions.

## Functions
- **Tolerance Property**
  - `public float Tolerance`: Gets or sets the tolerance value. Throws an exception if the value is less than or equal to zero.

- **Train**
  - `public void Train(TInput[] inputs, int[] outputs)`: Trains the model using the provided input data and corresponding output labels. It initializes necessary data structures, computes the kernel cache, solves for model parameters using the conjugate gradient method, and calculates the final weights and threshold.

- **ConjugateGradient**
  - `private float[] ConjugateGradient(float[] target, float[,] kernelCache, float[] diagonal, int length)`: Performs the conjugate gradient method to solve a system of linear equations. It iteratively refines the solution until it converges to the specified tolerance.

- **ComputeKernel**
  - `private float ComputeKernel(TInput x, TInput y)`: Computes the kernel value between two input vectors `x` and `y`. In this implementation, it uses a linear kernel, calculated as the dot product of the two vectors.

- **VectorCreate**
  - `private static float[] VectorCreate(int length, float value)`: Creates and returns an array (vector) of a specified length, initializing all elements to a given value.

- **DotProduct**
  - `private static float DotProduct(float[] a, float[] b)`: Calculates and returns the dot product of two float arrays `a` and `b`.

- **Predict**
  - `public float Predict(TInput input, TInput[] supportVectors)`: Makes a prediction for a given input based on the learned model. It computes the predicted output using the weights and kernel values associated with the support vectors, adding the threshold to the result.