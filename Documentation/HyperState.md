# HyperState

## Overview
The `HyperState` class represents a mathematical construct that operates in a multi-dimensional space, encapsulated in a vector. It provides functionality to initialize a vector with random values, perform element-wise multiplication (binding) with another `HyperState`, and perform element-wise addition (superposition) with another `HyperState`. This class can be useful in applications such as simulations, neural networks, or any system that requires manipulation of multi-dimensional data.

## Variables

- `Vector`: A `float[]` array that holds the multi-dimensional vector values. It is initialized with a specified number of dimensions and can be populated with random values or set to a specific vector.

## Functions

- `HyperState(int dimensions)`: Constructor that initializes the `Vector` with the specified number of dimensions and calls the `Randomize` method to populate the vector with random values between -1 and 1.

- `HyperState(float[] vector)`: Constructor that initializes the `Vector` with a provided array of floats, allowing the creation of a `HyperState` from an existing vector.

- `void Randomize()`: Populates the `Vector` with random float values between -1 and 1 using a random number generator.

- `HyperState Bind(HyperState other)`: Takes another `HyperState` object as input and returns a new `HyperState` that is the result of element-wise multiplication of the current vector and the vector of the provided `HyperState`.

- `HyperState Superpose(HyperState other)`: Takes another `HyperState` object as input and returns a new `HyperState` that is the result of element-wise addition of the current vector and the vector of the provided `HyperState`.