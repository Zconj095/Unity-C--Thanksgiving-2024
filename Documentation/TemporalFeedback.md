# TemporalFeedback

## Overview
The `TemporalFeedback` script is designed to manage and compute feedback based on a history of states in a Unity game environment. It stores a limited number of past state vectors and provides a method to compute the average feedback from these stored states. This functionality is particularly useful in scenarios where historical data influences current behavior, such as in AI decision-making or state management systems.

## Variables
- `stateHistory`: A queue that holds arrays of floats, representing the history of state vectors. This allows for efficient storage and retrieval of past states.
- `memoryCapacity`: An integer that defines the maximum number of past states the `stateHistory` can hold. When the limit is reached, the oldest state is removed to make room for a new one.
- `vectorSize`: An integer that specifies the size of each state vector stored in the `stateHistory`. This determines how many elements each state will contain.

## Functions
- `Start()`: This is a Unity lifecycle method that initializes the `stateHistory` queue when the script is first run. It ensures that the queue is ready to store state vectors.

- `StoreState(float[] state)`: This method takes a state vector as an input, checks if the `stateHistory` has reached its `memoryCapacity`, and if so, removes the oldest state. It then clones the input state and adds it to the queue, ensuring that the original state remains unaltered.

- `ComputeTemporalFeedback()`: This method calculates the average feedback from all stored states in the `stateHistory`. It iterates through each state, summing the corresponding elements across all states, and then normalizes the result by dividing each summed element by the number of stored states. This method returns an array of floats representing the computed feedback.