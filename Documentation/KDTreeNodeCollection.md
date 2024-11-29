# KDTreeNodeCollection Documentation

## Overview
The `KDTreeNodeCollection<TNode>` class is a collection designed to manage nodes of a k-dimensional tree specifically within the Unity environment. It allows for efficient storage and retrieval of nodes along with their associated distances, ensuring that the collection maintains a sorted order based on these distances. This class is useful for applications in machine learning and spatial data management where quick access to nearest or farthest nodes is necessary.

## Variables
- **distances**: An array of `double` values that holds the distances associated with each node in the collection.
- **nodes**: An array of type `TNode` that stores the nodes of the k-dimensional tree.
- **count**: An integer that keeps track of the current number of elements in the collection.
- **Capacity**: An integer property that indicates the maximum number of elements that the collection can hold.

## Functions
- **KDTreeNodeCollection(int capacity)**: Constructor that initializes a new instance of the `KDTreeNodeCollection` class with a specified maximum capacity. Throws an exception if the capacity is less than or equal to zero.

- **bool Add(TNode node, double distance)**: Adds a new node and its associated distance to the collection. If the collection is at capacity, it removes the farthest node if the new distance is smaller. Returns true if the node was successfully added; otherwise, false.

- **void Add(NodeDistance<TNode> item)**: Adds a `NodeDistance` item (which contains a node and its distance) to the collection by calling the `Add` method.

- **void Clear()**: Clears all nodes and distances from the collection, resetting the count to zero.

- **bool Contains(NodeDistance<TNode> item)**: Checks if the collection contains a specific `NodeDistance` item. Returns true if found; otherwise, false.

- **void CopyTo(NodeDistance<TNode>[] array, int arrayIndex)**: Copies the elements of the collection into a specified array starting at a given index.

- **bool Remove(NodeDistance<TNode> item)**: Not supported in this implementation. Throws a `NotSupportedException` if called.

- **IEnumerator<NodeDistance<TNode>> GetEnumerator()**: Returns an enumerator that iterates through the collection.

- **TNode Nearest**: Property that retrieves the node with the smallest distance from the collection. Returns null if the collection is empty.

- **TNode Farthest**: Property that retrieves the node with the largest distance from the collection. Returns null if the collection is empty.

- **double MinimumDistance()**: Returns the smallest distance in the collection. Throws an exception if the collection is empty.

- **double MaximumDistance()**: Returns the largest distance in the collection. Throws an exception if the collection is empty.

- **private void AddNode(double distance, TNode node)**: A private method that adds a node and its distance to the collection while ensuring that the collection remains sorted.

- **private void RemoveFarthest()**: A private method that removes the farthest node from the collection by decrementing the count.

- **private void Swap(int indexA, int indexB)**: A private method that swaps two elements (distances and nodes) in the collection based on their indices.