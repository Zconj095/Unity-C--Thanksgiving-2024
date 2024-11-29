# LatticeEncryption

## Overview
The `LatticeEncryption` class provides functionality for encrypting and decrypting data using lattice-based cryptography. This is particularly useful in secure communications where sensitive information needs to be transmitted safely. The class contains two main functions: `Encrypt` and `Decrypt`, which handle the encryption and decryption processes, respectively. The encryption process utilizes a public key, while the decryption process relies on a private key. Both functions operate on integer arrays and make use of a modulus to ensure that the results remain within a specified range.

## Variables
- `data` (int[]): An array of integers representing the plaintext data that needs to be encrypted.
- `publicKey` (int[,]): A two-dimensional integer array representing the public key used for encryption.
- `modulus` (int): An integer that defines the modulus used in the encryption and decryption processes to keep results within a manageable range.
- `encryptedData` (int[]): An array of integers that stores the result of the encryption process.
- `privateKey` (int[,]): A two-dimensional integer array representing the private key used for decryption.
- `decryptedData` (int[]): An array of integers that stores the result of the decryption process.
- `size` (int): An integer representing the size of the public or private key, which determines the length of the resulting encrypted or decrypted data.

## Functions
### Encrypt
```csharp
public static int[] Encrypt(int[] data, int[,] publicKey, int modulus)
```
- **Description**: This function takes an array of integers (`data`), a two-dimensional public key (`publicKey`), and a modulus value (`modulus`). It computes the encrypted data by performing matrix multiplication between the public key and the input data, followed by applying the modulus operation to each result. The output is an integer array containing the encrypted data.

### Decrypt
```csharp
public static int[] Decrypt(int[] encryptedData, int[,] privateKey, int modulus)
```
- **Description**: This function takes an array of integers (`encryptedData`), a two-dimensional private key (`privateKey`), and a modulus value (`modulus`). It computes the decrypted data by performing matrix multiplication between the private key and the encrypted data, followed by applying the modulus operation to each result. The output is an integer array containing the decrypted data.