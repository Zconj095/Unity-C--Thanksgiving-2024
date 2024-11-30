# SplitSetValidation2

## Overview
The `SplitSetValidation2` class is designed for validating machine learning models by splitting data into training and validation sets. It allows for both random and stratified sampling of the dataset, ensuring that the training set has a specified proportion of samples compared to the validation set. This class is crucial for evaluating the performance of machine learning models by providing a structured way to assess how well a model generalizes to unseen data.

## Variables

- **Indices**: `int[]`
  - Stores the indices of all available samples, which are used to create the training and validation sets.

- **Proportion**: `double`
  - Represents the ratio of training samples to the total number of samples, dictating how the dataset is split.

- **IsStratified**: `bool`
  - Indicates whether the split is stratified, meaning that it maintains the distribution of classes in the training and validation sets.

- **ValidationSet**: `int[]`
  - Contains the indices of the samples designated for validation.

- **TrainingSet**: `int[]`
  - Contains the indices of the samples designated for training.

- **Fitting**: `Func<int[], SplitSetStatistics<TModel>>`
  - A function delegate that defines how to fit the model using the training set.

- **Evaluation**: `Func<int[], TModel, SplitSetStatistics<TModel>>`
  - A function delegate that defines how to evaluate the model using the validation set.

## Functions

- **SplitSetValidation2(int size, double proportion)**
  - Constructor that initializes a new instance of `SplitSetValidation2` for random sampling. It takes the total number of available samples and the proportion of training samples as parameters.

- **SplitSetValidation2(int size, double proportion, int[] outputs)**
  - Constructor that initializes a new instance for stratified sampling. It requires the total number of samples, the proportion of training samples, and the output labels for stratification.

- **GenerateRandomIndices(int size, double proportion)**
  - A private static method that generates an array of random indices to split the dataset into training and validation sets based on the specified proportion.

- **CreateStratifiedSplit(List<int> negativeIndices, List<int> positiveIndices, double proportion)**
  - A private static method that creates a stratified split of the dataset based on the provided negative and positive indices. It ensures that the training set maintains the distribution of classes as per the specified proportion.

- **SplitSetResult<TModel>**
  - A nested class that encapsulates the results of the split-set validation process, including the validation object and statistics for both training and validation sets.