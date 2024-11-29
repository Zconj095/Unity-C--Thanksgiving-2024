# NeuralNetworkSystem

## Overview
The `NeuralNetworkSystem` class serves as an abstract base for implementing neural networks in Unity. It provides the foundational functionality for both forward and backward passes, allowing for the processing of batched inputs. This class is intended to be extended by specific implementations of neural networks, such as quantum neural networks, which will define the actual computation logic for the forward and backward passes.

## Variables

- **numInputs**: 
  - Type: `int`
  - Description: Represents the number of input features that the neural network expects.

- **numWeights**: 
  - Type: `int`
  - Description: Indicates the number of trainable weights in the neural network.

- **sparse**: 
  - Type: `bool`
  - Description: A flag that specifies whether the output of the neural network is sparse.

- **outputShape**: 
  - Type: `int[]`
  - Description: An array defining the shape of the output produced by the neural network.

- **inputGradients**: 
  - Type: `bool`
  - Description: A flag indicating whether to compute gradients with respect to the inputs during the backward pass.

## Functions

- **Initialize(int numInputs, int numWeights, bool sparse, int[] outputShape, bool inputGradients = false)**: 
  - Description: Initializes the neural network with the specified input, weight, and output configurations. Throws exceptions if the inputs are invalid (e.g., negative numbers or invalid output shape).

- **Forward(float[] inputData, float[] weights)**: 
  - Description: Executes the forward pass of the neural network using the provided input data and weights. It validates the inputs, performs the forward computation, and reshapes the output to match the expected dimensions.

- **Backward(float[] inputData, float[] weights)**: 
  - Description: Executes the backward pass of the neural network, calculating gradients for both inputs and weights. It validates the inputs, performs the backward computation, and reshapes the gradients to the expected dimensions.

- **ValidateInputs(float[] inputData, float[] weights)**: 
  - Description: Validates that the input data and weights are compatible with the network's specifications. Throws exceptions if any validation checks fail.

- **ReshapeOutput(float[] outputData, int batchSize)**: 
  - Description: Reshapes the output data from the forward pass to ensure it matches the expected batch and output dimensions. Throws an exception if the output data length does not match the expected shape.

- **ReshapeGradients(float[] gradients, int batchSize, int parameterCount)**: 
  - Description: Reshapes the gradients from the backward pass to ensure they correspond to the expected batch size and parameter count. Throws an exception if the dimensions do not match.

- **PerformForward(float[] inputData, float[] weights)**: 
  - Description: An abstract method that must be implemented by derived classes to define the specific logic for the forward pass.

- **PerformBackward(float[] inputData, float[] weights)**: 
  - Description: An abstract method that must be implemented by derived classes to define the specific logic for the backward pass.

- **GetPropertyValue<T>(object target, string propertyName)**: 
  - Description: Dynamically retrieves the value of a specified property from a target object using reflection. Throws an exception if the property is not found.

- **InvokeMethod<T>(object target, string methodName, params object[] args)**: 
  - Description: Dynamically invokes a specified method on a target object using reflection. Throws an exception if the method is not found.