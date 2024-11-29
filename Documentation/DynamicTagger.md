# DynamicTagger

## Overview
The `DynamicTagger` class is designed to manage and categorize data embeddings using clustering techniques. It allows users to add data points, cluster them into groups, and generate corresponding tags for each cluster. This functionality is particularly useful in scenarios where data needs to be organized based on similarities, such as in machine learning applications or data visualization tasks. The class serves as a component within a larger Unity-based codebase, enabling dynamic tagging of data based on clustering algorithms.

## Variables

- `clusterCount`: 
  - **Type**: `int`
  - **Description**: This variable stores the number of clusters that the data will be divided into. It is initialized through the constructor.

- `dataEmbeddings`: 
  - **Type**: `List<float[]>`
  - **Description**: This list holds the data embeddings (arrays of floats) that will be clustered. It is populated through the `AddData` method.

## Functions

- `DynamicTagger(int clusterCount)`: 
  - **Description**: Constructor for the `DynamicTagger` class. It initializes the `clusterCount` variable and creates an empty list for `dataEmbeddings`.

- `void AddData(float[] embedding)`: 
  - **Description**: This method allows users to add a new data embedding (an array of floats) to the `dataEmbeddings` list.

- `Dictionary<int, List<float[]>> ClusterData()`: 
  - **Description**: This method is intended to implement a clustering algorithm (such as K-Means or DBSCAN) to group the data embeddings. It returns a dictionary where the keys are cluster IDs and the values are lists of data points that belong to each cluster. Currently, it contains a placeholder implementation.

- `List<string> GenerateTags(Dictionary<int, List<float[]>> clusters)`: 
  - **Description**: This method generates tag names for each cluster based on the provided clusters dictionary. It currently creates simple tags in the format "Cluster-{ID}" for each cluster and returns a list of these tags.