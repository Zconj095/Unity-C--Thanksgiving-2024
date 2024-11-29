# ProcessManager

## Overview
The `ProcessManager` script is designed for managing system processes within a Unity application. It allows users to terminate a specified process and list all currently running processes. This script is particularly useful for developers who need to monitor or control external applications while working in a Unity environment. The `KillProcess` and `ListProcesses` methods provide essential functionality for process management.

## Variables

- `processNameInput`: An `InputField` component where users can type the name of the process they wish to terminate.
- `feedbackText`: A `Text` component that displays feedback messages to the user, indicating the success or failure of process termination.

## Functions

- `KillProcess()`: This method retrieves the name of the process from the `processNameInput` field and attempts to terminate all instances of that process. If successful, it updates `feedbackText` to confirm that the process has been terminated. If an error occurs during the termination attempt, it catches the exception and displays the error message in `feedbackText`.

- `ListProcesses()`: This method retrieves all currently running processes on the system and logs their names and IDs to the Unity console. It provides a way for users to see which processes are active at any given time, aiding in the management of system resources.