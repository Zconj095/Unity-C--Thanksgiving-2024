# AESEncryption

## Overview
The `AESEncryption` class provides functionality for encrypting and decrypting text using the Advanced Encryption Standard (AES) algorithm. It is designed to securely transform plain text into an encrypted format and vice versa, ensuring data confidentiality. This class can be integrated into a larger codebase where secure data transmission or storage is required, allowing developers to easily implement encryption and decryption features.

## Variables
- **Key**: A static readonly string that serves as the encryption key. It must be either 16, 24, or 32 characters long to comply with AES encryption standards.
- **IV**: A static readonly string that represents the initialization vector (IV). It must be exactly 16 characters long, which is necessary for the AES algorithm to ensure secure encryption.

## Functions
- **Encrypt(string plainText)**: 
  - Takes a plain text string as input and returns its encrypted version as a Base64 encoded string. This function creates an AES encryptor using the predefined key and IV, converts the plain text into bytes, and then transforms it into encrypted bytes.

- **Decrypt(string encryptedText)**: 
  - Accepts an encrypted Base64 encoded string and returns the original plain text string. This function creates an AES decryptor with the same key and IV, decodes the Base64 string back into byte format, and then transforms it back into the original plain text bytes.