# ShortTermMemory

## Overview
The `ShortTermMemory` class is designed to manage a collection of memory items in a way that mimics human short-term memory. It allows for the addition of new memory items while maintaining a specified capacity. When the capacity is exceeded, the oldest memory item is removed to make space for the new item. This class provides efficient access to memory items by utilizing both a queue and a dictionary for storage and retrieval. It plays a crucial role in scenarios where temporary data storage and quick access to recent items are necessary, such as in game development or AI applications.

## Variables

- `memoryQueue`: A `Queue<MemoryItem>` that holds the memory items in the order they were added. It allows for efficient addition and removal of items, specifically the oldest item when capacity is exceeded.
  
- `memoryLookup`: A `Dictionary<string, MemoryItem>` that maps unique keys to their corresponding memory items. This allows for quick retrieval of memory items without needing to search through the queue.
  
- `capacity`: An `int` that defines the maximum number of memory items that can be stored in the short-term memory. Once this limit is reached, the oldest item will be removed to accommodate new entries.

## Functions

- `ShortTermMemory(int capacity)`: Constructor that initializes a new instance of the `ShortTermMemory` class with a specified capacity. It sets up the internal queue and lookup dictionary.

- `void AddItem(MemoryItem item)`: Adds a new memory item to the short-term memory. If the item already exists, it updates the existing entry. If adding the new item exceeds the capacity, it removes the oldest memory item.

- `List<MemoryItem> RetrieveItems(int count)`: Retrieves a list of the most recent memory items, up to the specified count. The items are returned in reverse order, showing the most recently added items first.

- `MemoryItem RetrieveItem(string key)`: Retrieves a specific memory item based on its unique key. If the key is invalid (null or empty), an exception is thrown. If the key exists, the corresponding memory item is returned; otherwise, null is returned.

- `void Clear()`: Clears all memory items from the short-term memory, removing all entries from both the queue and the lookup dictionary. This effectively resets the memory state.