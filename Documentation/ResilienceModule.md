# ResilienceModule

## Overview
The `ResilienceModule` script is designed to handle error messages in a Unity application. Its primary function is to provide user-friendly feedback based on the type of error encountered. By categorizing errors into specific types, the script helps maintain a smooth user experience by informing users of the necessary actions to take when an error occurs. This module can be integrated into other parts of the codebase where error handling is required, ensuring consistency in user communication.

## Variables
- **errorType (string)**: This variable represents the type of error that has occurred. It is passed as an argument to the `HandleError` function to determine the appropriate error message to return.

## Functions
- **HandleError(string errorType)**: This function takes a string parameter representing the type of error. It uses a switch statement to return a corresponding error message based on the error type provided. The function handles three cases:
  - If the error type is "network", it returns "Reconnecting...".
  - If the error type is "input", it returns "Invalid input, please try again.".
  - For any other error type, it returns "An unexpected error occurred.".

This function is essential for providing clear and concise feedback to users, allowing them to understand the nature of the problem and how to respond.