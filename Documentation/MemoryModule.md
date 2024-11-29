# MemoryModule

## Overview
The `MemoryModule` script is designed to manage memory storage within a Unity application. It maintains two types of memory: short-term memory, which temporarily holds recent information, and long-term memory, which stores information persistently based on unique keys. This module can be utilized in various applications, such as games or simulations, where tracking and recalling information is crucial for gameplay mechanics or user interactions.

## Variables

- `shortTermMemory`: A `List<string>` that stores recent memories (up to 10 items). This represents the short-term memory of the module.
  
- `longTermMemory`: A `Dictionary<string, string>` that maps unique keys to their corresponding values, representing the long-term memory of the module.

## Functions

- `Remember(string key, string value)`: 
  - This function is responsible for adding a new memory to the module. It first checks if the short-term memory has reached its limit of 10 items; if so, it removes the oldest item. It then adds the new value to the short-term memory. Additionally, if the provided key does not already exist in the long-term memory, it stores the key-value pair in the long-term memory.

- `Recall(string key)`: 
  - This function retrieves a value from long-term memory based on the provided key. If the key exists, it returns the associated value; if not, it returns the message "No memory found." This allows for easy access to previously stored information.