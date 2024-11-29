# DimmdriveManager

## Overview
The `DimmdriveManager` class is designed to facilitate the creation and deletion of RAM disks using the `dimmdrive.exe` application. A RAM disk is a virtual disk drive that uses a portion of the computer's RAM as if it were a disk drive, allowing for faster data access. This class provides two static methods: `CreateRamDisk` and `DeleteRamDisk`, which allow users to manage RAM disks programmatically. It integrates with the broader codebase by providing a straightforward interface for RAM disk management, particularly useful in applications that require high-speed temporary storage.

## Variables
- **driveLetter**: A string representing the letter of the drive to be created or deleted (e.g., "R:").
- **sizeMB**: An integer representing the size of the RAM disk in megabytes (MB).

## Functions

### CreateRamDisk
```csharp
public static void CreateRamDisk(string driveLetter, int sizeMB)
```
This method initiates the creation of a RAM disk. It constructs a `ProcessStartInfo` object with the necessary parameters to execute `dimmdrive.exe` with the appropriate arguments for creating a disk. It waits for the process to complete and captures any output from the command line, which is then printed to the console.

### DeleteRamDisk
```csharp
public static void DeleteRamDisk(string driveLetter)
```
This method is responsible for deleting an existing RAM disk. Similar to the `CreateRamDisk` method, it sets up a `ProcessStartInfo` object to run `dimmdrive.exe` with the necessary arguments for deletion. After executing the command, it waits for the process to finish and outputs the result to the console.