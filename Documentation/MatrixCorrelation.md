# MatrixCorrelation

## Overview
The `MatrixCorrelation` script defines a method for computing the correlation between two vectors (arrays of floats) in a Unity environment. This correlation can be useful in various applications, such as data analysis, machine learning, or any scenario where understanding the relationship between two datasets is necessary. The `ComputeCorrelation` function calculates the cosine similarity between the two input vectors, providing a measure of how closely they relate to each other.

## Variables
- `dot` (float): This variable accumulates the dot product of the two input vectors, which is a measure of their directional alignment.
- `mag1` (float): This variable stores the magnitude (length) of the first vector (`vec1`), calculated as the square root of the sum of the squares of its components.
- `mag2` (float): This variable stores the magnitude (length) of the second vector (`vec2`), calculated similarly to `mag1`.

## Functions
- `ComputeCorrelation(float[] vec1, float[] vec2)`: This static method takes two float arrays as input parameters. It computes the dot product of the two vectors and their magnitudes, then returns the cosine of the angle between them as a float value. This value indicates the degree of correlation between the two vectors, ranging from -1 (completely opposite) to 1 (completely aligned).