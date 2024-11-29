# MeanShiftClusterCollection

## Overview
The `MeanShiftClusterCollection` class is designed to manage and analyze clusters of data points in a 3D space using the mean shift clustering algorithm. It provides functionality to assign data points to clusters based on their proximity to cluster centers (modes), calculate the proportions of points in each cluster, and transform data points into distances to their nearest clusters. This class fits within the `EdgeLoreMachineLearning` namespace, indicating its purpose in machine learning applications, particularly in the context of clustering.

## Variables

- **modes**: A `List<Vector3>` that holds the centers of the clusters (modes) identified during the clustering process.
- **clusters**: A `Dictionary<int, Vector3>` that maps cluster indices to their corresponding cluster centers. This helps in keeping track of which points belong to which clusters.
- **proportions**: A `List<float>` that stores the proportion of data points assigned to each cluster, calculated after clustering.

## Functions

- **MeanShiftClusterCollection(List<Vector3> modes, List<Vector3> data)**: Constructor that initializes a new instance of the `MeanShiftClusterCollection` class. It takes a list of cluster centers (modes) and the original data points, and it calls the `AssignClusters` method to categorize the data points into clusters.

- **List<Vector3> Modes**: A public property that returns the list of cluster centers (modes) for external access.

- **int Count**: A public property that returns the number of clusters in the collection by counting the modes.

- **List<float> Proportions**: A public property that returns the list of proportions of points in each cluster, allowing external access to this computed data.

- **private void AssignClusters(List<Vector3> data)**: A private method that assigns data points to the nearest clusters based on their proximity to the modes. It calculates how many points belong to each cluster and updates the `clusters` and `proportions` variables accordingly.

- **List<float> Transform(List<Vector3> data)**: A public method that transforms the input data points into a list of distances to their nearest cluster centers. It returns this list of distances, which can be useful for analyzing the distribution of points relative to the clusters.

- **float Distortion(List<Vector3> data)**: A public method that calculates the average distance (distortion) from each data point to its nearest cluster center. This metric can help evaluate the quality of the clustering by indicating how closely the points are grouped around their respective centers.