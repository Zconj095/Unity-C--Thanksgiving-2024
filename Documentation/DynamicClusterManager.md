# DynamicClusterManager

## Overview
The `DynamicClusterManager` is a Unity script that manages the clustering of term embeddings based on temperature-controlled granularity. It utilizes a K-Means clustering algorithm to dynamically group terms as new embeddings are added. The temperature control influences the number of clusters, allowing for adaptive clustering that responds to changes in the underlying data. This functionality is essential for applications that require real-time data processing and organization, such as natural language processing or machine learning tasks within the Unity environment.

## Variables

- `tempControl`: An instance of the `TemperatureControl` class, which is responsible for managing the temperature that affects clustering granularity.
  
- `kMeans`: An instance of the `KMeans` class, which implements the K-Means clustering algorithm used for grouping the term embeddings.
  
- `termEmbeddings`: A list of float arrays (`List<float[]>`), where each array represents a term embedding. This list stores all the embeddings that will be clustered.

## Functions

- `AddTerm(float[] embedding)`: This method adds a new term embedding to the `termEmbeddings` list. After adding the embedding, it recalculates the number of clusters based on the current temperature from `tempControl`. It then performs clustering on the term embeddings using the K-Means algorithm and logs the number of clusters formed.