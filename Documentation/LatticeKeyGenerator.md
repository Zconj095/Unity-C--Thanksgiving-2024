# LatticeKeyGenerator

## Overview
The `LatticeKeyGenerator` class is designed to generate pairs of cryptographic keys: a public key and a private key. These keys are represented as two-dimensional arrays (matrices) of integers. The `GenerateKeys` method within this class takes in two parameters: the size of the key matrices and a modulus value, which dictates the range of random integers used to populate the keys. This functionality is essential in cryptographic systems where secure key generation is critical for data protection and encryption processes.

## Variables
- **size**: An integer that specifies the dimensions (both width and height) of the public and private key matrices. The keys will be square matrices of size `size x size`.
- **modulus**: An integer that defines the upper limit for the random integers generated for the key matrices. The values in both the public and private keys will be in the range of 0 to `modulus - 1`.
- **random**: An instance of the `Random` class used to generate random integers. This is essential for creating the randomness required in key generation.
- **publicKey**: A two-dimensional array (matrix) of integers that holds the generated public key. The dimensions are defined by the `size` parameter.
- **privateKey**: A two-dimensional array (matrix) of integers that holds the generated private key. Like the public key, its dimensions are also defined by the `size` parameter.

## Functions
- **GenerateKeys(int size, int modulus)**: This static method generates a pair of cryptographic keys (public and private) as two-dimensional integer arrays. It utilizes the provided `size` to determine the dimensions of the keys and `modulus` to set the range for the random integers. The method populates the keys with random values and returns them as a tuple containing both the public key and private key matrices.