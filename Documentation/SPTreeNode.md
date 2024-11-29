# SPTreeNode

## Overview
The `SPTreeNode` class represents a node in a spatial partitioning tree (SPTree), which is used for efficiently organizing and querying spatial data. Each node can either store a point or subdivide its region into child nodes. This class is essential for managing the hierarchical structure of the tree, allowing for operations such as adding points and computing non-edge forces.

## Variables
- `owner`: An instance of `SPTree` that owns this node.
- `cumulativeSize`: An integer that keeps track of the number of points added to this node and its children.
- `region`: An instance of `SPCell` representing the spatial region that this node covers.
- `centerOfMass`: A double array that stores the center of mass of the points in this node.
- `buffer`: A double array used for intermediate calculations, particularly when computing forces.

### Public Properties
- `Point`: A double array representing the point stored in this node, if any.
- `Children`: An array of `SPTreeNode` representing the child nodes of this node.
- `Parent`: A reference to the parent `SPTreeNode` of this node.
- `Index`: An integer representing the index of this node among its siblings.
- `IsEmpty`: A boolean indicating whether this node currently contains any points.
- `IsLeaf`: A boolean indicating whether this node is a leaf node (i.e., it has no children).

## Functions
- `SPTreeNode(SPTree owner, SPTreeNode parent, int index, double[] corner, double[] width)`: Constructor that initializes a new instance of `SPTreeNode`, setting its owner, parent, index, region, center of mass, and buffer.

- `bool Add(double[] point)`: Adds a point to the node. If the point falls within the node's region, it updates the cumulative size and the center of mass. If the node is a leaf and empty, it stores the point. If it is a leaf but already has a point, it subdivides and attempts to add the point to one of its children.

- `void ComputeNonEdgeForces(double[] point, double theta, double[] negForces, ref double sumQ)`: Computes non-edge forces exerted on a given point based on the positions of the points stored in the tree. It updates the `negForces` array and the `sumQ` variable based on the calculated forces.

- `private void Subdivide()`: Splits the current node into child nodes, redistributing any existing point to the appropriate child node.

- `private bool IsEqual(double[] a, double[] b, double epsilon = 1e-10)`: Compares two double arrays for equality within a specified tolerance.

- `public override string ToString()`: Returns a string representation of the node, indicating whether it is empty, a leaf, or has children and their properties.

- `private string VectorToString(double[] vector)`: Converts a double array into a string format for easier visualization.