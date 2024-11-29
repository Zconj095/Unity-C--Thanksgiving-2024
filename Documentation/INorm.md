# UnityFuzzy: ReflectiveNorm Class

## Overview
The `ReflectiveNorm` class implements the `INorm` interface, which defines a method for performing fuzzy logical operations. Specifically, this class computes the result of a fuzzy "AND" operation on two fuzzy membership values by utilizing the `Mathf.Min` method from Unity's `Mathf` class. This class is a part of the UnityFuzzy namespace, which is likely designed to provide functionality for fuzzy logic operations within Unity-based applications.

## Variables
- **None**: The `ReflectiveNorm` class does not define any member variables. It relies solely on the method parameters for its operations.

## Functions

### `Evaluate(float membershipA, float membershipB)`
- **Description**: This method takes two fuzzy membership values as input parameters and computes the result of the "AND" operation by returning the minimum of the two values. It does this by dynamically invoking the `Mathf.Min` method using reflection. 
- **Parameters**:
  - `membershipA`: A float representing the first fuzzy membership value, expected to be in the range [0..1].
  - `membershipB`: A float representing the second fuzzy membership value, also expected to be in the range [0..1].
- **Returns**: A float that is the result of the "AND" operation applied to `membershipA` and `membershipB`. If the `Mathf.Min` method cannot be found, an `InvalidOperationException` is thrown.