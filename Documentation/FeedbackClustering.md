# FeedbackClustering

## Overview
The `FeedbackClustering` class provides methods for performing K-Means clustering on a set of embeddings and for adjusting those embeddings based on feedback. This class is intended to help organize and refine data points (embeddings) for further analysis or processing within a Unity project. It allows developers to group similar data points and modify them based on a specified learning rate, potentially improving the performance of machine learning algorithms or data analysis tasks.

## Variables
- **embeddings**: A `List<float[]>` representing the collection of embeddings (data points) to be clustered or adjusted. Each embedding is an array of floats.
- **k**: An `int` representing the number of clusters to create in the K-Means clustering process.
- **learningRate**: A `float` that determines the magnitude of the adjustment applied to each element of the embeddings during the feedback adjustment process.
- **clusters**: A `Dictionary<int, List<float[]>>` that stores the resulting clusters from the K-Means clustering, where each key is a cluster index and the value is a list of embeddings assigned to that cluster.
- **clusterIndex**: An `int` used to determine which cluster an embedding belongs to based on a simple modulus operation.

## Functions
- **KMeansClustering(List<float[]> embeddings, int k)**: This static method performs a basic K-Means clustering operation. It takes a list of embeddings and the number of desired clusters (k) as input. The method initializes the clusters and assigns each embedding to a cluster based on a simple logic that uses the sum of the embedding values. It returns a dictionary of clusters, where each key corresponds to a cluster index and the value is a list of embeddings in that cluster.

- **FeedbackAdjust(List<float[]> embeddings, float learningRate)**: This static method adjusts each embedding in the provided list based on the specified learning rate. For each element in each embedding, it applies a feedback mechanism that adds the product of the learning rate and the sine of the element's value. This adjustment can help fine-tune the embeddings for improved performance in subsequent processing or analysis.