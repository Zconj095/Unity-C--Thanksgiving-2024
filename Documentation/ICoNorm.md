# ReflectiveCoNorm.cs

## Overview
The `ReflectiveCoNorm` class implements the `ICoNorm` interface, which defines a method for performing CoNorm operations on fuzzy membership values. Specifically, this class provides the functionality to evaluate the maximum of two fuzzy membership values using reflection to call the `Mathf.Max` method from the Unity Engine. This class is part of the UnityFuzzy namespace and fits into a broader codebase that deals with fuzzy logic operations.

## Variables
- **membershipA**: A float representing the first fuzzy membership value, constrained within the range [0..1].
- **membershipB**: A float representing the second fuzzy membership value, also constrained within the range [0..1].

## Functions
- **Evaluate(float membershipA, float membershipB)**: 
  - This method calculates the result of a CoNorm (OR) operation applied to the two provided fuzzy membership values. It uses reflection to dynamically invoke the `Mathf.Max` method from the UnityEngine namespace. The method returns the maximum of the two input membership values. If the `Mathf.Max` method cannot be found, it throws an `InvalidOperationException`.