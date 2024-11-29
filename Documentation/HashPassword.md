# HashingUtility

## Overview
The `HashingUtility` class provides a method for securely hashing passwords using the SHA-256 cryptographic hash function. This is an essential practice in safeguarding sensitive information such as user passwords within a codebase. By converting a plain text password into a fixed-size string of characters (the hash), it ensures that the original password cannot be easily retrieved, thereby enhancing security.

## Variables
- **password**: A string input representing the plain text password that needs to be hashed.

## Functions
### HashPassword
- **Description**: This static method takes a plain text password as input and returns its hashed version as a Base64 encoded string. It utilizes the SHA-256 algorithm to compute the hash, ensuring that the password is securely transformed into an irreversible format. The method performs the following steps:
  1. Creates an instance of the SHA256 hashing algorithm.
  2. Converts the password string into a byte array using UTF-8 encoding.
  3. Computes the hash of the byte array.
  4. Converts the resulting byte array into a Base64 string for easier storage and transmission.
  
This function is crucial for applications that require user authentication, as it provides a secure way to handle user passwords.