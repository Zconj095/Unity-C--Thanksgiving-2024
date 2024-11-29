# FunctionBuilder

## Overview
The `FunctionBuilder` class is responsible for dynamically adding functions to a `FunctionRegistry` based on provided instructions. It interprets specific phrases within the instruction string to determine which mathematical functions to register, such as addition and factorial calculation. This class fits within a larger codebase that likely involves dynamic function handling, allowing for extensible and customizable functionality in a Unity environment.

## Variables
- **instruction**: A string that describes the function to be added. It contains specific phrases that dictate which function to register.
- **registry**: An instance of `FunctionRegistry` where the new functions will be registered. This acts as a central repository for the functions that can be called later.

## Functions
- **AddFunctionFromInstruction(string instruction, FunctionRegistry registry)**: 
  - This static method checks the provided instruction string for specific phrases and registers corresponding functions in the `FunctionRegistry`.
  - If the instruction contains "adds two numbers", it registers an "add" function that takes two arguments and returns their sum.
  - If the instruction contains "calculates factorial", it registers a "factorial" function that takes one argument and returns the factorial of that number.
  - Both registered functions include error handling to ensure the correct number of arguments is provided before executing their respective operations.