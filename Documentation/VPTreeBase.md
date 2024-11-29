# VPTreeBase

## Overview
The `VPTreeBase` class is a generic implementation of a vantage-point tree (VP-tree), which is a data structure used for organizing points in a metric space. Its primary function is to efficiently find the nearest neighbors of a given point based on a specified distance function. This class fits into the broader codebase by providing a foundational structure for spatial data representation and querying, particularly in applications related to machine learning and data analysis.

## Variables
- **tau**: A double that represents the current maximum distance threshold for the nearest neighbors search.
- **distanceFunction**: A function that calculates the distance between two points of type `TPoint`. This function is used to evaluate the proximity of points in the VP-tree.
- **Root**: An instance of type `TNode` that serves as the root node of the VP-tree.

## Functions
- **VPTreeBase(Func<TPoint, TPoint, double> distanceFunction)**: Constructor that initializes the VP-tree with a specified distance function. It throws an `ArgumentNullException` if the provided function is null.

- **List<NodeDistance<TNode>> Nearest(TPoint position, int neighbors)**: Overloaded method that finds the nearest neighbors to a specified position. It returns a list of `NodeDistance<TNode>` objects containing the nearest nodes and their distances.

- **List<NodeDistance<TNode>> Nearest(TPoint position, int neighbors, List<NodeDistance<TNode>> results)**: This method performs the nearest neighbor search and populates the provided results list with the nearest nodes. It uses a priority queue to manage and sort the nodes based on their distances.

- **internal TNode BuildFromPoints(TPoint[] items, int lower, int upper, bool inPlace)**: This method constructs the VP-tree from an array of points by recursively partitioning the points based on the median. It can operate in place or create a clone of the original array.

- **private void Search(TNode node, TPoint target, int k, PriorityQueue<NodeDistance<TNode>> heap)**: A private method that performs the search for the nearest neighbors recursively. It updates the priority queue with nodes that are closer than the current threshold distance (`tau`).

- **private void Swap(TPoint[] array, int i, int j)**: A utility method that swaps two elements in the provided array. This is used during the construction of the VP-tree to randomize the order of points.

## Related Classes
- **VPTreeNodeBase<TPoint, TNode>**: An abstract class that represents a node in the VP-tree, containing properties for the position of the point, a threshold for distance comparison, and references to left and right child nodes.

- **NodeDistance<TNode>**: A class that encapsulates a node and its distance from a target point, facilitating the storage of nearest neighbor results.

- **PriorityQueue<T>**: A custom implementation of a priority queue that maintains a maximum size and allows for efficient insertion and removal of the minimum element based on a provided comparer.