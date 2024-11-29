# ID3Learning Script Documentation

## Overview
The `ID3Learning` class implements the ID3 algorithm for training decision trees within the Unity framework. It facilitates the creation and training of a decision tree by utilizing a dataset of inputs and corresponding outputs. This class is designed to work with the `DecisionTreeV2` class and its associated nodes, enabling the development of machine learning models that can classify data based on learned attributes.

## Variables

- `tree`: An instance of `DecisionTreeV2`, representing the decision tree that will be trained using the ID3 algorithm.
- `attributeUsageCount`: An array of integers that keeps track of how many times each attribute (input feature) has been used for splitting nodes in the decision tree.
- `Rejection`: A boolean property that indicates whether rejection of certain classifications is allowed during the learning process. It defaults to `true`.

## Functions

- **ID3Learning(DecisionTreeV2 tree)**: 
  - Constructor that initializes a new instance of the ID3 learning algorithm. It takes a `DecisionTreeV2` object as a parameter and initializes the `attributeUsageCount` based on the number of inputs in the tree.

- **void InitializeTreeAttributes(DecisionTreeV2 tree)**: 
  - Private method that initializes the `attributeUsageCount` array to have the same length as the number of inputs in the decision tree.

- **DecisionTreeV2 Learn(int[][] inputs, int[] outputs)**: 
  - Public method that trains the decision tree using the ID3 algorithm. It accepts a 2D array of inputs and a 1D array of outputs (labels) and returns the trained decision tree. It also resets the attribute usage count before beginning the learning process.

- **void ResetAttributeUsage()**: 
  - Private method that resets the `attributeUsageCount` array to zero for all attributes.

- **void SplitNode(DecisionNodeV2 node, int[][] inputs, int[] outputs, int depth)**: 
  - Private recursive method that splits a node based on the best attribute. It checks for termination conditions (such as all outputs being the same or reaching maximum depth) and partitions data accordingly to create child nodes.

- **int SelectBestAttribute(int[][] inputs, int[] outputs)**: 
  - Private method that selects the attribute that provides the highest information gain for splitting the dataset. It iterates through each attribute and calculates its information gain, returning the index of the best attribute.

- **double CalculateInformationGain(int[][] inputs, int[] outputs, int attributeIndex)**: 
  - Private method that calculates the information gain of a specific attribute by comparing the entropy of the dataset before and after partitioning based on that attribute.

- **double CalculateEntropy(int[] outputs)**: 
  - Private method that calculates the entropy of the output labels. It computes the probability distribution of the labels and uses it to determine the entropy.

- **Dictionary<int, (int[][] Inputs, int[] Outputs)> PartitionData(int[][] inputs, int[] outputs, int attributeIndex)**: 
  - Private method that partitions the input data and corresponding outputs based on a specified attribute. It groups the data by the attribute values and returns a dictionary where each key is an attribute value and the value is a tuple containing the corresponding inputs and outputs.

- **int GetMostCommonLabel(int[] outputs)**: 
  - Private method that determines the most common label in the output array. It groups the outputs by their values and returns the label that occurs most frequently.

This documentation provides a comprehensive overview of the `ID3Learning` class, its variables, and its functions, facilitating easier understanding and usage for developers working with decision trees in Unity.