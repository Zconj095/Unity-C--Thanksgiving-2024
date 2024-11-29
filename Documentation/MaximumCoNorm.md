# MaximumCoNorm

## Overview
The `MaximumCoNorm` class is designed to calculate the linguistic value of an OR operation for fuzzy membership values using a maximum operator. This class implements the `ICoNorm` interface, which allows it to be part of a broader fuzzy logic framework. By employing reflection, it dynamically invokes the `Mathf.Max` method from Unity's engine to compute the maximum of two fuzzy membership values, thus enabling the evaluation of OR operations in fuzzy logic contexts.

## Variables
- **membershipA**: A float representing the first fuzzy membership value, constrained within the range [0..1].
- **membershipB**: A float representing the second fuzzy membership value, also constrained within the range [0..1].

## Functions
- **Evaluate(float membershipA, float membershipB)**: 
  - This method takes two fuzzy membership values as input and applies the OR operation by calculating the maximum of the two values. It utilizes reflection to dynamically access and invoke the `Mathf.Max` method from the Unity engine. The method returns the numerical result of the OR operation, which is a float value representing the greater of the two membership inputs. If the `Mathf.Max` method cannot be found, it throws an `InvalidOperationException`.