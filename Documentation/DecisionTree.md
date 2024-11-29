# DecisionTree Script Documentation

## Overview
The `DecisionTree` class is designed for use in Unity-based projects, providing a structure for implementing decision tree algorithms. This script allows for the creation and manipulation of a decision tree, which can be used for classification tasks. The tree is built using a collection of decision nodes, each representing a decision point based on certain attributes. The `DecisionTree` class fits within the broader EdgeLoreMachineLearning namespace, which likely contains other machine learning utilities and algorithms.

## Variables
- **root**: An instance of `DecisionNode` that serves as the starting point for the decision tree. It represents the top-most node in the tree structure.
- **attributes**: A list of `DecisionVariable` objects that define the attributes used for decision-making within the tree.
- **NumberOfInputs**: An integer representing the total number of input attributes in the decision tree.
- **NumberOfOutputs**: An integer indicating the number of outputs produced by the decision tree (assumed to be 1 for classification).
- **NumberOfClasses**: An integer that indicates the number of distinct classes that can be predicted by the decision tree.

## Functions
- **Root**: A property that gets or sets the root node of the decision tree.
- **Attributes**: A property that retrieves the collection of attributes processed by the decision tree.
- **DecisionTree(List<DecisionVariable> attributes, int numberOfClasses = 2)**: Constructor that initializes a new instance of the `DecisionTree` class with a specified list of attributes and the number of output classes. Throws an exception if the attributes list is null or empty.
- **GetAttributeName(int index)**: Retrieves the name of the attribute at the specified index. Throws an exception if the index is out of range.
- **Decide(double[] input)**: Predicts the class for a single input array using the decision tree. Throws an exception if the tree has no root node.
- **Decide(double[] input, DecisionNode subtree)**: Predicts the class for a given input starting from a specified subtree. This method traverses the tree based on the input values until it reaches a leaf node.
- **Decide(double[][] inputs)**: Predicts the classes for multiple input arrays, returning an array of predicted classes. Throws an exception if the inputs array is null or empty.
- **GetEnumerator()**: Implements pre-order traversal of the decision tree, allowing for enumeration of all decision nodes in the tree.
- **IEnumerator IEnumerable.GetEnumerator()**: Provides a non-generic enumerator for the decision tree, calling the generic `GetEnumerator()` method.

## Additional Class
- **Range**: A simple class that represents a range with a minimum and maximum value. It includes properties `Min` and `Max`, and a constructor to initialize these values. This class may be used in conjunction with decision variables to define valid ranges for inputs.