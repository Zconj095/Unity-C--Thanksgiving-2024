# DenseLayer2

## Overview
The `DenseLayer2` class represents a dense layer in a neural network, commonly used in machine learning and artificial intelligence applications. This layer processes input data through a set of weights and biases, applying a non-linear activation function (hyperbolic tangent) to produce an output. The `DenseLayer2` class is designed to be part of a larger codebase that implements neural network architectures, facilitating the propagation of data through the network's layers.

## Variables
- `weights`: A 2D array of floats that holds the connection weights between the input and output neurons. The dimensions are defined by the `inputSize` and `outputSize` parameters.
- `biases`: A 1D array of floats that contains the bias values for each output neuron. The length of this array corresponds to the number of output neurons.

## Functions
- **DenseLayer2(int inputSize, int outputSize)**: Constructor that initializes the weights and biases for the dense layer. It takes two parameters: `inputSize` (the number of input features) and `outputSize` (the number of neurons in the layer). It also calls the `InitializeWeights` method to set the initial values of weights and biases.

- **InitializeWeights()**: A private method that initializes the weights with random values between -1 and 1 and sets all biases to zero. It uses a random number generator to ensure that weights are initialized differently each time.

- **Forward(float[] input)**: This public method takes an array of input values and computes the output of the dense layer. It performs a matrix multiplication of the input with the weights, adds the biases, and applies the hyperbolic tangent activation function to the result. The method returns an array of output values corresponding to the output neurons.