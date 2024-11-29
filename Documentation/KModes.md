# KModes Class Documentation

## Overview
The `KModes` class implements the K-Modes algorithm, which is specifically designed for clustering categorical data. This algorithm groups data points into a specified number of clusters based on their similarity, using a distance function to measure how close data points are to each other. The class is generic, allowing it to work with various types of data, while also providing a specialized implementation for integer arrays.

## Variables
- **_k**: An integer representing the number of clusters to form.
- **_distanceFunction**: A function that takes two arrays of type `T` and returns a double representing the distance between them.
- **_maxIterations**: An integer that sets the maximum number of iterations the algorithm will run to prevent infinite loops.
- **_tolerance**: A double that defines the tolerance level for determining if centroids have changed significantly.
- **_centroids**: A list of arrays of type `T` that holds the current centroids of the clusters.

## Functions
- **KModes(int k, Func<T[], T[], double> distanceFunction)**: Constructor that initializes a new instance of the KModes class. It takes the number of clusters `k` and a custom distance function. If no distance function is provided, a default distance function is used.

- **List<T[]> Centroids**: A property that returns the current centroids of the clusters.

- **void Fit(T[][] data)**: Main function that fits the K-Modes model to the provided data. It initializes centroids, assigns clusters, and updates centroids iteratively until convergence or until the maximum number of iterations is reached.

- **private void InitializeCentroids(T[][] data)**: Initializes the centroids by randomly selecting `k` data points from the input data.

- **private List<List<T[]>> AssignClusters(T[][] data)**: Assigns each data point to the nearest centroid, creating a list of clusters.

- **private bool UpdateCentroids(List<List<T[]>> clusters)**: Updates the centroids based on the current clusters and returns whether any centroids have changed.

- **private int FindNearestCentroid(T[] point)**: Finds the index of the nearest centroid to a given data point using the distance function.

- **private T[] ComputeMode(List<T[]> cluster)**: Computes the mode (most frequent value) for each dimension of the given cluster and returns it as a new centroid.

- **private T FindMode(T[] values)**: Helper function that determines the mode of an array of values.

- **private static double DefaultDistanceFunction(T[] a, T[] b)**: A static method that calculates the default distance between two arrays of type `T`. It counts the number of differing elements and returns that count as the distance.

## Specialized KModes Class
- **public class KModes : KModes<int>**: A specialized version of the `KModes` class that specifically works with integer arrays.

- **public KModes(int k)**: Constructor for the specialized KModes class that initializes the instance with a default distance function for integer arrays.

- **private static double DefaultDistanceFunction(int[] a, int[] b)**: A static method that calculates the distance between two integer arrays by counting the number of differing elements.