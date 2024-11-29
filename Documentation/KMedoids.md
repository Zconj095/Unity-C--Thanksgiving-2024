# KMedoids Class

## Overview
The `KMedoids` class implements the K-Medoids clustering algorithm, which is a partitioning method used to group a set of data points into clusters. Unlike K-Means, K-Medoids uses actual data points as cluster centers (medoids), making it more robust to noise and outliers. This class works generically with any type of data, as specified by the generic type parameter `T`. It is designed to fit seamlessly into the `EdgeLoreMachineLearning` codebase, providing a method for unsupervised learning through clustering.

## Variables
- `_clusters`: A list of lists that holds the data points assigned to each cluster.
- `_distanceFunction`: A function that calculates the distance between two data points. If not provided, a default distance function is used.
- `_maxIterations`: The maximum number of iterations to run the algorithm before stopping.
- `_tolerance`: The threshold for determining convergence based on changes in cost.
- `_iterations`: A counter for the number of iterations completed during the fitting process.
- `_medoids`: A list that contains the current medoids for each cluster.

## Functions
- **KMedoids(int k, Func<T, T, double> distanceFunction = null, int maxIterations = 100, double tolerance = 1e-5)**: 
  Constructor that initializes the K-Medoids instance with the specified number of clusters, distance function, maximum iterations, and tolerance level.

- **List<T> Medoids**: 
  Property that returns the current list of medoids.

- **int Iterations**: 
  Property that returns the number of iterations completed during the fitting process.

- **void Fit(List<T> data)**: 
  Main function to fit the K-Medoids model to the provided data. It initializes medoids, assigns clusters, computes costs, and updates medoids iteratively until convergence or until the maximum number of iterations is reached.

- **private void InitializeMedoids(List<T> data)**: 
  Randomly selects initial medoids from the data points.

- **private void AssignClusters(List<T> data)**: 
  Clears existing clusters and assigns each data point to the nearest medoid.

- **private int FindNearestMedoid(T point)**: 
  Determines the index of the nearest medoid to a given data point based on the distance function.

- **private double ComputeTotalCost(List<T> data)**: 
  Computes the total cost of the current clustering by summing the distances from each point to its corresponding medoid.

- **private void UpdateMedoids()**: 
  Updates the medoids for each cluster by finding the point that minimizes the total cost within the cluster.

- **private double ComputeClusterCost(T candidate, List<T> cluster)**: 
  Calculates the total cost of using a candidate as a medoid for a given cluster.

- **private static double DefaultDistanceFunction(T a, T b)**: 
  A static method that provides a default distance function for numerical types. Throws an exception if the type is not supported. 

## Inheritance
- **KMedoids**: 
  A non-generic version of the KMedoids class specifically for handling `double` data types. It inherits from `KMedoids<double>` and provides a simplified interface for users who only need to work with numerical data.