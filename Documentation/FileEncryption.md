# FileEncryption

## Overview
The `FileEncryption` class is designed to facilitate the saving and loading of encrypted files. It provides two main functionalities: `SaveEncryptedFile` to store encrypted content to a specified file path, and `LoadEncryptedFile` to retrieve and decrypt content from a file. This class utilizes AES encryption to ensure that the data is securely stored and can only be accessed in its original form when decrypted. It fits within a larger codebase that likely deals with file handling and security, ensuring that sensitive information remains protected.

## Variables
- **path**: A string that represents the file path where the encrypted content will be saved or from which it will be loaded.
- **content**: A string that contains the data to be encrypted and saved to a file.
- **encryptedContent**: A string that holds the encrypted version of the content, either for saving to a file or for decrypting after loading.

## Functions
- **SaveEncryptedFile(string path, string content)**: 
  - This static method takes a file path and content as parameters. It encrypts the content using the `AESEncryption.Encrypt` method and saves the encrypted data to the specified file path using `File.WriteAllText`.

- **LoadEncryptedFile(string path)**: 
  - This static method takes a file path as a parameter. It reads the encrypted content from the specified file using `File.ReadAllText`, then decrypts it using the `AESEncryption.Decrypt` method, and returns the original content.