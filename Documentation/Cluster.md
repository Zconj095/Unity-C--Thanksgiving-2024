# Cluster Class Documentation

## Overview
The `Cluster` class serves as a base class for creating and managing data clusters within a Unity application. It is designed to work in conjunction with a collection of clusters, allowing for the organization and manipulation of data in a structured way. The class provides essential functionality for initializing clusters, accessing their properties, and managing their proportions within a collection.

## Variables

- `owner`: This variable holds a reference to the collection that contains this cluster, ensuring that each cluster knows its parent.
- `index`: This integer represents the position of the cluster within its collection, allowing for easy identification and access.

## Functions

### Public Functions

- `TCollection Owner`: 
  - **Description**: Retrieves the collection to which this cluster belongs.

- `int Index`: 
  - **Description**: Retrieves the index of this cluster within its collection.

- `float Proportion`: 
  - **Description**: Retrieves the proportion of samples contained in this cluster by calling the `GetProportion` method on the owner collection.

- `void Initialize(TCollection owner, int index)`:
  - **Description**: Initializes the cluster with its owner collection and index, setting up the cluster for use.

### Nested Class: ClusterCollection

#### Variables

- `owner`: Reference to the owning collection of clusters.
- `proportions`: A list that stores the proportions of samples for each cluster.
- `clusters`: A list that holds the actual cluster instances.

#### Public Functions

- `ClusterCollection(TCollection owner, int clusterCount)`:
  - **Description**: Constructor that initializes a new instance of the `ClusterCollection` class with a specified owner and number of clusters. It creates and initializes each cluster in the collection.

- `List<float> Proportions`:
  - **Description**: Retrieves the list of proportions for each cluster in the collection.

- `List<TCluster> Clusters`:
  - **Description**: Retrieves the list of clusters contained in the collection.

- `int Count`:
  - **Description**: Retrieves the total number of clusters in the collection.

- `TCluster GetCluster(int index)`:
  - **Description**: Retrieves the cluster at the specified index from the collection.

- `void SetProportion(int index, float proportion)`:
  - **Description**: Updates the proportion value for a specific cluster based on its index.

- `float GetProportion(int index)`:
  - **Description**: Retrieves the proportion of samples for a specific cluster based on its index.

- `IEnumerator<TCluster> GetEnumerator()`:
  - **Description**: Provides an enumerator for iterating through the clusters in the collection.

- `IEnumerator IEnumerable.GetEnumerator()`:
  - **Description**: Provides a non-generic enumerator for iterating through the clusters in the collection.

## Interface: IClusterCollection

### Overview
The `IClusterCollection` interface defines the essential methods and properties that any cluster collection must implement. This interface ensures that collections of clusters can be managed consistently across different implementations.

### Functions

- `int Count`: 
  - **Description**: Property to get the number of clusters in the collection.

- `List<float> Proportions`: 
  - **Description**: Property to get the list of proportions for each cluster.

- `TCluster GetCluster(int index)`:
  - **Description**: Method to retrieve a cluster at a specified index.

- `void SetProportion(int index, float proportion)`:
  - **Description**: Method to set the proportion of a specific cluster.

- `float GetProportion(int index)`:
  - **Description**: Method to retrieve the proportion of a specific cluster.

- `IEnumerator<TCluster> GetEnumerator()`:
  - **Description**: Method to get an enumerator for iterating through the clusters.

This documentation provides a clear understanding of the `Cluster` class and its associated components, making it easier for developers to work with and extend the functionality of the codebase.