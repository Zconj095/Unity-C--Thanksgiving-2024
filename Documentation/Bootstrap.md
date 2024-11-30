# Bootstrap Class Documentation

## Overview
The `Bootstrap` class implements Bootstrap resampling, a statistical method used for model validation within the Unity environment. This class allows developers to create multiple subsamples from a dataset, which can be used for training and validating machine learning models. By leveraging this class, users can efficiently manage resampling and model fitting processes, either in parallel or sequentially, depending on their needs.

## Variables

- **_subsampleIndices**: `int[][]`
  - An array of integer arrays that holds the indices of the samples selected for each bootstrap sample.
  
- **_samples**: `int`
  - The total number of samples in the original dataset.
  
- **_parallel**: `bool`
  - A flag indicating whether the bootstrap resampling should be performed in parallel (true) or sequentially (false).
  
- **_fitting**: `Func<int[], int[], BootstrapValues>`
  - A delegate function that takes two integer arrays (representing the training and validation sets) and returns an instance of `BootstrapValues`, which contains the results of the model fitting.

## Properties

- **B**: `int`
  - Returns the number of bootstrap samplings performed.

- **Samples**: `int`
  - Returns the total number of samples in the dataset.

- **Subsamples**: `int[][]`
  - Returns the bootstrap samples as indices.

- **Fitting**: `Func<int[], int[], BootstrapValues>`
  - Gets or sets the model fitting function. Throws an `ArgumentNullException` if set to null.

- **RunInParallel**: `bool`
  - Gets or sets a value indicating whether to run the resampling in parallel.

## Functions

- **Bootstrap(int size, int resamples)**:
  - Constructor that initializes the `Bootstrap` instance with a specified number of samples and resamples, using the same size for subsamples.

- **Bootstrap(int size, int resamples, int subsampleSize)**:
  - Constructor that initializes the `Bootstrap` instance with a specified number of samples, resamples, and a specific subsample size.

- **Bootstrap(int size, int[][] resamplings)**:
  - Constructor that initializes the `Bootstrap` instance using predefined resampling indices.

- **CreatePartitions(int index, out int[] trainingSet, out int[] validationSet)**:
  - Creates training and validation sets based on the specified bootstrap sample index. Throws an `ArgumentOutOfRangeException` if the index is invalid.

- **Compute()**:
  - Executes the fitting function on each bootstrap sample and returns a `BootstrapResult` containing the results. Throws an `InvalidOperationException` if the fitting function is not defined.

- **GetPartitionSize(int index, out int trainingCount, out int validationCount)**:
  - Retrieves the sizes of the training and validation sets for a specified bootstrap sample index. Throws an `ArgumentOutOfRangeException` if the index is invalid.

- **GenerateSubsamples(int size, int resamples, int subsampleSize)**: 
  - A private static method that generates random subsamples based on the specified parameters (total size, number of resamples, and subsample size). Returns an array of integer arrays representing the generated subsamples. 

This documentation provides a clear understanding of the `Bootstrap` class, its variables, properties, and functions, making it easier for developers to utilize and integrate into their projects.