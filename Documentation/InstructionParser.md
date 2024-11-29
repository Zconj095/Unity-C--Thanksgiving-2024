# InstructionParser

## Overview
The `InstructionParser` class is designed to process and parse a string of input instructions into a structured list of individual instructions. This is particularly useful in scenarios where instructions are provided in a natural language format, allowing for easier handling and execution of each instruction in the context of a Unity game or application. The main function of the script, `ParseInstructions`, takes a single input string and splits it into discrete instructions based on punctuation and specific connectors.

## Variables
- **input**: A `string` that represents the raw input containing multiple instructions. This is the data that the `ParseInstructions` function will process.
- **rawInstructions**: An array of `string` that holds the split instructions derived from the input string. It uses regular expressions to determine where to split the input.
- **instructions**: A `List<string>` that stores the final parsed instructions after trimming whitespace and filtering out any empty entries.

## Functions
- **ParseInstructions(string input)**: This public method takes a single string argument (`input`) and returns a `List<string>`. It uses regular expressions to split the input into individual instructions based on punctuation marks (like periods, exclamation points, and question marks) and specific phrases ("and then", ", then"). The method then trims any excess whitespace from each instruction and filters out any empty strings before returning the cleaned list of instructions.