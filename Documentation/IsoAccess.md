# IsoAccess

## Overview
The `IsoAccess` class provides functionality to read files from an ISO file system. The main function, `ReadFileFromISO`, takes a file path as input, constructs the full path to the file on the ISO, and attempts to read its content. If the file exists, it logs the content to the Unity console; if not, it logs a message indicating that the file was not found. This class is useful for accessing and retrieving data stored within an ISO file, which can be important for various applications in game development or data management within Unity.

## Variables
- **isoPath**: A string variable that constructs the full path to the file on the ISO based on the provided `filePath`. It is prefixed with the drive letter `Z:` which is assumed to be the location of the mounted ISO.

## Functions
- **ReadFileFromISO(string filePath)**: 
  - This static method accepts a string parameter `filePath`, which represents the relative path of the file to be read from the ISO. 
  - It checks if the file exists at the constructed `isoPath`. 
  - If the file exists, it reads the content of the file and logs it to the Unity console. 
  - If the file does not exist, it logs a message indicating that the file was not found on the ISO.