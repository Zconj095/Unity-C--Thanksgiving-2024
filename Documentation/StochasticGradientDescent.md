# StochasticGradientDescent

## Overview
The `StochasticGradientDescent` class implements a stochastic gradient descent algorithm for training a machine learning model using the logistic loss function. This script is designed to fit into the EdgeLoreMachineLearning codebase, allowing for efficient training of models by adjusting weights and biases based on input data and expected outputs. The class utilizes reflection for debugging purposes, enabling developers to inspect the internal state of the class during runtime.

## Variables
- `learningRate`: A double that determines the step size at each iteration while moving toward a minimum of the loss function. Default value is `0.01`.
- `regularizationTerm`: A double that adds a penalty to the loss function to prevent overfitting. Default value is `1e-5`.
- `tolerance`: A double that specifies the threshold for convergence. Training stops if the total loss is below this value. Default value is `1e-5`.
- `maxIterations`: An integer that sets the maximum number of iterations for the training process. Default value is `100`.
- `useBias`: A boolean that indicates whether to include a bias term in the model. Default value is `true`.
- `weights`: An array of doubles representing the weights for each feature in the input data.
- `bias`: A double representing the bias term in the model.
- `iterationCount`: An integer that counts the number of iterations completed during training.
- `fields`: An array of `FieldInfo` objects used to hold the fields of the class for reflection-based debugging.

## Functions
- `void Start()`: Unity's lifecycle method that initializes the training process by calling the `Init` method when the script starts.
  
- `private void Init()`: Initializes the weights, bias, and iteration count to their default values. It also uses reflection to log the internal state of the class for debugging purposes.

- `public void Train(double[][] inputs, bool[] outputs, LossFunctionType lossType = LossFunctionType.LogisticLoss)`: Trains the model using the provided input data and expected outputs. It adjusts weights and bias based on the computed error from the logistic loss function for a specified number of iterations or until convergence is achieved.

- `private double ComputeScore(double[] input)`: Computes the score (output) of the model for a given input by calculating the dot product of the weights and input features, adding the bias if applicable.

- `private double ComputeError(double predicted, bool actual, LossFunctionType lossType)`: Calculates the error between the predicted output and the actual output based on the specified loss function. Currently, it only implements the logistic loss.

- `public void PrintWeightsAndBias()`: Logs the current values of the weights and bias to the console for inspection.

- `public void Reset()`: Resets the internal state of the model by re-initializing the weights, bias, and iteration count.