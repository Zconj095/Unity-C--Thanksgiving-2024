# HardwareInfo

## Overview
The `HardwareInfo` class provides functionality to retrieve the cache sizes (L1, L2, and L3) of the CPU for different operating systems: Windows, Linux, and macOS. This class abstracts the underlying implementation details for each platform, allowing users to easily obtain cache size information without needing to handle platform-specific code. This functionality can be useful in performance tuning, system diagnostics, and resource management within the broader codebase.

## Variables
- **None**: This class does not define any public or private variables. All data is managed through method parameters and return values.

## Functions

### `GetL1CacheSize()`
- **Description**: Retrieves the size of the Level 1 (L1) cache for the CPU. It determines the appropriate method to call based on the operating system.

### `GetL2CacheSize()`
- **Description**: Retrieves the size of the Level 2 (L2) cache for the CPU. Similar to `GetL1CacheSize`, it selects the correct implementation based on the operating system.

### `GetL3CacheSize()`
- **Description**: Retrieves the size of the Level 3 (L3) cache for the CPU. It uses the same approach as the previous two methods to determine the correct platform-specific method.

### `GetWindowsCacheSize(int cacheLevel)`
- **Description**: A private method that retrieves the cache size for a specified level (L1, L2, or L3) on Windows by calling the native API to get processor information.

### `GetLinuxCacheSize(int cacheLevel)`
- **Description**: A private method that retrieves the cache size for a specified level (L1, L2, or L3) on Linux by reading the cache size information from the filesystem.

### `GetMacOSCacheSize(string sysctlKey)`
- **Description**: A private method that retrieves the cache size for a specified level (L1, L2, or L3) on macOS by executing a `sysctl` command and parsing the output.

### `ExecuteBashCommand(string command)`
- **Description**: A private method that executes a given bash command and returns the output as a string. It sets up the process start information and handles the execution of the command.

### `GetProcessorInformation()`
- **Description**: A private method that retrieves logical processor information using the Windows API. It allocates memory for the data, populates it, and returns an array of `SYSTEM_LOGICAL_PROCESSOR_INFORMATION_EX` structures.

## Enums and Structs

### `LOGICAL_PROCESSOR_RELATIONSHIP`
- **Description**: An enumeration that defines the types of relationships for logical processors, including `RelationCache` and `RelationAll`.

### `SYSTEM_LOGICAL_PROCESSOR_INFORMATION_EX`
- **Description**: A struct that represents detailed information about a logical processor, including its relationship type, size, and cache descriptor.

### `CACHE_DESCRIPTOR`
- **Description**: A struct that describes the cache level and size in bytes for a logical processor.

This documentation aims to provide clarity on the purpose and usage of the `HardwareInfo` class, making it easier for developers to understand and utilize its functionality within the codebase.