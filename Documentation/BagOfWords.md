# BagOfWords

## Overview
The `BagOfWords` class implements a Bag of Words model using Unity Reflection and Unity-compatible libraries. This model is used to process and analyze text data by converting text sequences into numerical feature vectors. It primarily serves as a tool for machine learning tasks where textual data needs to be represented in a quantifiable format. The class fits into the larger codebase by providing essential functionalities for text processing, allowing other components to leverage these capabilities for data analysis and machine learning applications.

## Variables

- `_stringToCode`: A dictionary that maps each unique word (string) to a corresponding integer code.
- `_codeToString`: A dictionary that maps each integer code back to its corresponding word (string).
- `_readOnlyStringToCode`: A read-only interface to the `_stringToCode` dictionary, preventing external modification.
- `_readOnlyCodeToString`: A read-only interface to the `_codeToString` dictionary, preventing external modification.
- `MaximumOccurrence`: An integer property that sets the maximum number of occurrences allowed for each word in the feature vector.
- `NumberOfWords`: A read-only property that returns the total number of unique words learned by the model.
- `StringToCode`: A read-only property that provides access to the `_readOnlyStringToCode` dictionary.
- `CodeToString`: A read-only property that provides access to the `_readOnlyCodeToString` dictionary.

## Functions

- `BagOfWords()`: Constructor that initializes a new instance of the `BagOfWords` class and calls the `Initialize` method to set up the necessary data structures.

- `Initialize()`: A private method that initializes the dictionaries used for mapping words to codes and vice versa, and sets the default value for `MaximumOccurrence`.

- `Learn(string[][] texts)`: A public method that takes an array of text sequences and builds the vocabulary by populating the `_stringToCode` and `_codeToString` dictionaries with unique words and their corresponding codes.

- `Transform(string[] input)`: A public method that converts a single text input into a feature vector represented as an integer array. It counts the occurrences of each word in the input and respects the `MaximumOccurrence` limit.

- `Transform(string[][] inputs)`: An overloaded public method that takes an array of text sequences and transforms each sequence into its corresponding feature vector, returning a 2D array of feature vectors.

- `GetProperty(object obj, string propertyName)`: A static method that uses reflection to retrieve the value of a specified property from a given object instance.

- `SetProperty(object obj, string propertyName, object value)`: A static method that uses reflection to set the value of a specified property on a given object instance.

- `InvokeMethod(object obj, string methodName, object[] parameters)`: A static method that uses reflection to invoke a specified method on a given object instance, passing the provided parameters and returning the result of the method invocation.