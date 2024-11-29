# MEMORYRECALLLSTM

## Overview
The `MEMORYRECALLLSTM` class implements a Long Short-Term Memory (LSTM) neural network unit, which is a type of recurrent neural network (RNN). This class is designed to process sequences of data while maintaining a memory of past inputs, making it suitable for tasks like time series prediction, natural language processing, and more. The LSTM unit consists of various gates that control the flow of information, allowing the network to learn dependencies over time. The LSTM's functionality fits within a larger codebase that may involve training models, making predictions, or processing sequential data.

## Variables
- `inputSize`: An integer representing the size of the input vector for each time step.
- `hiddenSize`: An integer representing the number of hidden units in the LSTM cell.
- `Wf`: A 2D array (matrix) representing the weights for the forget gate.
- `Wi`: A 2D array (matrix) representing the weights for the input gate.
- `Wo`: A 2D array (matrix) representing the weights for the output gate.
- `Wc`: A 2D array (matrix) representing the weights for the cell input gate.
- `bf`: A 1D array (vector) representing the bias terms for the forget gate.
- `bi`: A 1D array (vector) representing the bias terms for the input gate.
- `bo`: A 1D array (vector) representing the bias terms for the output gate.
- `bc`: A 1D array (vector) representing the bias terms for the cell input gate.
- `cellState`: A 1D array (vector) representing the current cell state of the LSTM.
- `hiddenState`: A 1D array (vector) representing the current hidden state of the LSTM.

## Functions
- `MEMORYRECALLLSTM(int inputSize, int hiddenSize)`: Constructor that initializes the LSTM with the specified input size and hidden size, and calls the method to initialize weights.

- `InitializeWeights()`: This private method initializes the weight matrices and bias vectors for the LSTM gates and sets the initial cell and hidden states.

- `RandomMatrix(int rows, int cols)`: A private method that generates a matrix of random float values between -1 and 1 with the specified number of rows and columns.

- `RandomVector(int size)`: A private method that generates a vector of random float values between -1 and 1 with the specified size.

- `Forward(float[] input)`: Public method that computes the forward pass of the LSTM unit. It takes an input vector, processes it through the LSTM gates, updates the cell and hidden states, and returns the new hidden state.

- `CombineArrays(float[] a, float[] b)`: A private method that combines two arrays into a single array.

- `Dot(float[,] matrix, float[] vector)`: A private method that performs matrix-vector multiplication and returns the resulting vector.

- `Add(float[] a, float[] b)`: A private method that adds two vectors element-wise and returns the result.

- `Sigmoid(float[] x)`: A private method that applies the sigmoid activation function to each element of the input array and returns the resulting array.

- `Tanh(float x)`: A private method that computes the hyperbolic tangent of a single float value.

- `Tanh(float[] x)`: A private method that applies the hyperbolic tangent function to each element of the input array and returns the resulting array.