# EstimatorQNN

## Overview
The `EstimatorQNN` class is a Unity-based implementation of a neural network that utilizes variational quantum circuits. It leverages the QuantumCircuit and Estimator primitives to perform both forward and backward passes, enabling the computation of predictions and gradients in a quantum machine learning context. This class serves as a bridge between traditional neural network operations and quantum computing frameworks, allowing for the integration of quantum algorithms into Unity applications.

## Variables
- `estimator`: An object representing the quantum estimator used for predictions.
- `gradient`: An object representing the gradient estimator used for computing gradients.
- `circuit`: An object representing the quantum circuit on which the neural network operates.
- `observables`: A list of objects that represent the observables used in the quantum circuit.
- `inputParams`: A list of objects that represent the input parameters for the quantum circuit.
- `weightParams`: A list of objects that represent the weight parameters for the quantum circuit.
- `inputGradients`: A boolean flag indicating whether to compute input gradients.
- `defaultPrecision`: A float representing the default precision for computations, defaulting to 0.015625f.

## Functions
- **Initialize**: 
  Initializes the `EstimatorQNN` with the necessary quantum circuit, estimator, and configuration options. It sets up the circuit, estimator, gradient, observables, input parameters, weight parameters, and computes input gradients if specified.

- **Forward**: 
  Executes a forward pass through the quantum neural network. It takes input data and weights, combines them into a unified parameter list, and runs the estimator to produce outputs.

- **Backward**: 
  Executes a backward pass through the quantum neural network to compute gradients. It similarly takes input data and weights, combines them, and runs the gradient estimator to obtain the gradients.

- **CombineParameters**: 
  Combines the input data and weights into a single array of parameters. It checks that the lengths of the input data and weights match the expected counts.

- **CreateDefaultGradient**: 
  Creates a default gradient object using Unity reflection based on the provided quantum estimator.

- **CreateDefaultObservables**: 
  Generates a list of default observables based on the quantum circuit, creating an observable for each qubit.

- **CreateDefaultObservable**: 
  Creates a default observable for the circuit using reflection based on the number of qubits.

- **GetInputParameters**: 
  Retrieves the input parameters from the quantum circuit using reflection.

- **GetWeightParameters**: 
  Retrieves the weight parameters from the quantum circuit using reflection.

- **RunEstimator**: 
  Executes the estimator to perform a forward computation using reflection to invoke the appropriate method.

- **RunGradientEstimator**: 
  Runs the gradient estimator to compute gradients, using reflection to access the method.

- **PostProcessForward**: 
  Processes the results of the forward computation to return only the relevant samples.

- **PostProcessBackward**: 
  Processes the results of the backward computation to separate and return input gradients and weight gradients.

- **GetProperty**: 
  Retrieves a property value from the specified target object using Unity reflection.

- **GetMethod**: 
  Retrieves a method from the specified target object using Unity reflection.