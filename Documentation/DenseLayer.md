# DenseLayer

## Overview
The `DenseLayer` script represents a single layer in a neural network, specifically a dense (fully connected) layer. In this layer, each neuron is connected to every neuron in the previous layer. The main function of this script is to initialize weights and biases for the layer, perform forward propagation using the input data, and apply an activation function (hyperbolic tangent) to the output. This script fits within a larger codebase that likely implements a neural network, where multiple layers are combined to form a complete model.

## Variables
- `weights`: A 2D array of floats that stores the weights for connections between the input neurons and the output neurons. The dimensions are determined by the size of the input and output.
- `biases`: A 1D array of floats that holds the bias values for each output neuron. It is initialized to zero for each neuron in the layer.

## Functions
- `DenseLayer(int inputSize, int outputSize)`: Constructor that initializes the weights and biases for the layer based on the specified input and output sizes. It also calls the `InitializeWeights` function to set initial values for the weights.
  
- `InitializeWeights()`: Private method that populates the `weights` array with random values between -1 and 1 and initializes the `biases` array to zero. This sets up the layer for training and inference.

- `GetWeights()`: Public method that returns the current weights of the layer as a 2D array. This allows other parts of the code to access the layer's weights.

- `GetBiases()`: Public method that returns the current biases of the layer as a 1D array. This provides access to the layer's bias values.

- `Forward(float[] input, float[,] weights, float[] biases)`: Public method that performs the forward propagation step. It takes an input array, the weights, and biases as parameters, computes the output for each neuron in the layer, applies the hyperbolic tangent activation function, and returns the resulting output array.