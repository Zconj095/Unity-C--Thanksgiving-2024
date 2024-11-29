# MemoryRecallSystem

## Overview
The `MemoryRecallSystem` script is designed to manage the encoding and recalling of memory sequences using a Long Short-Term Memory (LSTM) neural network. This component is intended to be used within a Unity environment, allowing for the interactive recall of stored memories based on user input. It integrates with the rest of the codebase by providing functionality to encode sequences into memory and to recall the closest matching memory based on a query input. The script also visualizes the recalled memory in the Unity scene using a sphere.

## Variables
- `lstm`: An instance of the `MEMORYRECALLLSTM` class, which is responsible for processing input sequences and managing the memory states.
- `memory`: A list that stores the encoded memory sequences as arrays of floats.
- `previousChange`: A float variable that stores the last computed distance change between the current and previous memory recall states.
- `recallSphere`: A GameObject used to visualize the recalled memory in the Unity scene.

## Functions
- `Start()`: Initializes the LSTM with specified input and hidden sizes, and encodes example sequences into memory upon the start of the game.
  
- `Update()`: Checks for user input (specifically the 'R' key) to trigger the memory recall process. It recalls memory based on a predefined query and logs the result.

- `EncodeSequence(float[] sequence)`: Takes a float array as input, processes it through the LSTM, and adds the resulting encoded memory to the `memory` list.

- `RecallMemory(float[] query)`: Computes the recalled memory based on the provided query. It compares the query's state with the stored memory sequences and returns the closest match based on distance.

- `ComputeDistance(float[] a, float[] b)`: Calculates the Euclidean distance between two float arrays, `a` and `b`. This distance metric is used to determine how similar or different memories are.

- `RecallMemory2(float[] query)`: Similar to `RecallMemory`, but also logs the change in distance between the current and previous memory states. It updates the visualization of the recalled memory.

- `UpdateVisualization(float[] recalledMemory)`: Creates or updates a visual representation (sphere) of the recalled memory in the Unity scene. The sphere's position and color are determined by the values in the recalled memory array.