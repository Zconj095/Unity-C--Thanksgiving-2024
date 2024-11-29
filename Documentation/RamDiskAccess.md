# RamDiskAccess

## Overview
The `RamDiskAccess` script is designed to facilitate the saving and loading of vector data to and from a RAM disk. This is particularly useful in scenarios where performance is critical, as accessing data from a RAM disk is significantly faster than traditional disk storage. The script provides functions to save an array of floats as a binary file and to load that array back into memory. It integrates seamlessly into a Unity project, allowing developers to manage vector data efficiently.

## Variables
- `BasePath`: A static string that defines the path to the mounted RAM disk (in this case, "D:\\"). This is where the vector files will be saved and loaded from.

## Functions
- `SaveVectorToDisk(string filename, float[] vector)`: 
  - This static method takes a filename and an array of floats as parameters. It saves the array of floats to a binary file at the specified path on the RAM disk. If the file already exists, it will be overwritten. The method uses a `BinaryWriter` to write each float value to the file.

- `LoadVectorFromDisk(string filename)`: 
  - This static method takes a filename as a parameter and attempts to load the corresponding binary file from the RAM disk. If the file does not exist, it returns `null`. If the file is found, it reads the float values using a `BinaryReader` and returns them as an array of floats. The method reads until it reaches the end of the file.