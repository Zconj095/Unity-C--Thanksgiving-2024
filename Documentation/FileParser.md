# FileParser

## Overview
The `FileParser` class serves as an abstract base class for parsing files in a codebase. It defines a contract for derived classes that implement specific file parsing logic. The main function of this class is to provide a method for parsing files given their file paths and a default method to determine the file type, which can be overridden by subclasses. This class fits into a larger codebase by allowing different file types to be parsed through polymorphism, enabling the application to handle various file formats in a consistent manner.

## Variables
- **None**: This class does not contain any instance variables. It defines methods that must be implemented by subclasses.

## Functions

### `Parse(string filePath)`
- **Description**: This is an abstract method that must be implemented by any derived class. It takes a string parameter `filePath`, which represents the location of the file to be parsed, and returns a string representing the parsed content of the file. The specific parsing logic will depend on the file type and the implementation in the derived class.

### `GetFileType()`
- **Description**: This is a virtual method that returns a string indicating the type of file being parsed. By default, it returns "Unknown". Derived classes can override this method to provide a specific file type based on the file they are designed to parse.