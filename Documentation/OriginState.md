# OriginState

## Overview
The `OriginState` class is a Unity MonoBehaviour that represents a state in a multi-dimensional space. It maintains a position in a four-dimensional array (x, y, z, t) and an associated state vector, which can be optimized based on feedback. This class is likely used in a larger system that requires tracking and updating states, such as in simulations or game mechanics. By encapsulating the position and state vector, it facilitates the management of state information and provides methods for updating and comparing states.

## Variables
- `Position` (float[]): A private property that holds the current position of the state in a four-dimensional space (x, y, z, t).
- `StateVector` (MultiStateVector): A private property that stores a vector representing the state, initialized with a specified number of dimensions.

## Functions
- `OriginState(float[] position, int vectorDimensions)`: Constructor that initializes a new instance of the `OriginState` class with a specified position and the number of dimensions for the state vector.

- `void UpdateOrigin(float[] newPosition, float[] feedback, float learningRate)`: Updates the current position of the origin state to a new position and optimizes the state vector using provided feedback and a learning rate.

- `float ComputeDistance(OriginState other)`: Calculates the Euclidean distance between the current `OriginState` instance and another `OriginState` instance. It sums the squared differences of their positions and returns the square root of that sum, providing a measure of how far apart the two states are in the four-dimensional space.