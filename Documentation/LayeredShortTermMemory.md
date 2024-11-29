# LayeredShortTermMemory

## Overview
The `LayeredShortTermMemory` script is a Unity component that manages a layered caching system for memory items. It is designed to store and retrieve memory items across three levels of cache (L1, L2, and L3) and a RAM-like memory. This structured approach helps in efficiently managing memory usage while providing fast access to frequently used items. The script fits into a larger codebase that requires effective memory management for applications such as AI or game development where temporary data storage is crucial.

## Variables
- **l1Cache**: A queue that stores memory items in Level 1 cache, which has the highest priority for storage and retrieval.
- **l2Cache**: A queue that stores memory items in Level 2 cache, serving as a secondary storage option.
- **l3Cache**: A queue that stores memory items in Level 3 cache, providing additional storage after L1 and L2.
- **ramMemory**: A list that holds memory items when all cache levels are full, simulating RAM storage.
- **l1Capacity**: An integer that defines the maximum number of items that can be stored in Level 1 cache.
- **l2Capacity**: An integer that defines the maximum number of items that can be stored in Level 2 cache.
- **l3Capacity**: An integer that defines the maximum number of items that can be stored in Level 3 cache.
- **ramCapacityBytes**: A long integer that specifies the maximum size of the RAM-like memory in bytes.

## Functions
- **LayeredShortTermMemory(int l1Size, int l2Size, int l3Size, long ramSizeBytes)**: Constructor that initializes the cache capacities and RAM size based on the provided parameters.
  
- **AddItem(MemoryItem item)**: Adds a memory item to the appropriate cache or RAM based on the current storage state. If all caches are full, it checks if the RAM can accommodate the new item; otherwise, it outputs a message indicating that the RAM limit has been reached.

- **RetrieveItem(string key)**: Retrieves a memory item based on its key. It checks the L1, L2, and L3 caches sequentially before searching through the RAM memory.

- **long RamCapacityBytes**: A public property that exposes the total RAM capacity in bytes.

- **long GetCurrentRAMUsage()**: A public method that returns the current usage of the RAM memory by calling the private method `GetRAMUsage()`.

- **private long GetRAMUsage()**: A private method that calculates and returns the total memory usage of the items stored in the RAM by summing up the sizes of their embeddings.