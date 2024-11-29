# BinarySplit Class Documentation

## Overview
The `BinarySplit` class implements a binary clustering algorithm that splits data into K clusters using a method similar to k-means. It begins with all data points in a single cluster and iteratively splits the cluster with the highest distortion until K clusters are formed. This class is part of the `EdgeLoreMachineLearning` namespace, which likely contains additional machine learning algorithms and utilities.

## Variables
- **K**: (int) The number of clusters to form. Must be greater than zero.
- **Centroids**: (List<double[]>) A list of centroids for each cluster, representing the center of each cluster.
- **MaxIterations**: (int) The maximum number of iterations to perform during the fitting process. Default is set to 100.
- **Tolerance**: (double) The tolerance level for convergence. Default is set to 1e-3.
- **Distance**: (Func<double[], double[], double>) A function that calculates the distance between two data points. If not provided, a default Euclidean distance function is used.
- **ComputeProportions**: (bool) A flag indicating whether to compute proportions of clusters (not utilized in the current implementation).
- **clusters**: (List<double[][]>) A private list holding the current clusters of data points during the fitting process.
- **distortions**: (double[]) An array to store the distortion values for each cluster.

## Functions
- **BinarySplit(int k, Func<double[], double[], double> distance = null)**: Constructor that initializes the `BinarySplit` instance with the specified number of clusters (k) and an optional distance function. Throws an exception if k is less than or equal to zero.

- **void Fit(double[][] data)**: Fits the model to the provided data by performing the clustering process. It throws exceptions if the input data is null, empty, or if there are fewer data points than clusters.

- **(double[][], double[][]) SplitCluster(double[][] cluster)**: Splits a given cluster into two sub-clusters using a simple k-means algorithm with k=2. Returns a tuple containing the two resulting clusters.

- **double ComputeClusterDistortion(double[][] cluster, double[] centroid)**: Computes the distortion of a cluster based on the distance of its points from the centroid. Returns the total distortion value.

- **double[] ComputeCentroid(double[][] cluster)**: Calculates the centroid of a cluster by averaging the coordinates of all points in the cluster. Returns the centroid as an array of doubles.

- **double DefaultDistance(double[] pointA, double[] pointB)**: A default method for calculating the Euclidean distance between two points represented as arrays. Returns the computed distance.

---

# KMeans Class Documentation

## Overview
The `KMeans` class implements the k-means clustering algorithm, which partitions data into K clusters by minimizing the variance within each cluster. It randomly initializes centroids and iteratively refines them until convergence or the maximum number of iterations is reached. This class is also part of the `EdgeLoreMachineLearning` namespace.

## Variables
- **K**: (int) The number of clusters to form. Must be greater than zero.
- **Centroids**: (List<double[]>) A list of centroids for each cluster, representing the center of each cluster.
- **Labels**: (int[]) An array that holds the cluster label assigned to each data point after fitting.
- **MaxIterations**: (int) The maximum number of iterations to perform during the fitting process. Default is set to 100.
- **Distance**: (Func<double[], double[], double>) A function that calculates the distance between two data points. If not provided, a default Euclidean distance function is used.

## Functions
- **KMeans(int k, Func<double[], double[], double> distance = null)**: Constructor that initializes the `KMeans` instance with the specified number of clusters (k) and an optional distance function. Throws an exception if k is less than or equal to zero.

- **void Fit(double[][] data)**: Fits the model to the provided data by performing the k-means clustering process. It throws exceptions if the input data is null, empty, or if there are fewer data points than clusters.

- **int GetNearestCentroid(double[] point)**: Determines the nearest centroid to a given data point and returns the index of that centroid.

- **double[] ComputeCentroid(double[][] cluster)**: Calculates the centroid of a cluster by averaging the coordinates of all points in the cluster. Returns the centroid as an array of doubles.

- **bool HasConverged(List<double[]> oldCentroids, List<double[]> newCentroids)**: Checks if the centroids have converged by comparing old and new centroid positions. Returns true if they have converged, otherwise false.

- **double DefaultDistance(double[] pointA, double[] pointB)**: A default method for calculating the Euclidean distance between two points represented as arrays. Returns the computed distance.