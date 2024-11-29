# FractalDeepLearning

## Overview
The `FractalDeepLearning` script implements a simple neural network architecture in Unity, where each layer's weights are generated using a fractal function. This script allows for the creation of a deep learning model that processes input data through multiple layers, applying transformations based on the weights and biases defined for each layer. The main function of this script is to perform a forward pass through the neural network, taking an input array and producing an output array after processing through all layers.

## Variables

- **layers**: A list of `Layer` objects that represent each layer of the neural network. Each layer contains weights and biases used for computations during the forward pass.

## Functions

- **FractalDeepLearning(int[] layerSizes)**: Constructor that initializes the neural network with a specified number of layers based on the provided `layerSizes` array. Each layer is created with the appropriate input and output sizes.

- **Forward(float[] input)**: This method takes an input array and processes it through all the layers of the neural network. It returns the final output after applying the activation function at each layer.

- **ActivateLayer(float[] input, Layer layer)**: This method takes an input array and a `Layer` object, computes the output for that layer by multiplying the input with the layer's weights, adding biases, and applying the hyperbolic tangent activation function. It returns the resulting output array for that layer.

### Inner Class - Layer

- **Layer**: Represents a single layer of the neural network. It contains the following properties and methods:
  - **Weights**: A 2D array of floats representing the connection weights between neurons in the layer.
  - **Biases**: A 1D array of floats representing the biases for each neuron in the layer.
  - **Layer(int inputSize, int outputSize)**: Constructor that initializes the weights using the `GenerateFractalWeights` method and sets biases to zero.
  - **GenerateFractalWeights(int inputSize, int outputSize)**: Generates a 2D array of weights using a fractal function based on the input and output sizes.
  - **FractalFunction(int i, int j)**: A private method that computes the weight value using a simple fractal formula, specifically approximating the Mandelbrot set through sine and cosine functions.