# RansacPlane

## Overview
The `RansacPlane` class is designed to implement the RANSAC (Random Sample Consensus) algorithm for estimating a plane from a set of 3D points in the EdgeLoreMachineLearning namespace. This class dynamically interacts with a generic RANSAC class to perform plane fitting, distance calculations, and degeneracy checks. It serves as a crucial component for 3D geometric modeling and analysis, allowing users to identify and fit planes to spatial data efficiently.

## Variables
- `ransacInstance`: An instance of a dynamically loaded RANSAC class that handles the core algorithm.
- `inliers`: An array of integers representing the indices of points that are considered inliers for the estimated plane.
- `points`: An array of `Vector3` objects that holds the input 3D points for plane estimation.
- `distances`: An array of floats that stores the distances from each point to the estimated plane.
- `defineMethod`: A `MethodInfo` object for the method that defines the plane from a set of indices.
- `distanceMethod`: A `MethodInfo` object for the method that calculates distances from the plane to the points.
- `degenerateMethod`: A `MethodInfo` object for the method that checks if a set of points is degenerate (collinear).

## Functions
- **Constructor: `RansacPlane(float threshold, float probability)`**
  - Initializes a new instance of the `RansacPlane` class. It dynamically creates an instance of a generic RANSAC class with specified threshold and probability values, and assigns methods for fitting, distance calculations, and degeneracy checks.

- **`Plane Estimate(IEnumerable<Vector3> inputPoints)`**
  - Estimates a plane from a collection of 3D points. It checks if there are enough points, invokes the RANSAC algorithm to compute inliers, and returns a fitted plane based on the inliers.

- **`Plane Define(int[] indices)`**
  - Defines a plane using three indices from the points array. It throws an exception if the number of indices is not three and returns a `Plane` object based on the specified points.

- **`int[] Distance(Plane plane, float threshold)`**
  - Calculates the distances from the specified plane to all input points and returns an array of indices for points that are within a given threshold distance from the plane.

- **`bool Degenerate(int[] indices)`**
  - Checks if three points are collinear (degenerate) by calculating the cross product of the vectors formed by the points. It throws an exception if the number of indices is not three and returns a boolean indicating whether the points are degenerate.

- **`Plane Fit(Vector3[] selectedPoints)`**
  - Fits a plane to a set of selected points using the least-squares method. It calculates the normal vector and offset of the plane and returns a `Plane` object. If only three points are provided, it directly constructs the plane from those points.