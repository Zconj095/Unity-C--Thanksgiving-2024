# NeuralNetworkRegressor

## Overview
The `NeuralNetworkRegressor` class implements a simple neural network for regression tasks using Unity's MonoBehaviour. It consists of an input layer, a hidden layer, and an output layer. The primary function of this script is to train the neural network on provided input data and targets, allowing it to learn and make predictions based on new inputs. It fits within a codebase that likely involves machine learning applications or simulations within Unity.

## Variables
- `_inputSize`: An integer representing the number of input features for the neural network.
- `_hiddenSize`: An integer representing the number of neurons in the hidden layer.
- `_outputSize`: An integer representing the number of output features (predictions).
- `_weights1`: A 2D array that holds the weights between the input layer and the hidden layer.
- `_weights2`: A 2D array that holds the weights between the hidden layer and the output layer.
- `_bias1`: A 1D array that holds the biases for the hidden layer neurons.
- `_bias2`: A 1D array that holds the biases for the output layer neurons.
- `_learningRate`: A float that determines the step size at each iteration while moving toward a minimum of the loss function.
- `_X`: A 2D array that stores the input data used for training the model.
- `_y`: A 2D array that stores the target values corresponding to the input data.

## Functions
- **NeuralNetworkRegressor(int inputSize, int hiddenSize, int outputSize, float learningRate = 0.01f)**: Constructor that initializes the neural network with the specified sizes for input, hidden, and output layers, as well as the learning rate. It also initializes weights and biases.
  
- **InitializeWeights()**: Private method that initializes weights and biases with small random values for the weights and zeros for the biases.

- **Sigmoid(float x)**: Private method that applies the sigmoid activation function to a given input `x`, returning a value between 0 and 1.

- **SigmoidDerivative(float x)**: Private method that computes the derivative of the sigmoid function, which is used during backpropagation.

- **Forward(float[,] X)**: Public method that performs a forward pass through the network. It takes input data `X`, computes the activations for the hidden layer and the output layer, and returns the output layer's activations.

- **ComputeLoss(float[,] predictions, float[,] y)**: Private method that calculates the Mean Squared Error (MSE) loss between the predicted values and the actual target values.

- **Backward(float[,] X, float[,] y, float[,] predictions)**: Private method that performs backpropagation to calculate gradients of the loss function with respect to weights and biases, updating them using gradient descent.

- **Fit(float[,] X, float[,] y, int epochs = 1000)**: Public method that trains the neural network using the provided input data `X` and target values `y` for a specified number of epochs. It logs the loss at each epoch to monitor training progress.