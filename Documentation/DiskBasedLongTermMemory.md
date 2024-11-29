# DiskBasedLongTermMemory

## Overview
The `DiskBasedLongTermMemory` class is designed to manage long-term storage of memory items on disk. It provides functionality to add and retrieve memory items, which consist of a key, content, embedding, timestamp, and relevance score. This class fits within a larger codebase that likely involves memory management or data persistence, enabling the application to store and retrieve important information even after it has been closed.

## Variables
- `storagePath`: A string that holds the file path where memory items will be stored. This path is created when an instance of the class is initialized.

## Functions
- `DiskBasedLongTermMemory(string path)`: Constructor that initializes a new instance of the `DiskBasedLongTermMemory` class. It takes a string parameter `path` which specifies the directory where memory items will be stored. The constructor also creates the directory if it does not already exist.

- `void AddItem(MemoryItem item)`: This method takes a `MemoryItem` object as a parameter and saves it to disk. It constructs a file path based on the item's key and writes the item's key, content, embedding, timestamp, and relevance score into a text file.

- `MemoryItem RetrieveItem(string key)`: This method retrieves a memory item from disk based on the provided key. It reads the corresponding text file and parses its contents into a `MemoryItem` object. If the file does not exist or does not contain the expected data, it returns `null`.