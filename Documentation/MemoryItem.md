# MemoryItem

## Overview
The `MemoryItem` class is a Unity MonoBehaviour that represents an individual memory item within a larger memory management system. Each instance of `MemoryItem` stores a unique identifier, raw content, an embedding vector representation, a timestamp indicating when the item was stored, and a relevance score for prioritization. This class is likely part of a system that manages memory items for applications such as machine learning, data storage, or game development, where quick retrieval and organization of memory data are essential.

## Variables

- **Key**: `string`
  - A unique identifier for the memory item, used to distinguish it from other items.

- **Content**: `string`
  - The raw content associated with the memory item, which can be any form of text or data.

- **Embedding**: `float[]`
  - An array representing the embedding vector of the memory item, useful for machine learning applications where numerical representations of data are required.

- **Timestamp**: `DateTime`
  - The date and time when the memory item was created or stored, recorded in Coordinated Universal Time (UTC).

- **RelevanceScore**: `float`
  - A score that indicates the relevance of the memory item, with a default value set to 1.0f. This score can be used for prioritizing memory items when retrieving or processing them.

## Functions

- **MemoryItem(string key, string content, float[] embedding)**
  - Constructor for the `MemoryItem` class. It initializes a new instance of the class with the specified key, content, and embedding vector. The constructor also sets the `Timestamp` to the current UTC time and initializes the `RelevanceScore` to a default value of 1.0f.