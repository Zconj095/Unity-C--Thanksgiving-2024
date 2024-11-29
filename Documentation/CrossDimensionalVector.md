# CrossDimensionalVector

## Overview
The `CrossDimensionalVector` class is a Unity MonoBehaviour that represents a mathematical construct consisting of two vectors: a hyperdimensional vector (`HyperVector`) and a quantum state vector (`QuantumVector`). This class is designed to facilitate operations involving these vectors, particularly through a method that allows for cross-referencing them using a user-defined correlation function. The vectors are initialized with random values upon instantiation, making this class useful for simulations or calculations that require multidimensional data.

## Variables

- `HyperVector`: An array of floats representing a hyperdimensional vector. This vector is initialized with random values ranging from -1 to 1.
  
- `QuantumVector`: An array of floats representing a quantum state vector. This vector is initialized with random values ranging from 0 to 1.

## Functions

- `CrossDimensionalVector(int dimensions)`: Constructor that initializes the `HyperVector` and `QuantumVector` arrays with the specified number of dimensions. It also calls the `InitializeVectors` method to populate these arrays with random values.

- `InitializeVectors()`: A private method that populates the `HyperVector` and `QuantumVector` with random values. The `HyperVector` values are generated in the range of [-1, 1], while the `QuantumVector` values are generated in the range of [0, 1].

- `CrossReference(Func<float, float, float> correlationFunction)`: This public method takes a correlation function as an argument and applies it to each pair of corresponding elements from the `HyperVector` and `QuantumVector`. It returns an array of floats containing the results of the correlation function applied to each pair.