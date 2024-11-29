# VocalEncryptor

## Overview
The `VocalEncryptor` class is a Unity script designed to handle the encryption and decryption of audio data represented as float samples. It provides functionality to convert audio samples into a string format, encrypt that string, and subsequently decrypt it back into the original float samples. This class is useful in scenarios where audio data needs to be securely stored or transmitted, ensuring that sensitive audio information is protected from unauthorized access.

## Variables
- `Key`: A static readonly string that serves as the encryption key. It must be either 16, 24, or 32 characters long to comply with AES encryption standards. This key is used in both the encryption and decryption processes.

## Functions
- `EncryptAudio(float[] samples)`: 
  - Takes an array of float samples as input, converts it to a string representation, and then encrypts that string. Returns the encrypted data as a byte array.

- `DecryptAudio(byte[] encryptedData)`: 
  - Accepts a byte array of encrypted data, decrypts it to retrieve the original string, and then converts that string back into an array of float samples. Returns the array of decrypted float samples.

- `ConvertSamplesToString(float[] samples)`: 
  - Converts an array of float samples into a single string, where each sample is separated by a comma. This string format is necessary for the encryption process.

- `ConvertStringToSamples(string data)`: 
  - Takes a string of comma-separated float samples and converts it back into an array of float values. This method is used during the decryption process to restore the original audio data.

- `EncryptString(string plainText)`: 
  - Encrypts a given plain text string using AES encryption with the predefined key and a default initialization vector (IV). Returns the encrypted data as a byte array.

- `DecryptString(byte[] encryptedBytes)`: 
  - Decrypts a byte array of encrypted data back into a plain text string using AES decryption with the same key and IV. Returns the decrypted string.