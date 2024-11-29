# InstructionSetLoader

## Overview
The `InstructionSetLoader` class is responsible for loading an instruction set from a specified file. It reads the file, extracts the name of the instruction set and its individual instructions, and then returns an `InstructionSet` object containing this data. This functionality is crucial for initializing instruction sets that may be used in various parts of the codebase, allowing for dynamic loading of instructions based on external files.

## Variables
- **filePath**: A string representing the path to the instruction set file that is to be loaded.
- **name**: A string that stores the name of the instruction set extracted from the file.
- **instructions**: A list of strings that contains all the instructions parsed from the file.

## Functions
- **LoadInstructionSet(string filePath)**: 
  - This static method takes a file path as input and attempts to load an instruction set from the specified file. 
  - It checks if the file exists and throws a `FileNotFoundException` if it does not.
  - It reads the file line by line, extracting the instruction set name and any instructions found, while ignoring any lines related to instruction counts.
  - Finally, it returns a new instance of `InstructionSet`, initialized with the extracted name and instructions.