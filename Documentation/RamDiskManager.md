# RamDiskManager

## Overview
The `RamDiskManager` class is responsible for managing virtual RAM disks within a Unity application. It allows the creation, deletion, and retrieval of RAM disks, which can be used to temporarily store data in memory for fast access. This class acts as a central point for handling multiple RAM disks, ensuring that each disk is uniquely identified by a name and that memory management is conducted properly.

## Variables
- `ramDisks`: A `Dictionary<string, VirtualRamDisk>` that stores the virtual RAM disks. Each disk is associated with a unique string key (the disk's name), allowing for easy access and management.

## Functions
- **RamDiskManager()**: Constructor that initializes the `ramDisks` dictionary when an instance of `RamDiskManager` is created.

- **CreateRamDisk(string name, long sizeBytes)**: This function creates a new virtual RAM disk with the specified name and size (in bytes). If a RAM disk with the same name already exists, it throws an exception. The newly created RAM disk is added to the `ramDisks` dictionary and returned.

- **DeleteRamDisk(string name)**: This function deletes a RAM disk identified by its name. If the specified RAM disk exists, it calls the `Dispose()` method on that disk to free up resources and then removes it from the `ramDisks` dictionary.

- **GetRamDisk(string name)**: This function retrieves a RAM disk by its name. It checks if the RAM disk exists in the `ramDisks` dictionary and returns it if found; otherwise, it returns `null`.