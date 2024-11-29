# HyperstateOptimizer

## Overview
The `HyperstateOptimizer` class is designed to optimize and normalize state vectors in a Unity environment. It provides two main functions: `RefineState` which adjusts a given state based on feedback using a specified learning rate, and `NormalizeState` which converts a state vector into a unit vector. This class is useful in scenarios where state adjustments are necessary based on external feedback, such as in reinforcement learning or game AI.

## Variables
- `optimizedState`: An array of floats that holds the adjusted state values after applying feedback and the learning rate in the `RefineState` method.
- `magnitude`: A float that represents the computed magnitude of the input state vector, used to normalize the state in the `NormalizeState` method.

## Functions
### RefineState
```csharp
public static float[] RefineState(float[] state, float[] feedback, float learningRate)
```
- **Description**: This function takes an input state vector and adjusts it based on the provided feedback vector and a learning rate. It iterates through each element of the state, applying the formula `state[i] + learningRate * feedback[i]` to produce an optimized state. The result is returned as a new float array.

### NormalizeState
```csharp
public static float[] NormalizeState(float[] state)
```
- **Description**: This function normalizes the input state vector to create a unit vector. It calculates the magnitude of the state vector and then divides each element of the state by this magnitude. The resulting normalized vector is returned as a new float array.