# NeuralOptimizer

## Overview
The `NeuralOptimizer` class is designed to manage the weights and biases of a neural network during the optimization process. It initializes the weights randomly and allows for their adjustment based on gradients, facilitating the learning process of a neural network. This class fits within a larger codebase that likely includes components for neural network architecture and training, enabling the optimization of model parameters to improve performance.

## Variables
- `weights`: A two-dimensional array of floats representing the connection strengths between input and output nodes. The dimensions are defined by the `inputSize` and `outputSize` parameters.
- `biases`: A one-dimensional array of floats that holds the bias values for each output node. The length corresponds to the `outputSize` parameter.

## Functions
- `NeuralOptimizer(int inputSize, int outputSize)`: Constructor that initializes the `weights` and `biases` arrays based on the specified input and output sizes. It also calls the `InitializeWeights` method to set the initial values of the weights and biases.
  
- `InitializeWeights()`: Private method that populates the `weights` array with random values between -1 and 1 and sets all values in the `biases` array to 0. This method is called during the construction of the `NeuralOptimizer` object to ensure that the network starts with a diverse set of weights.

- `AdjustWeights(float[] gradients)`: Public method that updates the `weights` based on the provided gradients. It applies a simple gradient descent algorithm by subtracting a scaled version of the gradients from the weights. The learning rate is set to 0.01, which determines the step size of the weight adjustments.