# UserManager

## Overview
The `UserManager` script is responsible for managing user authentication in a Unity application. It allows users to add new accounts, validate existing accounts, and remove accounts from a simulated user database. This script interacts with the user interface by using input fields for username and password, and it provides feedback to the user through a text element. It fits into the broader codebase by serving as a central component for user management, ensuring that user data is handled securely and efficiently.

## Variables
- `usernameInput`: An `InputField` component where users can enter their desired username.
- `passwordInput`: An `InputField` component where users can enter their desired password.
- `feedbackText`: A `Text` component that displays messages to the user, such as errors or confirmations.
- `userDatabase`: A private `Dictionary<string, string>` that simulates a database for storing usernames and their corresponding passwords.

## Functions
- `AddUser()`: This function adds a new user to the `userDatabase`. It checks if the username and password fields are not empty and verifies that the username does not already exist in the database. If successful, it stores the username and password and provides feedback to the user.
  
- `ValidateUser()`: This function checks if a user exists in the `userDatabase` and validates the entered password. If the username and password match, it confirms successful authentication; otherwise, it provides appropriate error messages for incorrect passwords or non-existent users.

- `RemoveUser()`: This function removes a user from the `userDatabase` based on the entered username. If the user is successfully removed, it provides confirmation feedback; if the user does not exist, it informs the user of the error.