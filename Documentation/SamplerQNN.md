# SamplerQNN

## Overview
The `SamplerQNN` class implements a sampler-based Quantum Neural Network (QNN) in Unity. This class is designed to simulate a neural network using a parameterized quantum circuit. It provides functionalities for initializing the network, performing forward and backward passes, and managing the network's weights. The `SamplerQNN` fits into the broader codebase by allowing developers to leverage quantum computing principles in neural network architectures, facilitating advanced machine learning applications.

## Variables
- `numInputs`: An integer representing the number of input features for the neural network.
- `numWeights`: An integer indicating the number of trainable weights in the network.
- `sparse`: A boolean that determines whether the output of the network is sparse or not.
- `outputShape`: An array of integers defining the shape of the network's output.
- `interpret`: A function that interprets the output of the sampler, taking an integer and returning an integer.
- `weights`: An array of floats representing the trainable weights of the network.
- `forwardFunction`: A function delegate for performing forward computation using the quantum circuit.
- `backwardFunction`: A function delegate for performing backward computation to calculate gradients.

## Functions
- `Initialize(object quantumCircuit, int? numInputs = null, int? numWeights = null, bool sparseOutput = false, Func<int, int> interpretFunction = null, int[] customOutputShape = null)`: 
  Initializes the `SamplerQNN` with a specified quantum circuit and optional parameters for inputs, weights, output sparsity, interpretation function, and custom output shape.

- `Forward(float[] inputData)`: 
  Executes a forward pass through the quantum circuit using the provided input data and current weights. It returns the processed output.

- `Backward(float[] inputData)`: 
  Performs a backward pass to compute gradients with respect to the input data and the weights. It returns a tuple containing input gradients and weight gradients.

- `RandomizeWeights()`: 
  Initializes the weights of the network randomly within the range of -1 to 1.

- `ValidateInputs(float[] inputData, float[] weights)`: 
  Checks the compatibility of the input data and weights with the network's expected dimensions, throwing exceptions if there are mismatches.

- `ProcessOutput(float[] rawOutput, int batchSize)`: 
  Processes the raw output from the forward pass to ensure it matches the expected dimensions based on the batch size and output shape.

- `ReshapeGradients(float[] gradients, int batchSize, int parameterCount)`: 
  Reshapes the gradients to align with the specified batch size and parameter count, ensuring dimensional consistency.

- `GetPropertyValue<T>(object target, string propertyName)`: 
  Retrieves the value of a specified property from a target object using reflection, throwing an exception if the property does not exist.

- `CreateMethodDelegate<TInput1, TInput2, TOutput>(object target, string methodName)`: 
  Dynamically creates a delegate for a method with two input parameters from a target object, throwing an exception if the method does not exist.