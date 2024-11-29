# SpeechEncryptor

## Overview
The `SpeechEncryptor` class is responsible for encrypting and decrypting audio data, specifically speech captured in the form of an `AudioClip`. It utilizes an instance of the `VocalEncryptor` class to perform the actual encryption and decryption processes. This script fits into the larger codebase by providing a secure means to handle audio data, ensuring that sensitive speech information can be protected when stored or transmitted.

## Variables
- `encryptor`: An instance of the `VocalEncryptor` class that handles the encryption and decryption of audio samples.

## Functions
- `EncryptSpeech(AudioClip clip)`: 
  - This method takes an `AudioClip` as input, retrieves its audio samples, and passes them to the `encryptor` for encryption. It returns the encrypted audio data as a byte array.

- `DecryptSpeech(byte[] encryptedData, int sampleRate)`:
  - This method accepts a byte array containing encrypted audio data and a sample rate. It uses the `encryptor` to decrypt the audio data back into float samples, then creates a new `AudioClip` from those samples. It returns the newly created `AudioClip` containing the decrypted speech.