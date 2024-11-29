# CodeParser

## Overview
The `CodeParser` class is designed to read and parse the content of files, specifically focusing on programming code files. It extends the functionality of the `FileParser` class, providing methods to read the contents of a file and determine the file type based on its extension. This class plays a crucial role in any code analysis or processing system within the codebase by enabling the retrieval of file contents and identifying the programming language used in the file.

## Variables
- `lastFilePath`: A private string variable that stores the path of the last parsed file. This variable is used to keep track of which file was last processed, allowing the class to determine the file type based on its extension.

## Functions
- `Parse(string filePath)`: 
  - **Description**: This method takes a file path as an argument, reads the contents of the file located at that path, and returns the file's content as a string. It also updates the `lastFilePath` variable with the current file path for future reference.

- `GetFileType()`: 
  - **Description**: This method checks the `lastFilePath` variable to determine the file type based on its extension. If `lastFilePath` is null or empty, it returns "Unknown File Type." Otherwise, it retrieves the file extension and returns a corresponding string indicating the programming language of the file (e.g., "C# Code," "Python Code," etc.). If the extension does not match any known programming languages, it returns "Unknown Code."