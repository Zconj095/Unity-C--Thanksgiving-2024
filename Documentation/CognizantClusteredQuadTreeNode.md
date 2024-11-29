# CognizantClusteredQuadTreeNode

## Overview
The `CognizantClusteredQuadTreeNode` class implements a quad-tree data structure, which is a tree used to partition a two-dimensional space by recursively subdividing it into four quadrants or regions. This structure is particularly useful for spatial indexing, allowing for efficient querying of points within a specified rectangular area. The class provides methods to insert points into the tree and to query points that fall within a given range. This functionality is essential for applications such as collision detection, spatial searching, and geographic information systems.

## Variables
- **Bounds**: A `Rect` that defines the spatial boundary of the node. It represents the area that this node covers in the 2D space.
- **Points**: A `List<Vector2>` that holds the data points (represented as 2D vectors) contained within this node.
- **Children**: An array of `CognizantClusteredQuadTreeNode` that represents the four subdivisions of the current node. Initially set to `null` until the node is subdivided.
- **capacity**: An `int` that defines the maximum number of points that can be stored in this node before it needs to be subdivided into children.

## Functions
- **CognizantClusteredQuadTreeNode(Rect bounds, int capacity)**: Constructor that initializes a new instance of the `CognizantClusteredQuadTreeNode` class with specified bounds and capacity. It sets up the initial state of the node with an empty list of points and no children.

- **bool Insert(Vector2 point)**: Attempts to insert a new point into the quad-tree node. It first checks if the point is within the bounds of the node. If the node has not reached its capacity, the point is added to the list. If the capacity is reached, the node subdivides into children and attempts to insert the point into one of the child nodes.

- **private void Subdivide()**: Divides the current node into four child nodes, each representing a quadrant of the current node's bounds. This method calculates the dimensions and positions of the new child nodes based on the current node's bounds.

- **List<Vector2> Query(Rect range, List<Vector2> found = null)**: Searches for all points within a specified rectangular range. It returns a list of points that fall within the range. If the node's bounds do not overlap with the query range, the search is terminated early. The method recursively checks child nodes if they exist, collecting points that match the query criteria.