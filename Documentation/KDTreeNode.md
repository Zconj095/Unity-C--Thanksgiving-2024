# KDTreeNodeV2 Script Documentation

## Overview
The `KDTreeNodeV2` script defines a K-dimensional tree (KD-Tree) structure for use in Unity. KD-Trees are useful for organizing points in a K-dimensional space, facilitating efficient range queries and nearest neighbor searches. This script includes a base class for KD-Tree nodes, as well as a generic version that allows for storing any type of value at each node. The structure is designed to be extensible and can be utilized for various applications in game development and machine learning.

## Variables

### KDTreeNodeV2<T>
- **Value**: `T` - The generic value stored in this node of the KD-Tree.

### KDTreeNodeBaseV2<TNode>
- **Position**: `double[]` - An array representing the spatial coordinates of the node in K-dimensional space.
- **Axis**: `int` - The index of the dimension along which the node splits the space. This corresponds to the index in the `Position` array.
- **Left**: `TNode` - A reference to the left child node in the KD-Tree.
- **Right**: `TNode` - A reference to the right child node in the KD-Tree.

## Functions

### KDTreeNodeV2<T>
- **ToString()**: Overrides the default string representation to provide a human-readable format of the node's position for debugging purposes.

### KDTreeNodeBaseV2<TNode>
- **ToString()**: Converts the node's position into a string format, allowing for easy readability in logs and debugging.
- **CompareTo(TNode other)**: Compares the current node with another node based on the value at the splitting axis. It uses Reflection to access the properties of the nodes for comparison.
- **Equals(TNode other)**: Determines if the current node is equal to another node by checking both references and comparing their positions using Unity-compatible methods. 

This documentation provides a clear understanding of how the KD-Tree nodes are structured and how they function within the codebase, making it easier for developers to utilize and extend this functionality in their projects.