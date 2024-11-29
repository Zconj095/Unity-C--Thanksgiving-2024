# FileParserManager

## Overview
The `FileParserManager` class is designed to handle the parsing of various file formats in a Unity application. It serves as a central manager that registers different file parsers for supported formats and provides methods to parse files and retrieve their types. This class fits within a larger codebase that likely involves file handling and processing, allowing for extensibility and modularity in dealing with different file types.

## Variables
- `parsers`: A `Dictionary<string, FileParser>` that holds the mappings between file extensions (as keys) and their corresponding `FileParser` instances (as values). This dictionary enables quick look-up and delegation to the appropriate parser based on the file extension.

## Functions
- `FileParserManager()`: Constructor that initializes the `parsers` dictionary with instances of various `FileParser` subclasses for different file formats, including PDF, TXT, DOCX, ODT, RTF, and code files (C# and Python).

- `string ParseFile(string filePath)`: This method takes a file path as input, determines the file extension, and checks if a parser exists for that extension in the `parsers` dictionary. If a suitable parser is found, it calls the `Parse` method of the corresponding parser and returns the parsed content. If no parser is found for the provided file extension, it throws a `NotSupportedException`.

- `string GetFileType(string filePath)`: This method also takes a file path as input and retrieves the file extension. It checks the `parsers` dictionary to see if a parser exists for that extension. If found, it calls the `GetFileType` method of the corresponding parser to return the type of the file. If no parser is found, it returns "Unknown".