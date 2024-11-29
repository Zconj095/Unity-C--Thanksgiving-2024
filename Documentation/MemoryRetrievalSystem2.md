# MemoryRetrievalSystem2

## Overview
The `MemoryRetrievalSystem2` class is designed to manage the retrieval of memory items from both short-term and long-term memory systems within a Unity application. It serves as a bridge between these two memory components, allowing for efficient retrieval based on a query embedding. The main functionality of this class is to combine memory items from both systems, prioritize them based on relevance, and return the top items that best match a given query.

## Variables
- `ShortTermMemory shortTermMemory`: An instance of the `ShortTermMemory` class, responsible for managing and retrieving items stored in short-term memory.
- `LongTermMemory longTermMemory`: An instance of the `LongTermMemory` class, responsible for managing and retrieving items stored in long-term memory.

## Functions
- `MemoryRetrievalSystem2(ShortTermMemory stm, LongTermMemory ltm)`: Constructor that initializes the `MemoryRetrievalSystem2` with instances of `ShortTermMemory` and `LongTermMemory`.

- `List<MemoryItem> RetrieveMemory(float[] queryEmbedding, int topN = 5)`: 
  - Retrieves memory items from both short-term and long-term memory based on a query embedding.
  - Combines the retrieved items, computes their relevance based on a similarity score and recency, and returns the top N relevant items.

- `private float ComputeCombinedRelevance(float[] query, MemoryItem item)`:
  - Calculates the combined relevance of a memory item based on its similarity to the query embedding and its recency.
  - Uses a weighted formula where similarity contributes 80% and recency contributes 20% to the final relevance score.

- `private float ComputeSimilarity(float[] query, float[] target)`:
  - Computes the cosine similarity between the query embedding and a target memory item's embedding.
  - This function calculates the dot product and magnitudes of the two vectors to derive the similarity score. 

This class effectively integrates short-term and long-term memory retrieval processes, making it a crucial component for applications that require memory-based operations, such as AI or game development in Unity.