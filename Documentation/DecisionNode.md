# DecisionNode

## Overview
The `DecisionNode` class represents a node in a decision tree used for machine learning within a Unity environment. It serves as a fundamental building block for decision trees, allowing for the evaluation of conditions based on attributes. Each node can have branches leading to other decision nodes, forming a tree-like structure that facilitates decision-making processes. This class is part of the `EdgeLoreMachineLearning` namespace, which likely contains additional machine learning components.

## Variables
- **parent**: A private variable that holds a reference to the parent node of the current decision node.
- **AttributeIndex**: An integer that specifies the index of the attribute that this node tests against.
- **Value**: A nullable double that represents the value used in the comparison for this node.
- **Comparison**: An instance of `ComparisonKind`, which defines the type of comparison (e.g., equal, greater than) this node will perform.
- **Output**: A nullable integer that may represent the output class or decision made at this node.
- **Branches**: A list of `DecisionNode` objects that represent the child nodes stemming from this node.
- **Owner**: A reference to the `DecisionTree` that owns this node.
- **Parent**: A property that gets or sets the parent node of the current node.
- **IsRoot**: A boolean property that indicates whether this node is the root node of the tree.
- **IsLeaf**: A boolean property that indicates whether this node is a leaf node (i.e., has no children).

## Functions
- **DecisionNode(DecisionTree owner)**: Constructor that initializes a new instance of the `DecisionNode`, setting its owner and default values for comparison and attribute index.
  
- **bool Compute(double x)**: Evaluates whether the provided double value satisfies the condition defined by this node's comparison type and value.

- **bool Compute(int x)**: Overloaded method that allows evaluation of an integer value by converting it to a double and calling the double version of `Compute`.

- **void ClearBranches()**: Clears all branches (child nodes) from the current node, effectively resetting its children.

- **int GetHeight()**: Computes the height of the node from the root of the tree, returning the number of edges from the root to this node.

- **override string ToString()**: Returns a string representation of the node, including its attribute name, comparison operator, and value.

- **IEnumerator<DecisionNode> GetEnumerator()**: Implements an enumerator that allows for traversing the subtree of this node using a depth-first search approach.

- **IEnumerator IEnumerable.GetEnumerator()**: Explicit interface implementation for non-generic enumeration.

- **void SetProperty(string propertyName, object value)**: Dynamically sets a property of this node using reflection, allowing for flexible property assignment.

- **object GetProperty(string propertyName)**: Dynamically retrieves a property of this node using reflection, returning its value if accessible. If the property is not found or is write-only, it logs an error.

This documentation aims to provide clarity on the structure and functionality of the `DecisionNode` class, making it accessible to developers working with decision trees in machine learning applications.