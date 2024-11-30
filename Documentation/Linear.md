# Linear Class Documentation

## Overview
The `Linear` class is part of the `EdgeLoreMachineLearning` namespace and provides functionality for computing the dot product between two vectors, which is a fundamental operation in machine learning, particularly in support vector machines (SVM). This class also includes a method to determine the length of input vectors. It serves as a utility within the overall machine learning framework, allowing for linear computations that are essential for various algorithms.

## Variables
- **None**: The `Linear` class does not have any instance variables. It operates solely through its methods, which utilize parameters passed to them.

## Functions

### Compute
```csharp
public double Compute(double[] a, double[] b)
```
- **Description**: This method calculates the dot product of two input vectors `a` and `b`. It first checks if both vectors have the same length; if not, it throws an `ArgumentException`. If they are of equal length, it iterates through each element, multiplying corresponding elements of the two vectors and summing the results to return the final dot product.

### GetLength
```csharp
public int GetLength(double[][] inputs)
```
- **Description**: This method returns the length of the first vector in a two-dimensional array of vectors (`inputs`). If the `inputs` array is empty, it returns 0. This function is useful for determining the dimensionality of the input data being processed.