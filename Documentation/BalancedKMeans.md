# BalancedKMeans

## Overview
The `BalancedKMeans` class implements a balanced version of the K-Means clustering algorithm, which aims to partition a dataset into K clusters while ensuring that each cluster has a roughly equal number of data points. This class utilizes the Hungarian algorithm to assign data points to clusters based on the calculated distances from the centroids. It is designed to fit within a machine learning codebase, focusing on clustering tasks where balance among clusters is crucial.

## Variables
- `K`: An integer representing the number of clusters to form. This value must be greater than zero.
- `Labels`: An array of integers that stores the cluster label assigned to each data point after fitting the model.
- `MaxIterations`: An integer that specifies the maximum number of iterations to run during the fitting process. Default is set to 100.
- `Tolerance`: A double that defines the convergence threshold for the centroids. Default is set to 1e-3.
- `Centroids`: A list of double arrays representing the centroids of each cluster after fitting the model.
- `distanceFunction`: A function that calculates the distance between two data points. It can be customized or defaults to the Euclidean distance.

## Functions
- `BalancedKMeans(int k, Func<double[], double[], double> distance = null)`: Constructor that initializes the K-Means instance with the specified number of clusters and an optional distance function. Throws an exception if `k` is less than or equal to zero.

- `void Fit(double[][] data)`: Main method to fit the K-Means model to the provided dataset. It initializes centroids, assigns clusters, recalculates centroids, and checks for convergence until the maximum number of iterations is reached or convergence is achieved.

- `private void AssignClusters(double[][] data)`: Assigns data points to clusters based on the calculated cost matrix using the Hungarian algorithm.

- `private double[,] CalculateCostMatrix(double[][] data)`: Computes a cost matrix that represents the distances between each data point and each centroid.

- `private List<double[]> RecalculateCentroids(double[][] data)`: Calculates the new centroids by averaging the data points assigned to each cluster. Throws an exception if any cluster ends up empty.

- `private bool CheckConvergence(List<double[]> oldCentroids, List<double[]> newCentroids)`: Checks if the centroids have converged by comparing the old centroids to the new centroids based on the defined tolerance.

- `private double DefaultDistance(double[] pointA, double[] pointB)`: A default method that calculates the Euclidean distance between two points.

## HungarianAlgorithm

### Overview
The `HungarianAlgorithm` class implements the Hungarian algorithm to solve the assignment problem, which is used to find the optimal assignment of data points to clusters based on the cost matrix.

### Variables
- `costMatrix`: A two-dimensional array representing the cost of assigning each data point to each cluster.
- `n`: An integer representing the number of data points.
- `m`: An integer representing the number of clusters.
- `rowCover`: An array of booleans indicating whether each row in the cost matrix is covered.
- `colCover`: An array of booleans indicating whether each column in the cost matrix is covered.
- `mask`: A two-dimensional array used to mark the assignments in the cost matrix.

### Functions
- `HungarianAlgorithm(double[,] costMatrix)`: Constructor that initializes the algorithm with the given cost matrix.

- `int[] Solve()`: Main method that executes the Hungarian algorithm steps to find the optimal assignments and returns an array of assignments.

- `private void Step1()`: Reduces the cost matrix by subtracting the minimum value from each row.

- `private void Step2()`: Marks zero values in the cost matrix to create initial assignments while covering the corresponding rows and columns.

- `private int Step3()`: Checks if all rows are covered. Returns 0 if all rows are covered, indicating that the solution is complete; otherwise, it returns 1 to indicate further processing is needed.