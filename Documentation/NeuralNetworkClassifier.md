# NeuralNetworkClassifier

## Overview
The `NeuralNetworkClassifier` class is a Unity MonoBehaviour that implements a simple neural network for classification tasks. It allows users to initialize the network with specified input and output sizes, train the model using a dataset, and make predictions on new data. The class supports options for different loss functions and one-hot encoding for output labels. This classifier can be integrated into larger machine learning or game development projects within Unity, enabling intelligent decision-making based on input data.

## Variables
- `weights`: A 2D array of floats representing the weights of the neural network. The dimensions are defined by the number of input and output nodes.
- `lossFunction`: A string that specifies the loss function used during training (default is "squared_error").
- `oneHotEncoding`: A boolean flag indicating whether to use one-hot encoding for the output labels.
- `isFitted`: A boolean flag that indicates whether the model has been trained (fitted) on the data.
- `numClasses`: A nullable integer that holds the number of distinct classes in the output data, determined after training.

## Functions
- `Initialize(int inputSize, int outputSize, string lossFunction = "squared_error", bool oneHotEncoding = false)`: Initializes the neural network with the specified input size, output size, loss function, and one-hot encoding option. It also calls the `InitializeWeights` function to set up the initial weights.

- `InitializeWeights()`: Randomly initializes the weights of the neural network to values between -1 and 1.

- `Train(float[,] X, float[,] y, int epochs, float learningRate)`: Trains the neural network using the provided input (`X`) and target (`y`) datasets over a specified number of epochs with a given learning rate. The function updates the weights using gradient descent and computes the number of classes based on the training data.

- `PredictSingle(float[] input)`: Predicts the output for a single input sample. It calculates the weighted sum of the inputs, applies the sigmoid activation function, and returns the predicted output, optionally applying one-hot encoding.

- `Predict(float[,] X)`: Predicts outputs for multiple input samples. It iterates through each sample in the input dataset, calls `PredictSingle` for each, and stores the results in a 2D array of predictions.

- `OneHotEncode(float[] probabilities)`: Converts an array of probabilities into a one-hot encoded format, where the index of the maximum probability is set to 1, and all other indices are set to 0.

- `Sigmoid(float x)`: Applies the sigmoid activation function to a given value `x`, returning a value between 0 and 1.

- `GetRow(float[,] matrix, int row)`: Extracts a specific row from a 2D array (matrix) and returns it as a 1D array.

- `NumClasses`: A public property that returns the number of classes determined during training.

- `IsFitted`: A public property that indicates whether the model has been trained.