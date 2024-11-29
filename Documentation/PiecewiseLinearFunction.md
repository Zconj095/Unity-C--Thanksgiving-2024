# PiecewiseLinearFunction

## Overview
The `PiecewiseLinearFunction` class is a representation of a membership function composed of several connected linear segments. This class is part of the `UnityFuzzy` namespace and is designed to approximate fuzzy sets using piecewise linear functions. It allows the definition of a function through a sequence of (X, Y) coordinates, enabling the creation of shapes that can be described by connected linear segments, such as trapezoidal functions.

## Variables

- **Point[] points**: An array of `Point` structures representing the (X, Y) coordinates for the start and end of each line segment in the piecewise function.

- **float LeftLimit**: A property that retrieves the leftmost X value of the membership function. It throws a `NullReferenceException` if the points array is not initialized or empty.

- **float RightLimit**: A property that retrieves the rightmost X value of the membership function. It also throws a `NullReferenceException` if the points array is not initialized or empty.

## Functions

- **PiecewiseLinearFunction(Point[] points)**: Constructor that initializes a new instance of the `PiecewiseLinearFunction` class. It requires an array of (X, Y) coordinates. The constructor validates that at least two points are provided, that Y values are within the range [0, 1], and that X values are in ascending order. It throws an `ArgumentException` if any of these conditions are not met.

- **float GetMembership(float x)**: This method calculates the degree of membership of a given value `x` to the piecewise function. It returns a float value representing the degree of membership in the range [0..1]. If `x` is less than the leftmost X value, it returns the Y value of the first point. If `x` falls between two points, it performs linear interpolation to determine the corresponding Y value. If `x` exceeds the rightmost X value, it returns the Y value of the last point. It throws a `NullReferenceException` if the points array is not initialized or empty.