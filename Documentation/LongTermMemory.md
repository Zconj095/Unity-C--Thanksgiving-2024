# LongTermMemory

## Overview
The `LongTermMemory` class is designed to manage a collection of memory items within a Unity application. It provides functionality to store memory items, retrieve specific items by their keys, and fetch the most relevant memory items based on a query embedding. This class acts as a central repository for memory items, allowing other parts of the codebase to interact with and utilize stored memories effectively.

## Variables
- **memoryStorage**: A dictionary that maps string keys to `MemoryItem` objects. This variable stores all memory items, allowing for quick access and retrieval based on their unique keys.

## Functions
- **LongTermMemory()**: Constructor that initializes the `memoryStorage` dictionary to hold memory items.

- **void StoreItem(MemoryItem item)**: 
  - Stores a new memory item in the `memoryStorage`. If an item with the same key already exists, it skips adding the new item. 
  - Throws an `ArgumentNullException` if the provided item is null.

- **MemoryItem RetrieveItem(string key)**: 
  - Retrieves a specific memory item from the `memoryStorage` using the provided key. 
  - Throws an `ArgumentException` if the key is null or empty. Returns the memory item if found, or null if not.

- **List<MemoryItem> RetrieveRelevantItems(float[] queryEmbedding, int topN = 5)**: 
  - Retrieves the top N most relevant memory items based on a query embedding. It ranks items using a similarity computation.
  - Throws an `ArgumentException` if the query embedding is null or empty. Returns a list of the most relevant memory items.

- **private float ComputeSimilarity(float[] query, float[] target)**: 
  - Computes the cosine similarity between two embedding vectors (query and target). 
  - Throws an `ArgumentException` if the lengths of the two embedding vectors do not match. Returns a float representing the similarity score, where 1 indicates perfect similarity and 0 indicates no similarity.