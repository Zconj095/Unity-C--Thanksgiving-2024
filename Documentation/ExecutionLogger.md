# ExecutionLogger

## Overview
The `ExecutionLogger` class is designed to manage and record logs within a Unity application. It provides functionality to log general messages and error messages, while also allowing retrieval of all logged entries. This class is useful for debugging and tracking the flow of execution in the codebase, making it easier for developers to understand the application's behavior during runtime.

## Variables
- `logs`: A private list of strings that stores all log messages recorded by the logger. This includes both regular log messages and error messages.

## Functions
- `ExecutionLogger()`: Constructor that initializes the `logs` variable as an empty list of strings when an instance of `ExecutionLogger` is created.

- `Log(string message)`: This method takes a string message as input, adds it to the `logs` list, and outputs it to the console. It is used for logging general information.

- `LogError(string error)`: This method takes an error message as input, formats it by prefixing with "ERROR: ", adds it to the `logs` list, and outputs it to the console. It is specifically used for logging error messages.

- `GetLogs()`: This method returns the complete list of logged messages as a `List<string>`. It allows other parts of the codebase to access the logs for further analysis or display.