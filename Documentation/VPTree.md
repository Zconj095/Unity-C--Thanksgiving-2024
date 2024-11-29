# VPTree

## Overview
The `VPTree` class implements a Vantage-Point Tree, a data structure used for organizing points in a metric space to facilitate efficient nearest neighbor searches. This implementation is designed to work within the Unity game engine and allows developers to quickly find the closest points to a given target point based on a specified distance function. The `VPTree` class fits into the larger codebase by providing a means to optimize spatial queries, which can be crucial for performance in applications such as game development, simulations, and machine learning tasks.

## Variables
- `distanceFunction`: A `Func<TPoint, TPoint, double>` that defines how the distance between two points is calculated.
- `root`: A `VPTreeNode<TPoint>` representing the root node of the Vantage-Point Tree.

## Functions
- **Constructor: `VPTree(Func<TPoint, TPoint, double> distanceFunction)`**
  - Initializes a new instance of the `VPTree` class with the specified distance function. Throws an `ArgumentNullException` if the distance function is null.

- **Property: `Root`**
  - Gets the root node of the VPTree.

- **Static Method: `FromData(TPoint[] points, Func<TPoint, TPoint, double> distanceFunction, bool inPlace = false)`**
  - Creates a `VPTree` from an array of points. It builds the tree structure using the provided distance function. The `inPlace` parameter determines if the original array of points should be modified.

- **Private Method: `BuildFromPoints(TPoint[] points, int lower, int upper, bool inPlace)`**
  - Recursively builds the Vantage-Point Tree from the specified range of points. It selects a random vantage point, partitions the remaining points based on distance to the vantage point, and creates child nodes for the left and right subtrees.

- **Method: `Nearest(TPoint target, int count)`**
  - Finds the `count` nearest points to a specified target point. It uses a priority queue to efficiently track the closest points during the search.

- **Private Method: `Search(VPTreeNode<TPoint> node, TPoint target, int k, PriorityQueueV2<NodeDistanceV4<TPoint>> heap, ref double tau)`**
  - Recursively searches the Vantage-Point Tree to find the nearest points to the target. It updates the priority queue and threshold distance (`tau`) as it traverses the tree.

- **Private Method: `Swap(TPoint[] array, int i, int j)`**
  - Swaps two elements in the provided array. This is used when selecting the vantage point.

## Additional Classes
- **`VPTreeNode<TPoint>`**
  - Represents a node in the Vantage-Point Tree, containing:
    - `Position`: The point represented by this node.
    - `Threshold`: The distance threshold used for partitioning.
    - `Left`: The left child node.
    - `Right`: The right child node.

- **`NodeDistanceV4<TNode>`**
  - Represents a node and its associated distance. Contains:
    - `Node`: The point associated with this distance.
    - `Distance`: The distance from the target point.

- **`PriorityQueueV2<T>`**
  - A custom implementation of a priority queue that maintains a maximum size. It provides methods to enqueue, dequeue, and peek at the minimum element in the queue.