# SPTree

## Overview
The `SPTree` class implements a space-partitioning tree structure, which is commonly used in computational geometry and machine learning applications. This tree efficiently organizes multi-dimensional points in space, enabling quick access and manipulation. The `SPTree` serves as a crucial component in the `EdgeLoreMachineLearning` codebase, facilitating operations such as inserting points and computing forces based on spatial relationships.

## Variables
- `dimension` (int): Represents the number of dimensions in the space that the tree covers. It is set during the initialization of the `SPTree` instance.
- `Root` (SPTreeNode): The root node of the space-partitioning tree. This node serves as the starting point for all tree operations, such as adding points and computing forces.

## Functions
- `SPTree(int dimensions)`: Constructor that initializes a new instance of the `SPTree` class with a specified number of dimensions. It throws an exception if the provided dimensions are less than or equal to zero.

- `static SPTree FromData(double[][] points)`: Static method that creates a new `SPTree` instance populated with the provided data points. It computes the mean, minimum, and maximum values for each dimension to set up the tree structure. It throws an exception if the input points are null or empty.

- `bool Add(double[] point)`: Method that inserts a new point into the space-partitioning tree. It checks if the dimensions of the point match the tree's dimensions and returns true if the point is successfully added, or false otherwise.

- `void ComputeNonEdgeForces(double[] point, double theta, double[] neg_f, ref double sum_Q)`: Method that computes non-edge forces using the Barnes-Hut algorithm for a given point. It requires the point's dimensions to match the tree's dimensions and updates the output parameters for negative forces and the sum of Q-values.

- `void ComputeEdgeForces(double[][] points, int[] row_P, int[] col_P, double[] val_P, double[][] pos_f)`: Method that computes edge forces based on a sparse matrix representation of points in space. It checks that the input points are valid and updates the output parameter for positive forces based on the computed values.