# RansacLine

## Overview
The `RansacLine` class is designed to estimate a line from a set of 2D points using the RANSAC (Random Sample Consensus) algorithm. This class dynamically interacts with a generic RANSAC implementation, allowing for the fitting of lines based on input points. It is part of the `EdgeLoreMachineLearning` namespace, which likely encompasses various machine learning algorithms and utilities related to edge detection or line fitting.

## Variables
- `ransacInstance`: An object representing a dynamically created instance of a generic RANSAC class used for line fitting.
- `inliers`: An array of integers that stores the indices of the inlier points that fit the estimated line.
- `points`: An array of `Vector2` structures that holds the input points used for line estimation.
- `distances`: An array of floats that contains the distances from each point to the estimated line.

- `defineMethod`: A `MethodInfo` object that represents the method used to define the line based on selected indices.
- `distanceMethod`: A `MethodInfo` object that represents the method used to calculate distances from the line to the points.
- `degenerateMethod`: A `MethodInfo` object that represents the method used to check if the selected points are degenerate (i.e., they do not form a valid line).

## Functions
- **RansacLine(float threshold, float probability)**: Constructor that initializes the RANSAC instance for line fitting. It dynamically creates an instance of a generic RANSAC class and assigns custom methods for defining lines, calculating distances, and checking degeneracy.

- **Line Estimate(IEnumerable<Vector2> inputPoints)**: Estimates a line from the provided input points. It checks if there are at least two points, invokes the RANSAC computation, retrieves inliers, and fits a line based on these inliers.

- **Line DefineLine(int[] indices)**: Defines a line using two indices from the `points` array. It throws an exception if the provided indices do not correspond to exactly two points.

- **int[] Distance(Line line, float threshold)**: Calculates distances from the specified line to all points and returns an array of indices of points that are within the given threshold distance from the line.

- **bool Degenerate(int[] indices)**: Checks if the two points specified by the indices are degenerate (i.e., they are the same point). It throws an exception if the provided indices do not correspond to exactly two points.

- **Line Fit(Vector2[] selectedPoints)**: Fits a line using the least-squares method based on the selected points. It calculates the slope and intercept of the line and returns a `Line` object representing the fitted line.

## Line Struct
- **Line**: A struct that represents a line in 2D space defined by its slope and intercept.

  - **float Slope**: The slope of the line.
  - **float Intercept**: The y-intercept of the line.

  - **Line(float slope, float intercept)**: Constructor that initializes the Line with a specified slope and intercept.

  - **static Line FromPoints(Vector2 p1, Vector2 p2)**: Static method that creates a `Line` object from two points.

  - **static Line FromSlopeIntercept(float slope, float intercept)**: Static method that creates a `Line` object using the slope and intercept.

  - **float DistanceToPoint(Vector2 point)**: Calculates the distance from a specified point to the line using the formula for point-to-line distance.