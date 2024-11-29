# ClusterVisualizer

## Overview
The `ClusterVisualizer` script is designed to visualize clusters of points in a 3D space within the Unity game engine. It takes a collection of clusters represented as lists of 3D coordinates and renders them as spheres in the Unity editor using Gizmos. This visualization aids developers in debugging and understanding the spatial distribution of data points in their application.

## Variables

- **Clusters**: A public variable of type `List<List<Vector3>>` that holds the clusters of points to be visualized. Each cluster is represented as a list of `Vector3` objects, where each `Vector3` corresponds to a 3D point in space.

## Functions

- **OnDrawGizmos()**: This private method is called by Unity to draw Gizmos in the editor. It checks if the `Clusters` list is populated, and if so, it iterates through each cluster. For each cluster, it sets the Gizmo color and draws a small sphere at each point in the cluster. The colors are cycled through a predefined array of colors (red, green, blue, yellow, magenta).

- **UpdateClusters(List<List<float[]>> clusterData)**: This public method updates the `Clusters` variable with new data. It takes a parameter `clusterData`, which is a list of clusters where each cluster is represented as a list of float arrays. Each float array contains three values corresponding to the x, y, and z coordinates. The method converts these float arrays into `Vector3` objects and populates the `Clusters` list with these points.