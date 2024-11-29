# DecisionBranchNodeCollection

## Overview

The `DecisionBranchNodeCollection` class is a specialized collection designed to manage decision nodes within a decision tree, specifically in the context of machine learning applications using Unity. This class facilitates the organization of nodes that represent branches in a decision-making process, where each branch corresponds to an attribute that needs to be evaluated against its children nodes. The collection not only stores these decision nodes but also provides functionality to access the attribute index and the corresponding attribute being evaluated.

## Variables

- **owner**: A private variable of type `DecisionNode` that represents the decision node that contains this collection. It helps maintain a reference to the parent node of the collection.

- **AttributeIndex**: An integer property that gets or sets the index of the attribute being used in the current stage of the decision-making process.

- **Attribute**: A read-only property that retrieves the `DecisionVariable` associated with the current `AttributeIndex`. It throws an exception if the attribute cannot be resolved due to invalid index or configuration issues.

- **Owner**: A property that gets or sets the `DecisionNode` that contains this collection, allowing for easy access to the parent node.

## Functions

- **DecisionBranchNodeCollection(DecisionNode owner)**: Constructor that initializes a new instance of the `DecisionBranchNodeCollection` class with a specified owner node. It throws an `ArgumentNullException` if the provided owner is null.

- **DecisionBranchNodeCollection(int attributeIndex, IEnumerable<DecisionNode> nodes)**: Constructor that initializes a new instance of the `DecisionBranchNodeCollection` with a specified attribute index and a collection of child nodes. It throws exceptions if the nodes collection is null or empty.

- **AddRange(IEnumerable<DecisionNode> children)**: Method that adds a collection of child nodes to the end of the current collection. It throws an `ArgumentNullException` if the provided collection is null.

- **ToString()**: Overrides the default `ToString()` method to return a string representation of the collection, including the attribute and its index, along with the string representations of the child nodes. This provides a clear and concise summary of the collection's contents.