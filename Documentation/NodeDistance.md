# NodeDistanceV3

## Overview
The `NodeDistanceV3` struct represents a pair consisting of a node and its distance from a query point in a k-dimensional tree structure used in Unity. This struct is designed to facilitate comparisons and equality checks between node-distance pairs, making it useful for algorithms that require sorting or prioritizing nodes based on their distances. It fits within the broader context of machine learning and spatial data structures in the `EdgeLoreMachineLearning` namespace.

## Variables

- **Node**: 
  - Type: `TNode`
  - Description: Represents the node in the pair. This type is generic, allowing for flexibility in the kind of node that can be used.

- **Distance**: 
  - Type: `double`
  - Description: Represents the distance of the node from a specified query point. This is a numerical value that indicates how far the node is from the point of interest.

## Functions

- **NodeDistanceV3(TNode node, double distance)**: 
  - Description: Constructor that initializes a new instance of the `NodeDistanceV3` struct with the specified node and distance values.

- **bool Equals(object obj)**: 
  - Description: Overrides the default equality check to determine if the specified object is equal to the current instance of `NodeDistanceV3`.

- **bool Equals(NodeDistanceV3<TNode> other)**: 
  - Description: Checks if the current instance is equal to another `NodeDistanceV3` instance by comparing both the node and the distance values.

- **int GetHashCode()**: 
  - Description: Returns a hash code for the current instance, which is useful for storing instances in hash-based collections.

- **int CompareTo(NodeDistanceV3<TNode> other)**: 
  - Description: Compares the current instance with another `NodeDistanceV3` instance based on the distance value, facilitating sorting operations.

- **static bool operator ==(NodeDistanceV3<TNode> a, NodeDistanceV3<TNode> b)**: 
  - Description: Defines the equality operator to check if two `NodeDistanceV3` instances are equal.

- **static bool operator !=(NodeDistanceV3<TNode> a, NodeDistanceV3<TNode> b)**: 
  - Description: Defines the inequality operator to check if two `NodeDistanceV3` instances are not equal.

- **static bool operator <(NodeDistanceV3<TNode> a, NodeDistanceV3<TNode> b)**: 
  - Description: Defines the less-than operator to compare the distance of two `NodeDistanceV3` instances.

- **static bool operator >(NodeDistanceV3<TNode> a, NodeDistanceV3<TNode> b)**: 
  - Description: Defines the greater-than operator to compare the distance of two `NodeDistanceV3` instances.

- **string ToString()**: 
  - Description: Returns a string representation of the current instance, formatted as `<Node, Distance>`, providing a clear visualization of its contents.