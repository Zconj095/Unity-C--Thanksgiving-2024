# IsoGenerator

## Overview
The `IsoGenerator` script is designed to create an ISO file from a specified source directory using the `mkisofs` command-line utility. This functionality is particularly useful in scenarios where developers need to package files into an ISO format for distribution or archival purposes. The `GenerateISO` method serves as the main function of the script, taking in the paths of the source directory and the desired output ISO file, and executing the necessary command to generate the ISO.

## Variables

- **sourcePath**: A string that represents the path to the directory containing the files to be included in the ISO.
- **isoPath**: A string that indicates the path where the generated ISO file will be saved.

## Functions

### GenerateISO(string sourcePath, string isoPath)
This static method is responsible for generating the ISO file. It takes two parameters:
- `sourcePath`: The directory containing the files to be packaged into the ISO.
- `isoPath`: The file path where the resulting ISO will be saved.

The method creates a `ProcessStartInfo` object to configure the execution of the `mkisofs.exe` command, setting the necessary arguments for ISO creation. It then starts the process, waits for it to complete, and logs any output from the command to the Unity console for debugging purposes.