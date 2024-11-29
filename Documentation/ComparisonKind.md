# ComparisonKind.cs

## Overview
The `ComparisonKind.cs` script defines an enumeration and extension methods that facilitate numeric comparisons within a Unity decision tree context. The `ComparisonKind` enumeration categorizes various types of comparisons (e.g., equality, greater than, less than) that can be performed between numeric values. The extension methods provide functionality to convert these comparison types into symbols and evaluate comparisons between numeric values, enhancing the decision-making capabilities of the codebase.

## Variables
- **ComparisonKind**: An enumeration that represents the different kinds of comparisons that can be made. The possible values include:
  - `None`: Indicates no comparison is made.
  - `Equal`: Checks if two values are equal.
  - `NotEqual`: Checks if two values are not equal.
  - `GreaterThanOrEqual`: Checks if one value is greater than or equal to another.
  - `GreaterThan`: Checks if one value is greater than another.
  - `LessThan`: Checks if one value is less than another.
  - `LessThanOrEqual`: Checks if one value is less than or equal to another.

## Functions
- **ToSymbolString(this ComparisonKind comparison)**: 
  - Converts a `ComparisonKind` enumeration value into its corresponding string representation of the comparison symbol (e.g., `==`, `>`, `<`, etc.).

- **Evaluate(this ComparisonKind comparison, double value1, double value2)**: 
  - Performs a numeric comparison between two double values based on the specified `ComparisonKind`. It returns a boolean indicating the result of the comparison. It handles various comparison types and logs an error if an invalid type is used.

- **ReflectiveEvaluate(this ComparisonKind comparison, object value1, object value2)**: 
  - Dynamically evaluates a comparison between two objects using Reflection. It converts the objects to doubles and uses the `Evaluate` method to perform the comparison. It also includes error handling for null values and conversion exceptions, logging any errors encountered during the process.