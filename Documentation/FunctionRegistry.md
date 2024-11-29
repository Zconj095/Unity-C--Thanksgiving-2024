# FunctionRegistry

## Overview
The `FunctionRegistry` class serves as a dynamic registry for functions within a Unity application. It allows developers to register functions by name and execute them later with specified arguments. This script is useful for creating a flexible system where functions can be added or modified at runtime, enabling greater modularity and extensibility in the codebase.

## Variables

- `functions`: A private dictionary that maps function names (as strings) to their corresponding delegate types (`Func<object[], object>`). This dictionary is used to store all registered functions and allows for their retrieval and execution.

## Functions

- **FunctionRegistry()**: 
  - Constructor that initializes the `functions` dictionary. It sets up an empty registry for storing functions when an instance of the `FunctionRegistry` class is created.

- **RegisterFunction(string name, Func<object[], object> function)**: 
  - This method takes a function name and a function delegate as parameters. It registers the provided function in the `functions` dictionary under the specified name. If a function with the same name already exists, it will be overwritten.

- **ExecuteFunction(string name, params object[] args)**: 
  - This method attempts to execute a registered function by its name. It takes the function name and an array of arguments as parameters. If the function exists in the registry, it invokes the function with the provided arguments and returns the result. If the function is not found, it throws an exception indicating that the function does not exist.

- **HasFunction(string name)**: 
  - This method checks if a function with the specified name is registered in the `functions` dictionary. It returns `true` if the function exists, and `false` otherwise. This is useful for verifying the presence of a function before attempting to execute it.