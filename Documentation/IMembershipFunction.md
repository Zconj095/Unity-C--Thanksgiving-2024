# IMembershipFunction Interface

## Overview
The `IMembershipFunction` interface defines the essential methods and properties that all membership functions must implement within the UnityFuzzy namespace. This interface is crucial for calculating a value's degree of membership to a fuzzy set, enabling the use of fuzzy logic in various applications. By standardizing the methods and properties for membership functions, it ensures consistency and interoperability among different implementations in the codebase.

## Variables
- **LeftLimit**: 
  - Type: `float`
  - Description: Represents the leftmost x value of the membership function. This defines the lower boundary of the fuzzy set.

- **RightLimit**: 
  - Type: `float`
  - Description: Represents the rightmost x value of the membership function. This defines the upper boundary of the fuzzy set.

## Functions
- **GetMembership(float x)**: 
  - Description: This method calculates the degree of membership of a given value `x` to the fuzzy set. It returns a float value between 0 and 1, indicating how strongly the value belongs to the fuzzy set. A return value of 0 means no membership, while a return value of 1 means full membership.