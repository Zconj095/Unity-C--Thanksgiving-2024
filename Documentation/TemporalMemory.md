# TemporalMemory

## Overview
The `TemporalMemory` script is a Unity component that manages a memory system for storing and retrieving past states represented as arrays of floats. It allows for a defined capacity of states to be stored, enabling the retrieval of the state that is most similar to a given query state. This functionality is useful in scenarios where tracking and comparing different states over time is necessary, such as in AI behavior, game state management, or simulation environments.

## Variables
- `public int memoryCapacity`: This variable defines the maximum number of past states that can be stored in memory. Once this limit is reached, the oldest state will be removed to make room for new ones.
  
- `private Queue<float[]> memoryQueue`: This is a queue that holds the past states. It uses a first-in-first-out (FIFO) approach to manage the states, ensuring that the oldest states are removed when new ones are added beyond the capacity.

- `public int vectorSize`: This variable specifies the size of the state vector, which determines how many elements each state array will contain.

## Functions
- `void Start()`: This function is called when the script instance is being loaded. It initializes the `memoryQueue` to ensure it is ready to store states.

- `public void StoreState(float[] state)`: This function takes an array of floats representing the current state and stores it in the memory queue. If the queue has reached its capacity, it removes the oldest state before adding the new one. It stores a clone of the state to avoid unintended modifications.

- `public float[] RetrieveStateClosestTo(float[] queryState)`: This function retrieves the state from memory that is most similar to the provided `queryState`. It calculates the distance between each stored state and the query state, returning the one with the smallest distance.

- `private float ComputeDistance(float[] state1, float[] state2)`: This private function computes the Euclidean distance between two state arrays. It sums the squared differences of their corresponding elements and returns the square root of that sum.

- `public List<float[]> GetAllStates()`: This function returns a list of all the states currently stored in memory, allowing access to the complete history of stored states.