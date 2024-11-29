# CognizantClusteredNeuralLayer

## Overview
The `CognizantClusteredNeuralLayer` class represents a single layer of a neural network, designed to process input data through weighted connections and biases. This class is integral to the neural network's architecture, allowing it to learn from input data by adjusting weights and biases based on computed errors during training. The layer utilizes a feedforward mechanism to compute outputs and a backpropagation method to update weights and biases based on error gradients.

## Variables
- `weights`: A 2D array of type `float` that holds the weights connecting the inputs to the neurons in the layer. The dimensions are defined by the number of inputs and outputs.
- `biases`: A 1D array of type `float` that contains the bias terms for each neuron in the layer. Each bias is added to the neuron's weighted input to adjust the output.
- `outputs`: A 1D array of type `float` that stores the output values of the neurons after processing the inputs.
- `learningRate`: A `float` value that determines how much to adjust the weights and biases during the learning process. A higher learning rate means larger adjustments.

## Functions
- `CognizantClusteredNeuralLayer(int inputSize, int outputSize, float learningRate)`: Constructor that initializes the neural layer with a specified number of inputs and outputs, as well as a learning rate. It also calls the `InitializeWeights` method to set initial weights and biases.

- `InitializeWeights()`: Private method that randomly initializes the weights and biases for the layer. Weights are assigned values between -0.5 and 0.5, ensuring a diverse starting point for the learning process.

- `Forward(float[] inputs)`: Public method that takes an array of input values, computes the outputs of the neurons by applying the weights and biases, and returns the resulting outputs after passing through the sigmoid activation function.

- `Backward(float[] inputs, float[] outputErrors)`: Public method that computes the gradient of the loss function with respect to the inputs, updating the weights and biases based on the output errors. It returns the calculated input errors that can be propagated back to the previous layer.

- `Sigmoid(float x)`: Private method that applies the sigmoid activation function to a given input `x`, transforming it into a value between 0 and 1.

- `SigmoidDerivative(float x)`: Private method that computes the derivative of the sigmoid function for a given input `x`. This derivative is used during the backpropagation process to adjust weights based on the error gradients.