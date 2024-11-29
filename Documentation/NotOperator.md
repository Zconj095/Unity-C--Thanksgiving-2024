# NotOperator

## Overview
The `NotOperator` class implements the NOT operator for fuzzy logic, specifically designed to calculate the complement of a fuzzy membership value. This operator is part of the `UnityFuzzy` namespace and adheres to the `IUnaryOperator` interface. The main function of this script is to provide a method to evaluate the NOT operation on fuzzy membership values, which is essential for fuzzy set operations within the codebase.

## Variables
- **None**: The `NotOperator` class does not define any instance variables. It relies solely on the input parameter for its operations.

## Functions

### `Evaluate(float membership)`
- **Parameters**: 
  - `membership`: A float representing the fuzzy membership value, which should be within the range [0..1].
- **Returns**: A float that represents the result of applying the NOT operation to the input `membership`.
- **Description**: This method calculates the complement of the provided fuzzy membership value. It attempts to use reflection to dynamically invoke the subtraction method from the `UnityEngine.Mathf` class. If the `Subtract` method is not found, it defaults to a manual subtraction operation (`1 - membership`). This allows for flexibility in how the subtraction is performed, while ensuring the functionality remains intact.