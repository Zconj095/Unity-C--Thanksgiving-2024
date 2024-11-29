# RSAEncryption

## Overview
The `RSAEncryption` class provides functionality for generating RSA encryption keys, encrypting plaintext messages, and decrypting ciphertext. This script is part of a codebase that likely deals with secure data transmission or storage, ensuring that sensitive information can be safely encrypted and decrypted using RSA (Rivest-Shamir-Adleman) cryptographic algorithm.

## Variables
- `_publicKey`: This variable holds the public key parameters used for encryption. It is of type `RSAParameters`, which contains the necessary information to perform RSA encryption.
- `_privateKey`: This variable holds the private key parameters used for decryption. Like `_publicKey`, it is also of type `RSAParameters`, and it is essential for decrypting messages that were encrypted with the corresponding public key.

## Functions
- `GenerateKeys()`: This static method generates a new pair of RSA keys (public and private) using the `RSACryptoServiceProvider`. The generated keys are stored in the `_publicKey` and `_privateKey` variables for later use in encryption and decryption processes.

- `Encrypt(string plainText)`: This static method takes a string input (`plainText`), converts it into a byte array, and encrypts it using the public key stored in `_publicKey`. The encrypted byte array is then converted to a Base64 string and returned. This method allows for secure transmission of plaintext data.

- `Decrypt(string encryptedText)`: This static method takes a Base64 encoded string input (`encryptedText`), converts it back into a byte array, and decrypts it using the private key stored in `_privateKey`. The method returns the original plaintext string after decryption. This function is essential for retrieving the original data from its encrypted form.