# LinearDualCoordinateDescent

## Overview
The `LinearDualCoordinateDescent` class is a machine learning algorithm that implements the dual coordinate descent method for training linear models. It is designed to work with various input types and kernels, allowing for flexibility in different machine learning tasks. This class is part of the `EdgeLoreMachineLearning` namespace, which likely contains other machine learning algorithms and utilities. The main function of this script is to train a linear model using the provided input data, outputs, and regularization parameters, and to make predictions based on the trained model.

## Variables
- `TKernel kernel`: An instance of the kernel that defines how to compute the similarity between input data points.
- `double[] alpha`: An array of multipliers corresponding to each training sample, used in the optimization process.
- `double[] weights`: An array representing the weights of the features in the linear model.
- `double bias`: The bias term in the linear model, which helps in adjusting the decision boundary.
- `double tolerance`: A threshold value that determines when the training process should stop, defaulted to 0.1.
- `int maxIterations`: The maximum number of iterations allowed for the training process, defaulted to 1000.
- `Loss loss`: An enumeration value that specifies the loss function to be used during training (either L1 or L2).

## Properties
- `double Tolerance`: Gets or sets the tolerance value. Throws an exception if the value is less than or equal to zero.
- `int MaxIterations`: Gets or sets the maximum number of iterations. Throws an exception if the value is less than or equal to zero.
- `Loss LossFunction`: Gets or sets the loss function used in training.

## Functions
- `void Train(TInput[] inputs, int[] outputs, double[] regularization)`: Trains the linear model using the provided input data, output labels, and regularization parameters. It initializes the weights and multipliers, computes the necessary values, and iteratively updates the model until convergence or until the maximum number of iterations is reached.
  
- `private void UpdateWeights(TInput input, double d)`: Updates the weights of the model based on the provided input data and the change `d` in the multipliers.

- `private double ComputeKernel(TInput x, TInput y)`: Computes the kernel value (similarity) between two input data points `x` and `y`.

- `private double ComputeDecisionFunction(TInput input)`: Calculates the decision function value for a given input, which is used to determine the output of the model.

- `int Predict(TInput input)`: Makes a prediction for the given input based on the trained model, returning 1 for positive class and -1 for negative class.

## Enum
- `public enum Loss`: Defines the types of loss functions that can be used during training, specifically L1 and L2 loss functions.