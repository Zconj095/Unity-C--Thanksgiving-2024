# CLIController

## Overview
The `CLIController` class is designed to manage command-line interface (CLI) commands within a Unity application. It allows developers to register commands and execute them based on user input. This class fits into a larger codebase by providing a way to handle user interactions through textual commands, making it easier to extend and manage functionalities in a modular manner.

## Variables
- `commands`: A `Dictionary<string, Action<string>>` that maps command strings to their corresponding action methods. This allows for dynamic command registration and execution based on user input.

## Functions
- `CLIController()`: Constructor that initializes the `commands` dictionary, preparing it to hold command-action pairs.

- `RegisterCommand(string command, Action<string> action)`: This method allows the registration of a new command. It takes a command string and an action (a method that takes a string argument) as parameters. The command is stored in the `commands` dictionary, pairing it with the specified action.

- `ExecuteCommand(string input)`: This method processes the input string, splits it into command and arguments, and checks if the command exists in the `commands` dictionary. If found, it executes the associated action with the remaining arguments; if not, it outputs "Unknown command." to the console.