# DataSubset Classes

## Overview
The `DataSubset` classes are designed to represent subsets of a larger dataset within the EdgeLoreMachineLearning codebase. These classes facilitate the management of input and output data samples, along with their associated weights and indices. The primary function of these classes is to provide a structured way to handle portions of datasets that can be used for various machine learning tasks, such as training and evaluation. The use of generics allows for flexibility in the types of data that can be handled, making these classes adaptable to different datasets.

## Variables

### DataSubset<TInput>
- **Inputs**: An array of type `TInput` that stores the input data samples in the dataset.
- **Weights**: An array of type `double` that contains the weights associated with each input sample.
- **Indices**: An array of type `int` that holds the indices of the samples in relation to the original dataset.
- **Proportion**: A `double` that represents the size of this subset as a proportion of the original dataset.

### DataSubset<TInput, TOutput>
- **Outputs**: An array of type `TOutput` that contains the output data samples corresponding to the inputs in the dataset.

## Functions

### DataSubset<TInput>
- **DataSubset()**: A parameterless constructor that initializes a new instance of the `DataSubset<TInput>` class.
- **DataSubset(int subsetSize, int totalSize)**: A constructor that initializes a new instance of the `DataSubset<TInput>` class with a specified subset size and total dataset size. It calculates the proportion of the subset relative to the total dataset and initializes the `Inputs`, `Weights`, and `Indices` arrays accordingly.

### DataSubset<TInput, TOutput>
- **DataSubset()**: A parameterless constructor that initializes a new instance of the `DataSubset<TInput, TOutput>` class, inheriting from the base `DataSubset<TInput>`.
- **DataSubset(int subsetSize, int totalSize)**: A constructor that initializes a new instance of the `DataSubset<TInput, TOutput>` class with a specified subset size and total dataset size. It calls the base constructor to set up the input-related properties and initializes the `Outputs` array for the output data.