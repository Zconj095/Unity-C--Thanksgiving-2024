# DoubleRange

## Overview
The `DoubleRange` struct is designed to represent a range of double values within a specified minimum and maximum. This struct provides a way to encapsulate the concept of a numeric range, ensuring that the minimum value is always less than or equal to the maximum value. It includes functionality to check if a specific value falls within this range and provides a string representation of the range.

## Variables
- **Min**: A read-only property that represents the minimum value of the range.
- **Max**: A read-only property that represents the maximum value of the range.

## Functions
- **DoubleRange(double min, double max)**: Constructor that initializes a new instance of the `DoubleRange` struct. It takes two parameters, `min` and `max`, and throws an `ArgumentException` if `min` is greater than `max`.

- **Contains(double value)**: A method that checks if the specified `value` is within the range defined by `Min` and `Max`. It returns `true` if the value is within the range, and `false` otherwise.

- **ToString()**: Overrides the default string representation method to return a formatted string that displays the range in the format `[Min, Max]`.