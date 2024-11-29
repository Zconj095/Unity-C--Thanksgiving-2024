# Database Class Documentation

## Overview
The `Database` class represents a fuzzy database that stores a collection of linguistic variables used in a Fuzzy Inference System. It provides functionality to add, retrieve, and clear these variables, allowing for the management of fuzzy logic components within the codebase. This class is essential for maintaining the state of linguistic variables, which are crucial for the operation of fuzzy logic systems.

## Variables
- **variables**: A private dictionary that holds linguistic variables, where the key is the variable name (a string) and the value is the variable object itself.

## Functions
- **Database()**: Constructor that initializes a new instance of the `Database` class. It sets up the `variables` dictionary with an initial capacity of 10 to store linguistic variables.

- **AddVariable(object variable)**: This method adds a linguistic variable to the database. It uses reflection to retrieve the `Name` property of the provided variable and checks for its existence in the database. If the variable is not initialized or if a variable with the same name already exists, it throws an exception.

- **ClearVariables()**: This method removes all linguistic variables from the database by clearing the `variables` dictionary.

- **GetVariable(string variableName)**: This method retrieves a linguistic variable by its name from the database. If the variable does not exist, it throws a `KeyNotFoundException`. If found, it returns a reference to the requested linguistic variable.