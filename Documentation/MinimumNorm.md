# MinimumNorm Class Documentation

## Overview
The `MinimumNorm` class implements the `INorm` interface and is designed to calculate the linguistic value of an AND operation using the minimum norm method. This class fits within the broader context of fuzzy logic operations, specifically for evaluating fuzzy membership values. The minimum operator is used to compute the AND operation between two fuzzy memberships, which is a common requirement in fuzzy logic systems.

## Variables
- **None**: This class does not define any instance variables. It operates solely through its method parameters.

## Functions

### Evaluate
```csharp
public float Evaluate(float membershipA, float membershipB)
```
- **Description**: This method calculates the numerical result of the AND operation applied to two fuzzy membership values, `membershipA` and `membershipB`, which should each be in the range [0..1]. It uses reflection to dynamically find and invoke the `Mathf.Min` method from the UnityEngine namespace to determine the minimum value between the two memberships. If the `Mathf.Min` method cannot be found, it throws an `InvalidOperationException`.

- **Parameters**:
  - `membershipA`: A fuzzy membership value, expected to be between 0 and 1.
  - `membershipB`: A fuzzy membership value, expected to be between 0 and 1.

- **Returns**: The method returns a float that represents the result of the AND operation, which is the minimum of `membershipA` and `membershipB`.