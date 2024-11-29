# Antecedent Struct Documentation

## Overview
The `Antecedent` struct represents an antecedent expression used in decision rules within the EdgeLore Machine Learning framework. It encapsulates the logic for comparing a specific variable's value against a defined value using various comparison types. This struct is essential for evaluating conditions in decision-making algorithms, allowing the system to determine if a certain rule applies based on input data.

## Variables
- **index**: An integer representing the position of the variable in the input array that is being evaluated.
- **comparison**: An instance of the `ComparisonKind` enum that specifies the type of comparison to be made (e.g., equal, greater than).
- **value**: A double that holds the value against which the variable at the specified index will be compared.

## Functions
- **Index**: 
  - A public property that retrieves the index of the variable used as the left-hand side term of the antecedent expression.

- **Comparison**: 
  - A public property that retrieves the comparison type being made between the variable value at the specified index and the defined value.

- **Value**: 
  - A public property that retrieves the right-hand side value of the antecedent expression.

- **Antecedent(int index, ComparisonKind comparison, double value)**: 
  - A constructor that initializes a new instance of the `Antecedent` struct with the specified variable index, comparison type, and value.

- **Match(double[] input)**: 
  - A public method that checks if the antecedent applies to a given input vector. It returns true if the value at the specified index in the input matches the defined value according to the specified comparison; otherwise, it returns false. It also handles cases where the input value is missing (NaN).

- **Equals(Antecedent other)**: 
  - A public method that determines whether the specified `Antecedent` instance is equal to the current instance based on its index, comparison, and value.

- **Equals(object obj)**: 
  - An overridden method that checks if the specified object is equal to the current instance, using the `Equals(Antecedent other)` method for comparison.

- **GetHashCode()**: 
  - An overridden method that returns a hash code for the current instance, which is useful for using the struct in collections.

- **ToString()**: 
  - An overridden method that returns a string representation of the `Antecedent`, showing the variable index, comparison operator, and value.

- **operator ==(Antecedent a, Antecedent b)**: 
  - A static method that implements the equality operator, allowing for comparison between two `Antecedent` instances.

- **operator !=(Antecedent a, Antecedent b)**: 
  - A static method that implements the inequality operator, allowing for determining if two `Antecedent` instances are not equal.