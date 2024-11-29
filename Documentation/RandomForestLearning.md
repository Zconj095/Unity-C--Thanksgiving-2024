# RandomForestLearning

## Overview
The `RandomForestLearning` class is designed to implement a Random Forest learning algorithm within the EdgeLoreMachineLearning namespace. It serves as a component for training a Random Forest model using input data and corresponding output labels. This class provides methods to learn from both integer and double input arrays and manages the training of multiple decision trees that comprise the Random Forest. It is a crucial part of the machine learning codebase, enabling the system to make predictions based on learned patterns from the data.

## Variables
- `forest`: An instance of the `RandomForest` class that represents the ensemble of decision trees created during the learning process.
- `attributes`: A list of `DecisionVariable` objects that represent the features or attributes used for training the Random Forest.
- `NumberOfTrees`: An integer property that defines the number of trees to be created in the Random Forest. Default value is 100.
- `SampleRatio`: A double property that indicates the proportion of the dataset to be used for training each tree. Default value is 0.632.
- `CoverageRatio`: A double property that specifies the proportion of variables to consider when splitting nodes in each tree. Default value is 1.0.
- `Join`: An integer property that determines the number of samples to join when training each tree. Default value is 100.

## Functions
- `RandomForestLearning()`: Constructor that initializes a new instance of the `RandomForestLearning` class without any attributes.
  
- `RandomForestLearning(List<DecisionVariable> attributes)`: Constructor that initializes a new instance of the `RandomForestLearning` class with a specified list of decision variables.

- `Learn(int[][] inputs, int[] outputs)`: Method that trains the Random Forest using integer input arrays and corresponding output labels. It generates attributes from the data if they are not already provided.

- `Learn(double[][] inputs, int[] outputs)`: Method that trains the Random Forest using double input arrays and corresponding output labels. Similar to the previous method, it generates attributes if necessary.

- `TrainForest<T>(T[][] inputs, int[] outputs)`: Private method that iterates through the number of trees specified and trains each tree using the provided inputs and outputs.

- `TrainTree<T>(T[][] inputs, int[] outputs, DecisionTree tree)`: Private method that samples data indices and trains a specific decision tree using the sampled inputs and outputs.

- `GetVariablesPerTree(int totalVariables)`: Private method that calculates the number of variables to consider for each tree based on the total number of variables and the `CoverageRatio`.

- `GenerateSampleIndices(int dataSize)`: Private method that generates a list of random sample indices based on the `SampleRatio`, which determines how many samples to take from the dataset.

- `GenerateAttributesFromData<T>(T[][] inputs)`: Private method that generates a list of `DecisionVariable` objects based on the input data, creating a feature for each column in the dataset.