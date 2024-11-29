# VirtualRamDisk

## Overview
The `VirtualRamDisk` class simulates a RAM disk, allowing for the storage of data in memory as if it were a physical disk. This class is designed to provide an in-memory storage solution where developers can write, read, and delete data using key-value pairs. It is particularly useful for testing and scenarios where quick access to data without the overhead of disk I/O is needed. The class manages its storage capacity and ensures that the data does not exceed the specified size limit.

## Variables
- `Name`: A string representing the name of the virtual RAM disk. It is set during construction and cannot be modified afterward.
- `SizeBytes`: A long integer that defines the total size of the RAM disk in bytes. This value is also set during construction and remains constant.
- `storage`: A private dictionary that emulates file storage, mapping string keys to byte arrays. This holds the actual data stored in the RAM disk.

## Functions
- `VirtualRamDisk(string name, long sizeBytes)`: Constructor that initializes a new instance of the `VirtualRamDisk` class with a specified name and size in bytes. It also initializes the storage dictionary.

- `void Write(string key, byte[] data)`: Writes data to the RAM disk using a specified key. It checks if there is enough space before writing; if not, it throws an exception.

- `byte[] Read(string key)`: Retrieves data associated with a specified key from the RAM disk. If the key does not exist, it returns `null`.

- `void Delete(string key)`: Removes the data associated with a specified key from the RAM disk if it exists.

- `long GetUsedSpace()`: Calculates and returns the total amount of space currently used in the RAM disk by summing the lengths of all stored byte arrays.

- `void Dispose()`: Clears the storage dictionary, releasing all resources used by the `VirtualRamDisk` instance. This is part of the `IDisposable` interface implementation, ensuring proper resource management.