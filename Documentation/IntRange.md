# IntRange Structure

## Overview
The `IntRange` structure is designed to represent a range of integer values with defined minimum and maximum boundaries. It is a part of the `EdgeLoreMachineLearning` namespace and serves as a utility to ensure that integer values fall within a specified range. This can be particularly useful in machine learning applications where certain parameters must adhere to specific limits.

## Variables
- `Min`: An integer that represents the minimum value of the range.
- `Max`: An integer that represents the maximum value of the range.

## Functions
- **Constructor `IntRange(int min, int max)`**: Initializes a new instance of the `IntRange` structure. It takes two parameters, `min` and `max`, and checks if `min` is greater than `max`. If it is, an exception is thrown to prevent invalid range creation.

- **Method `Contains(int value)`**: Checks if a given integer `value` falls within the defined range (inclusive). It returns `true` if the value is within the range, and `false` otherwise.

- **Method `ToString()`**: Provides a string representation of the `IntRange` instance in the format `[Min, Max]`, making it easy to visualize the range when debugging or logging.