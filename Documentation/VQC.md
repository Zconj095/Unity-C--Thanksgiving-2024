# VQC (Variational Quantum Circuit)

## Overview
The `VQC` class simulates a Variational Quantum Circuit that processes input data through a feature map and ansatz, computes the loss, and trains the weights using gradient descent. This class is designed to be used in conjunction with Unity, enabling developers to implement quantum-inspired algorithms in a game or simulation environment. The main function of this class is to initialize the quantum circuit, train it with input data, and make predictions based on the trained model.

## Variables
- `numQubits`: An integer representing the number of qubits in the quantum circuit.
- `featureMap`: A function that maps input data to a feature space.
- `ansatz`: A function that represents the variational form of the quantum circuit.
- `circuit`: A function that combines the feature map and ansatz to process input data.
- `lossFunction`: A function that computes the loss between predicted and target outputs.
- `weights`: An array of floats representing the weights of the quantum circuit.
- `callback`: An optional action that can be invoked to monitor the training progress, receiving weights and loss as parameters.

## Functions
- `Initialize(object model, int? numQubits = null, string featureMapMethod = "DefaultFeatureMap", string ansatzMethod = "DefaultAnsatz", string lossFunctionMethod = "DefaultLossFunction", string callbackMethod = null)`: 
  Initializes the VQC model with specified parameters and methods using reflection. It dynamically retrieves the feature map, ansatz, loss function, and optional callback method from the provided model.

- `RandomizeWeights()`: 
  Randomly initializes the weights of the quantum circuit to values between -1 and 1.

- `Train(float[,] X, float[,] y, int epochs, float learningRate)`: 
  Trains the VQC model using gradient descent. It iterates through the dataset for a specified number of epochs, computes the loss for each sample, and updates the weights based on the calculated gradients.

- `PredictSingle(float[] input)`: 
  Predicts the output for a single input sample by processing it through the quantum circuit and applying the softmax transformation to obtain probabilities.

- `Softmax(float[] input)`: 
  Computes the softmax transformation for a given vector, stabilizing the computation by subtracting the maximum value in the input array.

- `GetRow(float[,] matrix, int row)`: 
  Retrieves a specific row from a 2D array and returns it as a 1D float array.

- `CreateSingleInputDelegate<TInput, TOutput>(object target, string methodName)`: 
  Dynamically creates a delegate for a method with one input parameter, allowing the method to be invoked with the specified input.

- `CreateDoubleInputDelegate<TInput1, TInput2, TOutput>(object target, string methodName)`: 
  Dynamically creates a delegate for a method with two input parameters, enabling invocation of the method with two specified inputs.

- `CreateVoidDelegate<TInput1, TInput2>(object target, string methodName)`: 
  Dynamically creates a delegate for a void method with two input parameters, allowing the method to be invoked without returning a value.