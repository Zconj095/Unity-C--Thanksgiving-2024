# EdgeLoreMachineLearning Clustering Interfaces

## Overview
This script defines a set of interfaces for clustering algorithms in the EdgeLoreMachineLearning namespace. These interfaces provide a common structure for collections of clusters, allowing for operations on clusters and their centroids. The interfaces are designed to be implemented by various clustering algorithms, such as KMeans and MeanShift, facilitating the handling of clusters and their associated data.

## Variables

- **TData**: A generic type parameter representing the type of data being clustered.
- **TCluster**: A generic type parameter representing the type of cluster.
- **TCentroids**: A generic type parameter representing the type of centroid, which may differ from the clustered data type.

## Functions

### IClusterCollectionEx<TData, TCluster>
- **int Count**: Property that returns the number of clusters in the collection.
- **TCluster[] Clusters**: Property that returns the collection of clusters currently modeled by the clustering algorithm.
- **double[] Proportions**: Property that returns the proportion of samples in each cluster.
- **TCluster this[int index]**: Indexer that retrieves the cluster at the specified index, which should also be the class label of the cluster.

### ICentroidClusterCollection<TData, TCentroids, TCluster>
- **TCentroids[] Centroids**: Property that gets or sets the clusters' centroids.
- **Func<TData, TCentroids, float> Distance**: Property that gets or sets the distance function used to measure the distance between a point and the cluster centroid.
- **float Distortion(TData[] data, int[] labels = null, float[] weights = null)**: Method that calculates the average square distance from the data points to the nearest clusters' centroids.
- **float[][] Transform(TData[] points, float[] weights = null, float[][] result = null)**: Method that transforms data points into feature vectors containing the distance between each point and each of the clusters.
- **float[] Transform(TData[] points, int[] labels, float[] weights = null, float[] result = null)**: Overloaded method that transforms data points into feature vectors containing the distance between each point and each of the clusters, with optional labels.

### ICentroidClusterCollection<TData, TCluster>
- This interface inherits from `ICentroidClusterCollection<TData, TData, TCluster>` and does not introduce additional methods or properties. It is used for clusters with centroids of the same data type as the clustered data.