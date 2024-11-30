# TrainValDataSplit.cs

## Overview
The `TrainValDataSplit` class is designed to facilitate the partitioning of a dataset into training and validation subsets, which is a common practice in machine learning. This class is generic, allowing it to work with various data types for both input and output. It is a crucial part of the codebase as it provides the structure needed to manage and utilize different segments of data effectively during the training and validation phases of machine learning models.

## Variables

- **SplitIndex**: 
  - Type: `int`
  - Description: Represents the index of the split in relation to the original dataset, if applicable.

- **Training**: 
  - Type: `DataSubset4<TInput, TOutput>`
  - Description: Holds the training subset of data, encapsulating the input instances, output instances, associated weights, and their indices.

- **Validation**: 
  - Type: `DataSubset4<TInput, TOutput>`
  - Description: Holds the validation subset of data, similar to the training data, containing input instances, output instances, weights, and their indices.

## Functions

- **TrainValDataSplit()**: 
  - Description: Default constructor for the `TrainValDataSplit` class, initializing a new instance without any parameters.

- **TrainValDataSplit(int index, TInput[] inputs, TOutput[] outputs, double[] weights, int[] trainIndices, int[] validationIndices)**: 
  - Description: Overloaded constructor that initializes a new instance of the `TrainValDataSplit` class with specified parameters. It takes the split index, arrays of input and output instances, weights, and the indices for training and validation data. This constructor populates the `Training` and `Validation` properties by creating instances of the `DataSubset4` class.

### DataSubset4 Class

- **Index**: 
  - Type: `int`
  - Description: The index associated with this subset of data.

- **Inputs**: 
  - Type: `TInput[]`
  - Description: An array containing the input instances for this subset.

- **Outputs**: 
  - Type: `TOutput[]`
  - Description: An array containing the output instances corresponding to the input instances.

- **Weights**: 
  - Type: `double[]`
  - Description: An array of weights associated with the input instances, which can be used to influence the training process.

- **Indices**: 
  - Type: `int[]`
  - Description: An array of indices that indicates the position of the data in relation to the original dataset.

- **DataSubset4(int index, TInput[] inputs, TOutput[] outputs, double[] weights, int[] indices)**: 
  - Description: Constructor for the `DataSubset4` class that initializes a new instance using the provided index, input and output arrays, weights, and indices. This constructor sets up the properties of the class with the given data.