# TrainValTestDataSplit and DataSubset3 Classes

## Overview
The `TrainValTestDataSplit` class is designed to manage the partitioning of a dataset into three distinct subsets: training, validation, and testing. This structure is essential in machine learning workflows, where models are trained on one subset of data, validated on another, and finally tested on a separate set to evaluate performance. The `DataSubset3` class serves as a container for the actual data instances (inputs and outputs), their weights, and their indices in relation to the original dataset. Together, these classes facilitate the organization and management of datasets for machine learning tasks.

## Variables

### TrainValTestDataSplit
- **SplitIndex**: An integer representing the index of the split in relation to the original dataset.
- **Training**: An instance of `DataSubset3<TInput, TOutput>` that holds the training data subset.
- **Validation**: An instance of `DataSubset3<TInput, TOutput>` that holds the validation data subset.
- **Testing**: An instance of `DataSubset3<TInput, TOutput>` that holds the testing data subset.

### DataSubset3
- **Index**: An integer representing the index associated with this data subset.
- **Inputs**: An array of type `TInput` that contains the input instances for this subset.
- **Outputs**: An array of type `TOutput` that contains the output instances for this subset.
- **Weights**: An array of doubles representing the weights associated with the input instances.
- **Indices**: An array of integers representing the indices of the data in relation to the original dataset.

## Functions

### TrainValTestDataSplit
- **Constructor**: 
  - `TrainValTestDataSplit()`: Initializes a new instance of the `TrainValTestDataSplit` class without parameters.
  - `TrainValTestDataSplit(int index, TInput[] inputs, TOutput[] outputs, double[] weights, int[] trainIndices, int[] validationIndices, int[] testingIndices)`: Initializes a new instance of the `TrainValTestDataSplit` class with specified parameters. It sets the `SplitIndex` and creates three instances of `DataSubset3` for training, validation, and testing using the provided inputs, outputs, weights, and their respective indices.

### DataSubset3
- **Constructor**: 
  - `DataSubset3(int index, TInput[] inputs, TOutput[] outputs, double[] weights, int[] indices)`: Initializes a new instance of the `DataSubset3` class. It sets the `Index`, `Inputs`, `Outputs`, `Weights`, and `Indices` based on the parameters passed during instantiation.

This documentation provides a clear understanding of the purpose and structure of the `TrainValTestDataSplit` and `DataSubset3` classes, making it easier for developers to utilize and extend this functionality in their machine learning projects.