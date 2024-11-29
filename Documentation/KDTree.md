# KDTree.cs

## Overview
The `KDTree` class implements a K-dimensional tree (KD-Tree) for storing and efficiently querying multi-dimensional data in Unity. This data structure is particularly useful for spatial partitioning and nearest neighbor search operations. The KD-Tree allows for the addition of nodes, as well as querying for the nearest neighbors within a specified radius or a fixed number of neighbors. It provides a way to organize points in a k-dimensional space, making it easier to perform operations such as range searches and nearest neighbor searches.

## Variables
- `dimensions`: An integer representing the number of dimensions for the KD-Tree. It must be greater than zero.
- `Root`: A property of type `KDTreeNode<T>` that represents the root node of the KD-Tree.

## Functions
- **KDTree(int dimensions)**: Constructor that initializes a new instance of the `KDTree` with the specified number of dimensions. It throws an exception if dimensions are less than or equal to zero.

- **KDTree(int dimensions, KDTreeNode<T> root)**: Constructor that initializes a new instance of the `KDTree` with the specified number of dimensions and a root node.

- **void Add(double[] position, T value)**: Adds a new node with the specified position and value to the KD-Tree. It throws an exception if the length of the position array does not match the tree's dimensions.

- **List<KDTreeNode<T>> Nearest(double[] position, double radius)**: Returns a list of nodes within a specified radius from a given position. It throws an exception if the length of the position array does not match the tree's dimensions.

- **List<KDTreeNode<T>> Nearest(double[] position, int neighbors)**: Returns a list of the nearest nodes to the specified position, limited to a certain number of neighbors. It throws an exception if the length of the position array does not match the tree's dimensions.

- **private KDTreeNode<T> AddNode(KDTreeNode<T> node, double[] position, T value, int depth)**: A recursive helper method that adds a new node to the KD-Tree based on the position and depth.

- **private void SearchNearestRadius(KDTreeNode<T> node, double[] target, double radiusSquared, List<KDTreeNode<T>> results)**: A recursive helper method that searches for nodes within a specified radius and adds them to the results list.

- **private void SearchNearestNeighbors(KDTreeNode<T> node, double[] target, PriorityQueue2<KDTreeNode<T>> results, int neighbors)**: A recursive helper method that searches for the nearest neighbors and maintains a priority queue of the closest nodes.

- **private double EuclideanDistanceSquared(double[] a, double[] b)**: Calculates and returns the squared Euclidean distance between two points in the k-dimensional space.

## KDTreeNode Class
- **double[] Position**: An array representing the position of the node in k-dimensional space.
- **T Value**: The value associated with the node.
- **int Axis**: An integer that indicates the axis along which the node is split.
- **KDTreeNode<T> Left**: A reference to the left child node.
- **KDTreeNode<T> Right**: A reference to the right child node.

## PriorityQueue2 Class
- **int maxSize**: An integer representing the maximum size of the priority queue.
- **void Enqueue(T item)**: Adds an item to the priority queue and removes the maximum item if the size exceeds `maxSize`.
- **T Dequeue()**: Removes and returns the minimum item from the priority queue.