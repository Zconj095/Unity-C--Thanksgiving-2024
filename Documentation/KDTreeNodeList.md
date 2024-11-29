# KDTreeNodeList.cs

## Overview
The `KDTreeNodeList` script is part of the `EdgeLoreMachineLearning` namespace and provides a specialized list structure for managing nodes and their distances in k-dimensional trees. This is particularly useful for search algorithms that operate on k-dimensional data, allowing for efficient storage and retrieval of neighbor nodes based on their distance to a specified query point. The script enhances the functionality of Unity's list by adding type constraints and specific behaviors tailored to k-dimensional tree operations.

## Variables

### KDTreeNodeList<T>
- **T**: A generic type parameter that represents the type of the value stored in the nodes. It must implement the `IComparable<T>` and `IEquatable<T>` interfaces to ensure that the stored values can be compared and checked for equality.

### NodeDistanceV2<T>
- **Node**: This property holds the node associated with the distance. It represents the actual data point in the k-dimensional space.
- **Distance**: This property stores the distance from the query point to the node, represented as a `double`.

## Functions

### KDTreeNodeList<T> Constructors
- **KDTreeNodeList()**: Initializes a new instance of the `KDTreeNodeList<T>` class with an empty list.
- **KDTreeNodeList(int capacity)**: Initializes a new instance of the `KDTreeNodeList<T>` class with a specified initial capacity.

### NodeDistanceV2<T> Constructor
- **NodeDistanceV2(T node, double distance)**: Initializes a new instance of the `NodeDistanceV2<T>` class with the specified node and its distance from a query point.

### NodeDistanceV2<T> Methods
- **ToString()**: Returns a string representation of the node-distance pair in the format of "Node at distance Distance", providing a human-readable description of the node and its distance from the query point. 

This documentation serves as a guide for developers to understand the purpose, structure, and functionality of the `KDTreeNodeList` and `NodeDistanceV2` classes within the context of k-dimensional tree algorithms in Unity.