# CrossValidation

## Overview
The `CrossValidation` class provides a mechanism for performing k-Fold Cross-Validation in a Unity-compatible environment using reflection. This technique is essential in machine learning for assessing how the results of a statistical analysis will generalize to an independent dataset. The class allows for partitioning a dataset into a specified number of folds and systematically evaluates a learning algorithm's performance using those folds.

## Variables

- **_folds**: An integer that represents the number of folds to be used in the cross-validation process. It is initialized in the constructor and is crucial for partitioning the dataset.
  
- **_partitions**: An array of lists containing integers. Each list corresponds to a fold and contains the indices of the data points assigned to that fold. This structure is created in the constructor and used during the cross-validation process.

## Functions

- **CrossValidation(int size, int folds)**: Constructor that initializes the `CrossValidation` instance. It takes the total size of the dataset and the number of folds as parameters. It throws an exception if the number of folds exceeds the dataset size.

- **List<int>[] CreatePartitions(int size, int folds)**: A private method that creates partitions of the dataset indices into the specified number of folds. It shuffles the indices randomly and distributes them into the folds. It returns an array of lists, where each list contains the indices for a specific fold.

- **void PerformCrossValidation<TModel, TInput, TOutput>(object learnerInstance, TInput[] inputs, TOutput[] outputs, MethodInfo learnMethod, MethodInfo evaluateMethod)**: This method executes the k-Fold Cross-Validation process. It takes in a learner instance (the model to be trained), the input data, the expected output data, and the methods for learning and evaluating the model. It iterates over each fold, creating training and validation datasets, invoking the learning method on the training set, and evaluating the resulting model with the validation set. The results are logged to the Unity console.