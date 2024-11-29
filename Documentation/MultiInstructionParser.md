# MultiInstructionParser

## Overview
The `MultiInstructionParser` class is designed to parse a string of instructions into structured sequences. It takes a single input string and separates it into manageable lists of instructions based on logical connectors such as "then", "next", and "and". This functionality is essential for processing complex command strings in a way that allows the rest of the codebase to handle each instruction independently and in sequence.

## Variables
- **input**: A string containing the raw instructions that need to be parsed into sequences.
- **rawSequences**: An array of strings that holds the initial split of the input based on logical connectors.
- **instructionSequences**: A list of lists, where each inner list contains a set of parsed instructions derived from the input.
- **sequence**: A temporary variable used to iterate through each element in `rawSequences`.
- **instructions**: A list of strings that contains individual instructions split from each sequence based on punctuation.

## Functions
- **ParseInstructionSequences(string input)**: 
  - This is the main function of the class. It takes a string input, splits it into sequences using regular expressions based on logical connectors, and further splits those sequences into individual instructions based on punctuation marks. It then cleans up the resulting lists by removing any empty strings and returns a list of instruction sequences.