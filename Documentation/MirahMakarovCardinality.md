# MirahMakarovCardinality

## Overview
The `MirahMakarovCardinality` script is designed to perform clustering of 3D data points within the Unity game engine. It utilizes a k-means clustering algorithm to group the data points into a specified number of clusters, defined by the `clusterCount` variable. The script initializes centroids for each cluster, assigns data points to the nearest centroid, and updates the centroids iteratively to refine the clusters. This functionality is essential for organizing and analyzing spatial data in a structured manner within the codebase.

## Variables
- `public int clusterCount`: The number of clusters to create. Default is set to 3.
- `public List<Vector3> dataPoints`: A list of 3D points that will be clustered. 
- `private List<Vector3> centroids`: A list that stores the current centroid positions for each cluster.
- `private List<List<Vector3>> clusters`: A list of lists, where each inner list contains the data points assigned to a specific cluster.
- `private float[] radii`: An array that holds the average distances from the points in each cluster to their respective centroid.

## Functions
- `void Start()`: This is the Unity lifecycle method that initializes the clustering process by calling the `InitializeClusters` and `PerformClustering` functions when the script starts.

- `void InitializeClusters()`: This function initializes the centroids and clusters. It randomly selects initial centroid positions from the `dataPoints` and creates empty lists for each cluster.

- `void PerformClustering()`: This function runs the clustering algorithm for a fixed number of iterations (10 in this case). It calls the `AssignPointsToClusters` and `UpdateCentroidsAndRadii` functions in each iteration to refine the clusters.

- `void AssignPointsToClusters()`: This function assigns each data point to the nearest centroid. It first clears existing clusters and then iterates through each data point to determine the closest centroid based on Euclidean distance.

- `void UpdateCentroidsAndRadii()`: This function recalculates the centroids and the average radii for each cluster based on the points assigned to them. It computes the new centroid as the average position of all points in the cluster and updates the radii to reflect the average distance of points from the centroid.