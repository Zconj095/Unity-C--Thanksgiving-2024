# MLKMeans

## Overview
The `MLKMeans` class implements the K-means clustering algorithm, which is a popular method for partitioning a dataset into a predefined number of clusters (K). This class is designed to fit into the EdgeLoreMachineLearning codebase, providing a straightforward way to group data points based on their similarities. It allows users to specify a distance function for measuring the similarity between data points and centroids, defaulting to the Euclidean distance if none is provided.

## Variables

- `K`: An integer representing the number of clusters to form. It is set during initialization and cannot be changed afterward.
- `Centroids`: A list of double arrays, where each array represents the coordinates of a centroid for a cluster.
- `Labels`: An integer array storing the cluster label assigned to each data point.
- `Tolerance`: A double value that determines the convergence criterion. The algorithm stops if centroids do not move more than this value between iterations. Default is set to `1e-5`.
- `MaxIterations`: An integer that limits the maximum number of iterations the algorithm will run. Default is set to `100`.
- `Error`: A double representing the average error of the clustering, calculated as the average distance of data points to their respective centroids.
- `DistanceFunction`: A function delegate that defines how the distance between two data points is calculated. It can be customized by the user.

## Functions

- **MLKMeans(int k, Func<double[], double[], double> distanceFunction = null)**: Constructor that initializes the K-means instance. It takes the number of clusters (`k`) and an optional distance function. Throws an exception if `k` is less than or equal to zero.

- **void Fit(double[][] data)**: Main method to train the K-means model on the provided dataset. It initializes centroids, assigns data points to clusters, updates centroids, and checks for convergence. Throws exceptions if the input data is invalid or if there are fewer data points than clusters.

- **private void AssignClusters(double[][] data)**: Assigns each data point to the nearest centroid, updating the `Labels` array with the cluster indices.

- **private int GetNearestCentroid(double[] point)**: Helper method that determines the nearest centroid for a given data point based on the specified distance function.

- **private List<double[]> UpdateCentroids(double[][] data)**: Calculates the new centroids based on the current cluster assignments. It computes the mean of all points assigned to each cluster. If a cluster is empty, it retains the previous centroid.

- **private bool HasConverged(List<double[]> oldCentroids, List<double[]> newCentroids)**: Checks if the centroids have converged by verifying if the distances between old and new centroids are within the specified tolerance.

- **private double CalculateError(double[][] data)**: Computes the average error of the clustering by calculating the distance from each data point to its assigned centroid.

- **private double EuclideanDistance(double[] pointA, double[] pointB)**: Computes the Euclidean distance between two points, used as the default distance function.