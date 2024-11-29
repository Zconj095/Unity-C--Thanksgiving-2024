# UnifiedMemorySystem

## Overview
The `UnifiedMemorySystem` class is designed to manage memory retrieval and storage for an application using both short-term and long-term memory systems. It acts as an interface that allows for seamless access to memory items, prioritizing short-term memory (STM) for quick access while providing a fallback to long-term memory (LTM) when necessary. This class integrates the two memory systems, ensuring efficient memory management and retrieval for the overall codebase.

## Variables
- **shortTermMemory**: An instance of `LayeredShortTermMemory` that handles the short-term storage of memory items. It is used for quick retrieval and storage of frequently accessed data.
- **longTermMemory**: An instance of `DiskBasedLongTermMemory` that manages long-term storage of memory items. It serves as a backup for items that exceed the capacity of short-term memory.

## Functions
- **UnifiedMemorySystem(LayeredShortTermMemory stm, DiskBasedLongTermMemory ltm)**: Constructor that initializes the `UnifiedMemorySystem` with instances of short-term and long-term memory systems.

- **MemoryItem RetrieveMemory(string key)**: This function retrieves a memory item based on the provided key. It first attempts to retrieve the item from the short-term memory. If the item is not found there, it falls back to the long-term memory to find the item.

- **void AddMemory(MemoryItem item)**: This function adds a new memory item to the short-term memory. If adding the item causes the current RAM usage to exceed the defined capacity of the short-term memory, the item is also backed up to the long-term memory for persistence.