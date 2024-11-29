# VoronoiIteration

## Overview
The `VoronoiIteration` class implements the K-means clustering algorithm using Voronoi diagrams. It is designed to partition a set of data points into `k` clusters based on a specified distance function. The class iteratively updates the centroids of the clusters until convergence is achieved or the maximum number of iterations is reached. This class is part of the `EdgeLoreMachineLearning` namespace, which suggests that it is intended for machine learning applications, particularly those involving clustering techniques.

## Variables
- `_centroids`: A private list that stores the current centroids of the clusters. Each centroid is represented as an array of type `T`.
- `_distanceFunction`: A function that calculates the distance between two points. It takes two arrays of type `T` and returns a double representing the distance.
- `_maxIterations`: An integer that specifies the maximum number of iterations to perform during the clustering process.
- `_tolerance`: A double that defines the convergence tolerance level. The algorithm stops if the centroids do not change significantly between iterations.
- `_k`: An integer that represents the number of clusters to form.

## Functions
- `VoronoiIteration(int k, Func<T[], T[], double> distanceFunction)`: Constructor that initializes the Voronoi iteration with a specified number of clusters (`k`) and a distance function. It throws an exception if `k` is less than or equal to zero.

- `List<T[]> Centroids`: A public property that provides access to the current centroids of the clusters.

- `void Fit(T[][] data)`: Main method that executes the clustering algorithm. It initializes the centroids, assigns data points to clusters, and updates the centroids iteratively.

- `private void InitializeCentroids(T[][] data)`: Initializes the centroids by randomly selecting `k` data points from the provided dataset.

- `private List<List<T[]>> AssignClusters(T[][] data)`: Assigns each data point to the nearest centroid, creating a list of clusters.

- `private bool UpdateCentroids(List<List<T[]>> clusters)`: Updates the centroids based on the current clusters. Returns a boolean indicating whether any centroids have changed.

- `private int FindNearestCentroid(T[] point)`: Finds the index of the nearest centroid to a given data point based on the distance function.

- `private T[] ComputeMedoid(List<T[]> cluster)`: Computes the medoid of a cluster, which is the point within the cluster that minimizes the total distance to all other points in the cluster.

- `private static double DefaultDistanceFunction(T[] a, T[] b)`: A static method that provides a default distance calculation for numerical types. It computes the Euclidean distance between two points represented as arrays.

- `public class VoronoiIteration : VoronoiIteration<double>`: A specialized version of the `VoronoiIteration` class that uses `double` as the data type. It includes a specific default distance function for double arrays.

- `private static double DefaultDistanceFunction(double[] a, double[] b)`: A static method that calculates the Euclidean distance between two double arrays. It is used in the specialized `VoronoiIteration` class.