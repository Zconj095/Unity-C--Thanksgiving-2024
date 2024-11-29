# RansacCircle

## Overview
The `RansacCircle` class is designed to implement the RANSAC (Random Sample Consensus) algorithm to fit a circle to a set of 2D points. It dynamically interacts with a generic RANSAC class to estimate the best-fitting circle based on input points while handling inliers and outliers efficiently. This class plays a crucial role in the EdgeLoreMachineLearning codebase by enabling robust circle fitting, which can be used in various machine learning applications, particularly in geometric data analysis.

## Variables
- `ransacInstance`: An instance of a dynamically loaded RANSAC class that performs the core fitting algorithm.
- `inliers`: An array of integers representing the indices of the points that are considered inliers for the fitted circle.
- `points`: An array of `Vector2` representing the input points used for circle fitting.
- `distances`: An array of floats storing the distances from the fitted circle to each input point.
- `defineMethod`: A `MethodInfo` object to invoke the method for defining a circle based on selected points.
- `distanceMethod`: A `MethodInfo` object to invoke the method for calculating distances from the circle to the points.
- `degenerateMethod`: A `MethodInfo` object to invoke the method for checking if selected points are collinear (degenerate case).

## Functions
- **RansacCircle(float threshold, float probability)**: Constructor that initializes the RANSAC instance with specified thresholds and probabilities, and sets up the dynamic method bindings for fitting, distance calculation, and degeneracy checks.

- **Circle Estimate(IEnumerable<Vector2> inputPoints)**: Estimates the best-fitting circle for the provided input points. It checks if there are at least three points, invokes the RANSAC compute method, and retrieves the inliers to fit the circle.

- **Circle Define(int[] indices)**: Defines a circle using the indices of three points. Throws an exception if the number of indices is not exactly three.

- **int[] Distance(Circle c, float threshold)**: Calculates the distances from the circle to each point and returns the indices of points that are within the specified threshold distance from the circle.

- **bool Degenerate(int[] indices)**: Checks if the three points defined by the provided indices are collinear. Throws an exception if the number of indices is not exactly three.

- **Circle Fit(Vector2[] selectedPoints)**: Fits a circle to the selected points using a least squares approach. It constructs matrices, performs matrix operations, and extracts the circle parameters (center and radius).

- **float[,] Transpose(float[,] matrix)**: Transposes a given matrix, switching its rows and columns.

- **float[,] Multiply(float[,] A, float[,] B)**: Multiplies two matrices and returns the resulting matrix. Throws an exception if the dimensions are incompatible.

- **float[] Multiply(float[,] A, float[] B)**: Multiplies a matrix with a vector and returns the resulting vector. Throws an exception if the dimensions are incompatible.

- **float[] Solve(float[,] A, float[] B)**: Solves a linear equation system represented by matrix A and vector B using LU decomposition, returning the solution vector.

## Circle Struct
- **Circle(Vector2 p1, Vector2 p2, Vector2 p3)**: Constructor that initializes a circle based on three points. Throws an exception if the points are collinear.
- **Circle(Vector2 center, float radius)**: Constructor that initializes a circle with a given center and radius.
- **float DistanceToPoint(Vector2 point)**: Calculates and returns the distance from the circle to a specified point.