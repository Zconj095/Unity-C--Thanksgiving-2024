# KModesClusterCollection

## Overview
The `KModesClusterCollection` class is designed to implement the KModes clustering algorithm, which is a variant of the K-means algorithm specifically tailored for categorical data. This class manages a collection of cluster centroids and provides methods to assign data points to the nearest centroid, calculate distortion, initialize centroids, and evaluate the association between data points and centroids. It fits into the broader codebase of machine learning algorithms, allowing for efficient clustering of categorical datasets.

## Variables

- **_centroids**: A private list of arrays that holds the centroids for each cluster. Each centroid represents the central point of a cluster in the dataset.
- **_distanceFunction**: A private function that calculates the distance between two data points (arrays). This function is used to determine how similar or different two points are, which is crucial for assigning points to clusters.

## Functions

- **KModesClusterCollection(int k, Func<T[], T[], double> distanceFunction)**: Constructor that initializes a new instance of the `KModesClusterCollection` class with a specified number of clusters (`k`) and a distance function for measuring distance between data points.

- **List<T[]> Centroids**: Property that gets the current list of cluster centroids or sets new centroids.

- **Func<T[], T[], double> DistanceFunction**: Property that gets the distance function used for measuring the distance between a data point and a cluster centroid.

- **int[] AssignClusters(T[][] data)**: Assigns each data point in the provided dataset to the nearest cluster centroid and returns an array of labels indicating the cluster each point belongs to.

- **double CalculateDistortion(T[][] data, int[] labels)**: Calculates the average distortion (or total distance) from the data points to their assigned cluster centroids based on the provided labels.

- **void InitializeCentroids(T[][] data, int k)**: Randomly selects `k` data points from the dataset to initialize the cluster centroids.

- **double Score(T[] input, int clusterIndex)**: Computes a score that indicates how well an input vector is associated with a specific cluster centroid, based on the negative distance to the centroid.

- **private int FindNearestCentroid(T[] point)**: A helper method that finds and returns the index of the nearest centroid for a given data point by calculating distances to all centroids.