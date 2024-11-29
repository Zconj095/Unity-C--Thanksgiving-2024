# CognizantCortexReaderKMeansClustering

## Overview
The `CognizantCortexReaderKMeansClustering` class implements the K-Means clustering algorithm, which is a popular method for partitioning a set of data points into a specified number of clusters. This script allows users to add data points, initialize centroids randomly from those points, and perform clustering to assign each data point to the nearest centroid. The clustering results can be used in various applications, such as pattern recognition, data analysis, and machine learning within the codebase.

## Variables

- **ClusterCount**: An integer that represents the number of clusters to form. This value is set during the instantiation of the class and cannot be modified afterward.
- **dataPoints**: A list of float arrays where each array represents a data point in multidimensional space.
- **centroids**: A list of float arrays representing the centroids of each cluster. These centroids are updated during the clustering process.

## Functions

- **CognizantCortexReaderKMeansClustering(int clusterCount)**: Constructor that initializes the clustering object with a specified number of clusters and prepares the data structures for data points and centroids.

- **AddDataPoint(float[] point)**: Method that adds a new data point to the `dataPoints` list. Each point is represented as an array of floats.

- **InitializeCentroids()**: Method that clears any existing centroids and randomly selects new centroids from the available data points based on the specified `ClusterCount`.

- **PerformClustering(int maxIterations = 10)**: Method that executes the K-Means clustering algorithm. It assigns each data point to the nearest centroid and updates the centroids based on the mean of the assigned points. The process is repeated for a maximum number of iterations (default is 10). It returns a list of integers where each integer represents the cluster assignment for the corresponding data point.

- **ComputeDistance(float[] a, float[] b)**: Private helper method that calculates the Euclidean distance between two data points (represented as float arrays). This distance is used to determine the nearest centroid for each data point during the clustering process.