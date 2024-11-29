# QNNCircuit

## Overview
The `QNNCircuit` class implements a quantum neural network circuit that allows for the composition of a feature map and an ansatz function. It is designed to work with a specified number of qubits, which can be dynamically adjusted. The main function of this class is to evaluate inputs by transforming them through the feature map and ansatz functions, effectively creating a quantum circuit that can process quantum data.

This class fits within a larger codebase related to quantum computing or quantum machine learning, where it serves as a foundational building block for constructing and evaluating quantum circuits.

## Variables

- `numQubits`: An integer representing the number of qubits in the circuit. It is initialized through the constructor and can be updated using the `UpdateNumQubits` method.
  
- `featureMap`: A function that takes an array of floats as input and returns an array of floats. It is responsible for transforming the input data before it is processed by the ansatz. If not provided, it defaults to the `DefaultFeatureMap` method.

- `ansatz`: A function that takes an array of floats as input and returns an array of floats. It represents the quantum operation applied to the data after the feature map transformation. If not provided, it defaults to the `DefaultAnsatz` method.

- `circuit`: A function that combines the feature map and ansatz into a single operation. It takes an array of floats as input and returns the processed output using the composed circuit.

## Functions

- **Constructor: `QNNCircuit(int? numQubits = null, Func<float[], float[]> featureMap = null, Func<float[], float[]> ansatz = null)`**
  - Initializes a new instance of the `QNNCircuit` class. It derives the number of qubits, assigns the feature map and ansatz functions (using defaults if necessary), and composes them into a circuit.

- **`private void ComposeCircuit()`**
  - Composes the feature map and ansatz into a single circuit function that can be called with an input array.

- **`public void UpdateNumQubits(int numQubits)`**
  - Updates the number of qubits in the circuit. It ensures the new number is a positive integer and adjusts the feature map and ansatz accordingly.

- **`public void UpdateFeatureMap(Func<float[], float[]> newFeatureMap)`**
  - Updates the feature map function used in the circuit. It ensures the new feature map is not null and re-composes the circuit.

- **`public void UpdateAnsatz(Func<float[], float[]> newAnsatz)`**
  - Updates the ansatz function used in the circuit. It ensures the new ansatz is not null and re-composes the circuit.

- **`public float[] Evaluate(float[] input)`**
  - Evaluates an input array by passing it through the composed circuit. It checks that the input size matches the number of qubits.

- **`public int GetNumQubits()`**
  - Returns the current number of qubits in the circuit.

- **`private float[] DefaultFeatureMap(float[] input)`**
  - A default implementation of the feature map that simply returns the input array unchanged (identity mapping).

- **`private float[] DefaultAnsatz(float[] input)`**
  - A default implementation of the ansatz that applies a sine transformation to each element of the input array.

- **`public int NumInputParameters`**
  - A property that returns the number of input parameters, which is equivalent to the number of qubits.

- **`public int NumWeightParameters`**
  - A property that returns the number of weight parameters, which is also equivalent to the number of qubits.