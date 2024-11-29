# LLMHebbianNetwork

## Overview
The `LLMHebbianNetwork` script implements a simple Hebbian network model in Unity. This model is based on the Hebbian learning principle, which states that the connections between neurons strengthen when they are activated together. In this script, connections between memories (represented by unique IDs) can be strengthened, and the network can retrieve memories that are associated with a given memory ID based on the strength of their connections. This functionality can be useful in various applications such as neural network simulations or memory retrieval systems.

## Variables
- `weights`: A dictionary that stores the strength of connections between pairs of memory IDs. The key is a tuple of two integers representing the memory IDs, and the value is a float representing the strength of the connection.

## Functions
- `LLMHebbianNetwork()`: Constructor that initializes the `weights` dictionary to store connections between memory IDs.

- `void StrengthenConnection(int memoryId1, int memoryId2)`: This function strengthens the connection between two memory IDs. It checks if a connection already exists and either increments the weight by 0.1 or initializes it to 0.1 if it is a new connection.

- `List<int> GetAssociatedMemories(int memoryId)`: This function retrieves a list of memory IDs that are associated with the given `memoryId`. It filters the connections to find those that include the specified memory ID, sorts them by the strength of their connections in descending order, and returns the associated memory IDs.