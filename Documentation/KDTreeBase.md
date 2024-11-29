# KDTreeBase<TNode> Class Documentation

## Overview
The `KDTreeBase<TNode>` class implements a K-D tree, a data structure used for organizing points in a multi-dimensional space. This class allows for efficient nearest neighbor searches and supports adding new nodes to the tree. It is generic and can be used with any type that inherits from `KDTreeNodeBase<TNode>`. The K-D tree is particularly useful in machine learning and computer graphics for spatial partitioning.

## Variables
- `count`: An integer that keeps track of the total number of nodes in the K-D tree.
- `dimensions`: An integer representing the number of dimensions in the K-D tree.
- `leaves`: An integer that counts the number of leaf nodes in the tree.
- `distanceFunction`: A function that calculates the distance between two points. It defaults to the `EuclideanDistance` method.
- `Root`: The root node of the K-D tree, which is of type `TNode`.

## Properties
- `Dimensions`: Gets the number of dimensions for the K-D tree.
- `DistanceFunction`: Gets or sets the function used to calculate the distance between points. Throws an `ArgumentNullException` if set to null.
- `Count`: Gets the total number of nodes in the K-D tree.
- `Leaves`: Gets the total number of leaf nodes in the K-D tree.

## Constructor
- `KDTreeBase(int dimensions)`: Initializes a new instance of the K-D tree with a specified number of dimensions.
- `KDTreeBase(int dimensions, TNode root)`: Initializes a new instance of the K-D tree with a specified number of dimensions and a provided root node. It also calls `InitializeCounts` to set the count and leaves.

## Functions
- `void InitializeCounts()`: Initializes the `count` and `leaves` variables. It traverses the tree starting from the root node to update these values.
- `IEnumerable<TNode> TraverseTree(TNode node)`: Recursively traverses the K-D tree and yields each node. This is used by `InitializeCounts` to count the nodes and leaves.
- `TNode Nearest(double[] position, out double distance)`: Finds the nearest node to a given position in the K-D tree. It returns the nearest node and outputs the distance to that node.
- `void Nearest(TNode current, double[] position, ref TNode match, ref double minDistance)`: A recursive helper function that finds the nearest node to a given position by comparing distances and traversing the tree.
- `void AddNode(double[] position)`: Adds a new node with the specified position to the K-D tree, incrementing the count.
- `TNode Insert(TNode node, double[] position, int depth)`: A recursive helper function that inserts a new node into the K-D tree based on its position and the current depth in the tree.
- `void Clear()`: Clears the K-D tree by setting the root to null and resetting the count and leaves.
- `static double EuclideanDistance(double[] a, double[] b)`: A static method that calculates the Euclidean distance between two points represented by arrays. This is the default distance function used by the K-D tree. 

## KDTreeNodeBase<TNode> Class
This is an abstract class that serves as the base for nodes used in the K-D tree. It contains the following properties:
- `Axis`: An integer that indicates the axis along which the current node splits the space.
- `Position`: An array of doubles representing the position of the node in multi-dimensional space.
- `Left`: A reference to the left child node.
- `Right`: A reference to the right child node.
- `IsLeaf`: A boolean property that indicates whether the node is a leaf (i.e., it has no children). 

This documentation provides a clear understanding of the structure and functionality of the `KDTreeBase<TNode>` class and its associated components.