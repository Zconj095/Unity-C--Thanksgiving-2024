# SimpleClustering

## Overview
The `SimpleClustering` class is designed to perform basic clustering operations using a list of centroids represented as `Vector3` points in a 3D space. This class is part of a larger codebase that likely focuses on machine learning or data analysis, specifically within the context of clustering algorithms. The primary function of this script is to compute clusters from a given set of points and manage the associated centroids. It provides an interface to retrieve these clusters and their properties.

## Variables
- **centroids**: A private list of `Vector3` objects that represent the centroids of the clusters. This list is initialized when an instance of the `SimpleClustering` class is created.

## Functions
- **SimpleClustering()**: Constructor for the `SimpleClustering` class. It initializes the `centroids` list to store the centroids of the clusters.

- **Clusters**: A property that returns a new instance of `SimpleClusterCollection`, which implements the `IClusterCollection` interface. It allows access to the centroids and their associated proportions.

- **Compute(Vector3[] points)**: This method takes an array of `Vector3` points as input. It adds these points to the `centroids` list and creates an array of labels corresponding to each point. Currently, it assigns each point a unique label based on its index.

### Nested Class
- **SimpleClusterCollection**: A private nested class that implements the `EdgeLoreMachineLearning.IClusterCollection<Vector3, Vector3>` interface. This class manages the centroids and their proportions, allowing for cluster classification and retrieval.

  - **SimpleClusterCollection(List<Vector3> centroids)**: Constructor that initializes the `SimpleClusterCollection` with the provided centroids and creates a list of default proportions.

  - **Count**: A property that returns the number of centroids in the collection.

  - **Classify(Vector3 dataPoint)**: Method that currently serves as a placeholder for classifying a given data point. It always returns 0.

  - **GetCluster(int index)**: Retrieves the centroid at the specified index. Throws an `IndexOutOfRangeException` if the index is invalid.

  - **GetProportion(int clusterIndex)**: Returns the proportion associated with the specified cluster index. Throws an `IndexOutOfRangeException` if the index is invalid.

  - **SetProportion(int clusterIndex, float proportion)**: Sets the proportion for the specified cluster index. Throws an `IndexOutOfRangeException` if the index is invalid.

  - **Proportions**: A property that returns the list of proportions for the clusters.

  - **GetEnumerator()**: Returns an enumerator that iterates through the centroids.

  - **IEnumerable.GetEnumerator()**: Explicit interface implementation that returns the enumerator for the centroids.