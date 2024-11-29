# GUIScript Documentation

## Overview
The `GUIScript` class is a Unity MonoBehaviour that manages user interactions within a graphical user interface (GUI). It allows users to input text through an `InputField`, trigger a response by clicking a `Button`, and display the response in a `Text` component. The script interacts with an instance of `LLMManager`, which is responsible for processing the user's input and generating a response asynchronously. This functionality is essential for creating interactive applications that require user input and feedback.

## Variables

- **inputField**: An `InputField` component where the user can type their prompt or query.
- **responseText**: A `Text` component that displays the response received from the `LLMManager` based on the user's input.
- **sendButton**: A `Button` component that, when clicked, triggers the sending of the user's input to the `LLMManager`.
- **llmManager**: A reference to the `LLMManager` instance in the scene, which handles the logic for processing the user's input and generating a response.

## Functions

- **Start()**: This is a Unity lifecycle method that is called when the script instance is being loaded. It initializes the `llmManager` by searching for an instance of `LLMManager` in the scene. If the instance is not found, it logs an error. Additionally, it sets up a listener on the `sendButton` that calls the `OnSendPromptClicked` method asynchronously when clicked.

- **OnSendPromptClicked()**: This is an asynchronous method that is invoked when the user clicks the `sendButton`. It first checks if the `responseText` is valid. If the `InputField` is empty, it prompts the user to enter a prompt. If there is user input, it updates the `responseText` to indicate that the prompt is being sent. It then calls the `SendPromptAsync` method of the `llmManager` with the user's input and awaits the response. Finally, it updates the `responseText` with the response received from the `LLMManager`, or logs a warning if the `responseText` was destroyed during the asynchronous operation.