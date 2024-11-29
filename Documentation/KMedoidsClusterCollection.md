# KMedoidsClusterCollection

## Overview
The `KMedoidsClusterCollection` class implements the K-Medoids clustering algorithm, which is a method used in machine learning to partition a set of data points into clusters. This class is generic, allowing it to work with any type that can be represented as an array. It provides functionality to initialize clusters, assign data points to the nearest centroids, compute the distortion of the clusters, and update the centroids based on the assigned points. This class is a part of the `EdgeLoreMachineLearning` namespace and is essential for organizing data into meaningful groups based on similarity.

## Variables
- **_centroids**: A private list of arrays that hold the current centroid points for each cluster.
- **_distanceFunction**: A private function that calculates the distance between two data points. It can be customized by the user or defaults to a standard distance function.
- **_clusters**: A private list of lists, where each inner list contains the data points assigned to each cluster.

## Functions
- **KMedoidsClusterCollection(int k, Func<T[], T[], double> distanceFunction)**: Constructor that initializes a new instance of the `KMedoidsClusterCollection` class. It sets the number of clusters (`k`) and defines the distance function used to compute distances between points.
  
- **List<T[]> Centroids**: Public property that returns the current centroids of the clusters.

- **List<List<T[]>> Clusters**: Public property that returns the clusters, each represented as a list of data points.

- **int Count**: Public property that returns the number of centroids (and thus clusters) currently maintained.

- **void Randomize(IEnumerable<T[]> data)**: Randomly selects initial centroid points from the provided dataset.

- **void AssignClusters(IEnumerable<T[]> data)**: Assigns each data point in the dataset to the nearest centroid, effectively forming clusters.

- **double ComputeDistortion()**: Calculates the total distortion of the clusters. Distortion is the sum of distances between each point and its corresponding centroid.

- **void UpdateCentroids()**: Updates the centroids of the clusters based on the current assignments of data points.

- **private int FindNearestCentroid(T[] point)**: Determines the index of the nearest centroid to a given data point by calculating distances.

- **private T[] ComputeCentroid(List<T[]> cluster)**: Computes the new centroid for a given cluster by averaging the points within that cluster. This method is currently implemented for numerical types only.

- **private static double DefaultDistanceFunction(T[] a, T[] b)**: A static method that calculates the Euclidean distance between two points. This method is also limited to numerical types unless a custom distance function is provided.