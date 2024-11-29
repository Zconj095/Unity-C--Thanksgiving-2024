# BinaryDataProcessor

## Overview
The `BinaryDataProcessor` script is designed for processing binary data files in Unity. Its main function, `ProcessBinaryData`, reads the contents of a specified binary file, normalizes the byte values to a range between 0 and 1, and returns an array of floats representing these normalized values. This functionality is crucial for applications that require the conversion of raw binary data into a usable format, such as machine learning models or graphical representations.

## Variables
- `binaryContent`: A byte array that holds the raw binary data read from the specified file. This array is populated by the `File.ReadAllBytes` method.
- `embedding`: A float array that stores the normalized values of the binary data. Its length is equal to the length of `binaryContent`, and each byte value is converted to a float in the range of [0, 1].

## Functions
- `ProcessBinaryData(string filePath)`: This static method takes a file path as an argument, reads the binary data from the file, normalizes each byte to a float value between 0 and 1, and returns an array of these float values. The normalization is done by dividing each byte by 255, which is the maximum value for a byte.