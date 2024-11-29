# UserInputHandler

## Overview
The `UserInputHandler` script is designed to manage user input within a Unity application, specifically handling login and processing submissions. This script interacts with UI elements such as input fields and buttons, capturing user data and providing feedback based on the validity of the input. It serves as a crucial component for user interaction, ensuring that the application properly processes user credentials and other relevant information.

## Variables
- `InputField usernameInput`: A UI input field for the user to enter their username.
- `InputField passwordInput`: A UI input field for the user to enter their password.
- `Button loginButton`: A button that triggers the login process when clicked.
- `InputField processNameInput`: A UI input field for the user to enter the name of the process.
- `InputField spectralContentInput`: A UI input field for the user to enter spectral content data.
- `Button processButton`: A button that triggers the process submission when clicked.
- `Text feedbackText`: A UI text element that displays feedback messages to the user regarding their input.

## Functions
- `void Start()`: Initializes the script by attaching click listeners to the login and process buttons. This function is called when the script instance is being loaded.
  
- `void OnLoginSubmit()`: Handles the logic for the login submission. It retrieves the username and password from the input fields, checks if either is empty, and updates the feedback text accordingly. If both fields are filled, it logs the submitted information and provides a confirmation message.

- `void OnProcessSubmit()`: Manages the process submission logic. Similar to `OnLoginSubmit`, it retrieves the process name and spectral content from their respective input fields, checks for empty values, and updates the feedback text. If the inputs are valid, it logs the submitted process information and provides confirmation feedback.