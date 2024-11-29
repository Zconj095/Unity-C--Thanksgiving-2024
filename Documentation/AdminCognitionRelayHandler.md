# AdminCognitionRelayHandler

## Overview
The `AdminCognitionRelayHandler` class is a Unity component responsible for processing input commands related to system state management and feedback. It acts as a bridge between user inputs and the underlying system states, allowing for updates and resets of the system. This class fits within a larger codebase by managing state transitions and relaying feedback through events, enabling other components to respond to changes in the system's status.

## Variables

- **InputData**: A serializable class that holds input command data.
  - `command`: A string representing the command to be processed.
  - `parameters`: A string containing any parameters associated with the command.

- **StateManager**: A private class that manages the state of the system.
  - `states`: A dictionary that stores key-value pairs representing the state of various components.

- **stateManager**: An instance of the `StateManager` class used to manage the state of the system.

- **OnFeedbackRelay**: An event that is triggered to relay feedback messages and associated data.

## Functions

- **LogFeedback(string message)**: A private method that logs feedback messages to the Unity console prefixed with "[ACRH Feedback]".

- **Start()**: Unity's built-in method that initializes the state manager with default states when the component starts.
  - Initializes `SystemActive` to `true`.
  - Initializes `FeedbackCount` to `0`.

- **ProcessInput(InputData input)**: The main method for processing input commands.
  - Checks if the system is active. If not, logs a message and exits.
  - Processes commands based on the input:
    - `"update"`: Calls `HandleUpdateCommand` with the provided parameters.
    - `"reset"`: Calls `HandleResetCommand`.
    - Logs an unknown command message if the command is not recognized.

- **HandleUpdateCommand(string parameters)**: A private method that processes update commands.
  - Logs the parameters being processed.
  - Updates the `LastUpdateParams` state with the provided parameters.
  - Increments the `FeedbackCount` state.
  - Triggers the `OnFeedbackRelay` event with the update status.

- **HandleResetCommand()**: A private method that resets the system's state.
  - Logs a reset message.
  - Resets the `FeedbackCount` state to `0`.
  - Clears the `LastUpdateParams` state.

- **Update()**: Unity's built-in method that is called once per frame.
  - Monitors the `FeedbackCount` state and logs a message if it exceeds 5, indicating high feedback activity.