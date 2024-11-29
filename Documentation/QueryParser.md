# QueryParser

## Overview
The `QueryParser` class is designed to preprocess and tokenize input strings, making it easier to handle queries in a consistent format. The preprocessing step converts the input into a standardized form by transforming it to lowercase, removing special characters, and normalizing whitespace. The tokenization step generates an array of hash codes for each word in the input string. This class is particularly useful in scenarios where input needs to be processed before further analysis or storage, fitting seamlessly into a larger codebase that handles user queries or text data.

## Variables
- **None**: This class does not use any instance variables. All methods are static and operate solely on their input parameters.

## Functions

### Preprocess
```csharp
public static string Preprocess(string input)
```
- **Description**: This function takes a string input and processes it by performing the following steps:
  - Converts the entire string to lowercase to ensure uniformity.
  - Removes any special characters that are not alphanumeric or whitespace.
  - Normalizes whitespace by replacing multiple spaces with a single space and trimming leading or trailing spaces.
- **Parameters**: 
  - `input`: A string that represents the raw input to be processed.
- **Returns**: A cleaned and standardized string.

### Tokenize
```csharp
public static int[] Tokenize(string input)
```
- **Description**: This function takes a preprocessed string input and converts it into an array of integers by computing the hash code for each word in the input. This transformation allows for efficient storage and comparison of words.
- **Parameters**: 
  - `input`: A string containing the preprocessed words separated by spaces.
- **Returns**: An array of integers, where each integer is the hash code of a word from the input string.