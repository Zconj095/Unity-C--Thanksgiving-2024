# KMeansClusterCollection

## Overview
The `KMeansClusterCollection` class is designed to facilitate the implementation of the K-Means clustering algorithm. It manages a collection of clusters, each represented by a centroid, and provides functionality to compute various metrics related to clustering, such as distortion and transformation of data points. This class is integral to the machine learning module of the EdgeLore project, allowing users to cluster data points effectively based on specified distance metrics.

## Variables
- `clusters`: A `List<KMeansCluster>` that holds the clusters in the collection.
- `distanceFunction`: A `Func<double[], double[], double>` that defines the method for calculating the distance between data points and centroids. Defaults to Euclidean distance if not provided.
- `covariances`: A jagged array of type `double[][][]` that stores the covariance matrices for each cluster, which are calculated as needed.

## Properties
- `Centroids`: A property that gets or sets the centroids of the clusters. Throws an exception if the number of centroids does not match the number of clusters.
- `Proportions`: A read-only property that returns an array of proportions for each cluster.
- `Covariances`: A read-only property that returns the covariance matrices for the clusters.
- `Count`: A read-only property that returns the total number of clusters in the collection.
- `this[int index]`: An indexer that allows access to a specific cluster by its index.

## Functions
- `Distortion(double[][] data, int[] labels = null, double[] weights = null)`: Calculates the distortion of the clustering based on the provided data and optional labels and weights. If labels are not provided, it determines the nearest cluster for each data point.
  
- `Transform(double[][] data, double[] weights = null, double[][] result = null)`: Transforms the input data into a new format based on the distances to each cluster's centroid.

- `Randomize(double[][] points, Seeding strategy)`: Randomly initializes the centroids of the clusters based on the specified seeding strategy. Supports fixed, uniform, and KMeansPlusPlus strategies. Throws an exception for unimplemented strategies.

- `InitializeKMeansPlusPlus(double[][] points, Random random)`: A private method that implements the KMeans++ initialization strategy for selecting initial centroids based on distance probabilities.

- `FindNearestCluster(double[] point)`: A private method that finds and returns the index of the nearest cluster for a given data point based on the distance function.

- `GetEnumerator()`: Returns an enumerator that iterates through the collection of clusters.

## Nested Class
### KMeansCluster
- Represents an individual cluster within the `KMeansClusterCollection`.
- **Properties**:
  - `Owner`: The collection to which this cluster belongs.
  - `Index`: The index of the cluster in the collection.
  - `Centroid`: The current centroid of the cluster.
  - `Proportion`: The proportion of data points assigned to this cluster.
  - `Covariance`: A property that retrieves the covariance matrix for the cluster, throwing an exception if it hasn’t been computed.

## Enum
### Seeding
- An enumeration that defines the different strategies for initializing cluster centroids:
  - `Fixed`: No randomization, centroids are predefined.
  - `Uniform`: Centroids are selected uniformly from the input points.
  - `KMeansPlusPlus`: Centroids are selected using the KMeans++ initialization method.
  - `PamBuild`: A placeholder for a strategy that is not yet implemented.