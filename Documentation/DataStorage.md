# DataStorage

## Overview
The `DataStorage` class provides functionalities for securely storing and validating user data, specifically usernames and their corresponding hashed passwords. It utilizes SHA256 hashing to ensure that passwords are not stored in plain text, thereby enhancing security. The class integrates with Unity's `PlayerPrefs` for data storage, making it suitable for use in Unity-based applications. This class is essential for managing user authentication and protecting sensitive information within the codebase.

## Variables
- **None**: The `DataStorage` class does not contain any instance variables. All methods are static and operate directly on the parameters passed to them.

## Functions

### `HashData(string input)`
- **Description**: This method takes a string input, hashes it using the SHA256 algorithm, and returns the hashed value as a Base64-encoded string. This function is crucial for transforming passwords into a secure format before storage.

### `StoreData(string username, string hashedPassword)`
- **Description**: This method stores a username and its corresponding hashed password securely using Unity's `PlayerPrefs`. It saves the data to disk to ensure persistence. This function is vital for saving user credentials securely.

### `ValidateData(string username, string input)`
- **Description**: This method checks if a username exists in the stored data. If it does, it compares the hashed version of the provided input (password) against the stored hashed password. It returns `true` if they match, indicating successful validation, and `false` otherwise. This function is essential for authenticating users by verifying their credentials.