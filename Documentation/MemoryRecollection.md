# MemoryRecollection

## Overview
The `MemoryRecollection` class is designed to manage memory items by utilizing both short-term and long-term memory systems. It facilitates the addition, retrieval, and recollection of memory items based on their relevance and recency. This class is integral to the codebase as it connects the short-term and long-term memory components, allowing for efficient memory management and retrieval based on user queries.

## Variables
- `ShortTermMemory stm`: An instance of the `ShortTermMemory` class that holds recently added memory items.
- `LongTermMemory ltm`: An instance of the `LongTermMemory` class that stores memory items that are less frequently accessed but still important.
  
## Functions
- `MemoryRecollection(ShortTermMemory shortTermMemory, LongTermMemory longTermMemory)`: Constructor that initializes the `MemoryRecollection` instance with short-term and long-term memory objects. It throws an `ArgumentNullException` if either memory object is null.

- `void AddToMemory(MemoryItem memoryItem)`: Adds a `MemoryItem` to either short-term or long-term memory based on its `RelevanceScore`. If the score is 0.7 or higher, the item is added to short-term memory; otherwise, it is stored in long-term memory. Throws an `ArgumentNullException` if the `memoryItem` is null.

- `MemoryItem GetMemoryById(string key)`: Retrieves a `MemoryItem` using its unique key. It first checks the short-term memory, and if not found, it checks the long-term memory. Throws an `ArgumentException` if the key is null or empty.

- `List<MemoryItem> Recollect(float[] queryEmbedding, int topN = 5)`: Recollects memory items based on a query embedding. It retrieves items from both short-term and long-term memory, combines them, and returns the top N items ranked by relevance. Throws an `ArgumentException` if the `queryEmbedding` is null or empty.

- `private float ComputeRelevance(float[] query, MemoryItem item)`: Computes the relevance of a memory item based on the similarity of its embedding to a query embedding and its recency. It combines similarity and recency factors to produce a relevance score.

- `private float ComputeSimilarity(float[] query, float[] target)`: Calculates the cosine similarity between two embeddings (query and target). It returns a value between 0 and 1, where 1 indicates perfect similarity.

- `private float ComputeRecencyFactor(DateTime timestamp)`: Computes a recency factor for a memory item based on the time elapsed since it was last updated. The factor decreases over time, providing a decay effect to less recent items.