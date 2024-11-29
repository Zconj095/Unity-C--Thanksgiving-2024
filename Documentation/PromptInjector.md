# PromptInjector Script Documentation

## Overview
The `PromptInjector` script is a Unity component that manages and accumulates prompts in a string format. It allows for the injection of new prompts and retrieval of the current list of prompts. This script is particularly useful in scenarios where you need to maintain a history of prompts or messages, such as in chat applications or interactive storytelling within a Unity game. It fits within the broader codebase by providing a simple interface for prompt management, which can be utilized by other components or systems that require prompt handling.

## Variables
- **currentPrompt**: A private string that holds the accumulated prompts. It starts as an empty string and is updated whenever a new prompt is injected.

## Functions
- **Inject(string newPrompt)**: 
  - Description: This public method takes a string parameter `newPrompt` and appends it to the `currentPrompt` variable, followed by a newline character. This allows multiple prompts to be stored in a single string, separated by new lines.
  
- **GetPrompt()**: 
  - Description: This public method returns the current value of the `currentPrompt` variable. It allows other components to access the complete list of prompts that have been injected so far.