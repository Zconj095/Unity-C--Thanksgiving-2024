# LinearNewtonMethod

## Overview
The `LinearNewtonMethod` class implements a machine learning algorithm based on the Newton-Raphson method for training linear models. It is designed to optimize weights and bias for a given set of inputs and outputs, using gradient descent techniques. This class fits into the broader context of machine learning within the `EdgeLoreMachineLearning` namespace, providing a structured approach to linear regression or classification tasks.

## Variables
- `kernel`: An instance of a type that implements the `ILinear<TInput>` interface, responsible for defining the linear function used during training.
- `weights`: An array of doubles representing the weights assigned to each input feature, which are adjusted during training.
- `bias`: A double representing the bias term in the linear model, which is also optimized during training.
- `tolerance`: A double that sets the threshold for convergence. The training process stops when the objective function is below this value (default is 0.1).
- `maxIterations`: An integer that defines the maximum number of iterations for the training process (default is 1000).

## Properties
- `Tolerance`: Gets or sets the convergence threshold. Throws an exception if the value is less than or equal to zero.
- `MaxIterations`: Gets or sets the maximum number of iterations for training. Throws an exception if the value is less than or equal to zero.

## Functions
- **Train(TInput[] inputs, int[] outputs, double[] regularization)**: 
  Trains the linear model using the provided input data, output labels, and regularization parameters. It iteratively computes decision values, the objective function, gradient, and Hessian, updating the weights and checking for convergence.

- **ComputeDecision(TInput input)**: 
  Computes the decision value for a given input based on the current weights and bias.

- **ComputeObjective(double[] z, int[] outputs, double[] regularization)**: 
  Calculates the objective function value based on the decision values, outputs, and regularization terms.

- **ComputeGradient(TInput[] inputs, int[] outputs, double[] z, double[] regularization, double[] gradient)**: 
  Computes the gradient of the objective function with respect to the weights, which is used to update the weights during training.

- **ComputeHessian(TInput[] inputs, int[] outputs, double[] z, double[] regularization, double[] hessian)**: 
  Computes the Hessian matrix (second derivative) of the objective function, which is used to determine the step size for weight updates.

- **UpdateWeights(double[] gradient, double[] hessian)**: 
  Updates the weights using the computed gradient and Hessian based on the Newton-Raphson method.

- **Predict(TInput input)**: 
  Makes a prediction for a given input by computing the decision value and returning 1 if it is greater than zero, or -1 otherwise.

## Interface
- **ILinear<TInput>**: 
  An interface that defines a method for calculating the output of a linear function given weights and an input, ensuring that any implementing class provides a way to evaluate the linear relationship.