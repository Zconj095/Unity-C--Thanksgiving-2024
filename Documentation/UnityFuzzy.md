# CentroidDefuzzifier

## Overview
The `CentroidDefuzzifier` class implements the centroid defuzzification method, a technique used in fuzzy logic to convert fuzzy outputs into a single crisp value. This class utilizes Unity APIs and reflection to dynamically access properties and methods of provided fuzzy output and normalization operator objects. It fits within the broader UnityFuzzy codebase, which likely handles fuzzy logic operations and calculations.

## Variables
- `intervals`: An integer that specifies the number of intervals to divide the range between the start and end of the output variable. This is used to incrementally evaluate the fuzzy outputs.

## Functions
- **Constructor: `CentroidDefuzzifier(int intervals)`**
  - Initializes a new instance of the `CentroidDefuzzifier` class with a specified number of intervals.

- **Method: `Defuzzify(object fuzzyOutput, object normOperator)`**
  - Takes a fuzzy output object and a normalization operator object as parameters.
  - Calculates the defuzzified output using the centroid method by iterating over a range defined by the output variable's start and end values. It utilizes reflection to access properties and methods dynamically, evaluating the membership values and applying the normalization operation to compute the weighted sum and total membership sum.
  - Returns the defuzzified crisp value. Throws an exception if all memberships are zero.

- **Private Method: `GetPropertyValue<T>(object obj, string propertyName)`**
  - A helper method that retrieves the value of a specified property from an object using reflection. It returns the value cast to the specified type `T`.
  - Throws an exception if the property is not found.

- **Private Method: `GetPropertyValue(object obj, string propertyName)`**
  - Overloaded method that retrieves the value of a specified property from an object using reflection and returns it as an object. This is a convenience method that calls the generic version.