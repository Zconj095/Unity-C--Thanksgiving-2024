# CentroidCluster

## Overview
The `CentroidCluster` class is a generic implementation of a clustering algorithm that organizes data into clusters based on centroids. It is designed to work with a collection of data points, allowing for various seeding strategies to initialize cluster centroids. This class is part of the `EdgeLoreMachineLearning` namespace and extends the `Cluster` class, providing additional functionality specific to centroid-based clustering.

## Variables

- **TCollection**: A type parameter representing a collection of clusters that implements the `IClusterCollection` interface.
- **TData**: A type parameter representing the data type of the elements being clustered.
- **TCluster**: A type parameter representing the type of clusters being created, which must be a subclass of `CentroidCluster`.
- **centroids**: An array of type `TData` that holds the current centroid points for the clusters.
- **clusters**: A list of `TCluster` instances representing the individual clusters created during the clustering process.
- **proportions**: A list of doubles that may represent the proportion of data points in each cluster.
- **owner**: A reference to the owner collection of type `TCollection`.
- **Distance**: A function delegate that defines how to calculate the distance between two `TData` points.

## Functions

- **ClusterCollection(TCollection owner, int k, Func<TData, TData, float> distance)**: Constructor that initializes a new instance of the `ClusterCollection`, setting the owner, the number of centroids `k`, and the distance function.

- **Count**: Property that returns the number of centroids in the cluster collection.

- **Centroids**: Property that gets or sets the centroids array. It throws an exception if the provided array is null or has a different length.

- **Clusters**: Property that returns an array of the current clusters.

- **Proportions**: Property that returns an array of the proportions of each cluster.

- **this[int index]**: Indexer that allows access to clusters by their index. It throws an exception if the index is out of range.

- **Randomize(TData[] points, Seeding strategy, ParallelOptions parallelOptions)**: Method that randomizes the centroids based on the specified seeding strategy. It throws an exception if the points array is null.

- **InitializeFixed(TData[] points)**: Private method that initializes centroids by selecting the first `k` points from the provided array.

- **InitializeUniform(TData[] points)**: Private method that initializes centroids by randomly selecting `k` points from the provided array.

- **InitializeKMeansPlusPlus(TData[] points)**: Private method intended for implementing the KMeans++ seeding strategy (details to be added).

- **InitializePamBuild(TData[] points, ParallelOptions parallelOptions)**: Private method intended for implementing the PAM build logic for clustering (details to be added).

- **Transform(TData[] points, float[] weights, float[][] transformationMatrix)**: Method that transforms the input points using a transformation matrix. Currently, it returns the transformation matrix as a placeholder.

- **Transform(TData[] points, int[] labels, float[] weights, float[] output)**: Overloaded method that transforms the input points based on labels and weights, returning the output as a placeholder.

- **Distortion(TData[] points, int[] labels, float[] weights)**: Method that calculates the total distortion of the clustering based on the provided points, labels, and weights.

- **GetEnumerator()**: Method that returns an enumerator for iterating through the clusters.

- **IEnumerable.GetEnumerator()**: Explicit interface implementation for the non-generic enumerator.