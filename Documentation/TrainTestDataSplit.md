# TrainTestDataSplit.cs

## Overview
The `TrainTestDataSplit` class is designed to manage the partitioning of a dataset into training and testing subsets, which is essential in machine learning workflows. This class allows users to create splits of data, enabling them to train models on one portion of the data while validating their performance on another. It works in conjunction with the `DataSubset2` class, which represents the actual data subsets used for training and testing.

## Variables

- **SplitIndex**: An integer that indicates the index of the split in relation to the original dataset. This is useful for tracking multiple splits.
- **Training**: An instance of `DataSubset2<TInput, TOutput>`, representing the subset of data used for training the model.
- **Testing**: An instance of `DataSubset2<TInput, TOutput>`, representing the subset of data used for testing the model.

## Functions

### TrainTestDataSplit()
- **Description**: A parameterless constructor that initializes a new instance of the `TrainTestDataSplit` class.

### TrainTestDataSplit(int index, TInput[] inputs, TOutput[] outputs, double[] weights, int[] trainIndices, int[] testIndices)
- **Description**: A constructor that initializes a new instance of the `TrainTestDataSplit` class with specified parameters.
  - **Parameters**:
    - `index`: The index associated with this subset, if any.
    - `inputs`: An array of input instances in this subset.
    - `outputs`: An array of output instances in this subset.
    - `weights`: An array of weights associated with the input instances.
    - `trainIndices`: An array of indices representing the training instances in relation to the original dataset.
    - `testIndices`: An array of indices representing the testing instances in relation to the original dataset.

### DataSubset2<TInput, TOutput>

#### Variables
- **Index**: The index associated with this subset.
- **Inputs**: An array of input instances.
- **Outputs**: An array of output instances.
- **Weights**: An array of weights associated with the input instances.
- **Indices**: An array of indices representing the data in relation to the original dataset.

#### Functions

### DataSubset2(int index, TInput[] inputs, TOutput[] outputs, double[] weights, int[] indices)
- **Description**: A constructor that initializes a new instance of the `DataSubset2` class with specified parameters.
  - **Parameters**:
    - `index`: The index of this subset.
    - `inputs`: An array of input data.
    - `outputs`: An array of output data.
    - `weights`: An array of weights associated with the data.
    - `indices`: An array of indices representing the data in relation to the original dataset. 

This documentation provides a clear understanding of the purpose and functionality of the `TrainTestDataSplit` and `DataSubset2` classes, making it easier for developers to use and integrate them into machine learning workflows.