# TrainValSplit<T> Class Documentation

## Overview
The `TrainValSplit<T>` class is designed to represent a split of data into two distinct parts: a training dataset and a validation dataset. This is a common practice in machine learning where a model is trained on one subset of data (the training split) and evaluated on another subset (the validation split). This class is generic, meaning it can work with any data type specified by the type parameter `T`. It implements the `IEnumerable<T>` interface, allowing it to be easily iterated over, which is useful for accessing both the training and validation datasets.

## Variables
- **Training**: This property holds the training split of the dataset. It can be set and retrieved, allowing for flexibility in how the training data is managed.
- **Validation**: This property holds the validation split of the dataset. Similar to the `Training` property, it can be set and retrieved.

## Functions
- **TrainValSplit()**: This is the default constructor that initializes a new instance of the `TrainValSplit<T>` class without any predefined training or validation data.

- **TrainValSplit(T training, T validation)**: This constructor allows for the initialization of the `TrainValSplit<T>` class with specified training and validation datasets. It takes two parameters: `training` (the training split) and `validation` (the validation split).

- **GetEnumerator()**: This method returns an enumerator that iterates through the `TrainValSplit<T>` collection. It yields the `Training` and `Validation` properties, allowing users to access these datasets sequentially.

- **IEnumerator IEnumerable.GetEnumerator()**: This method implements the non-generic `IEnumerable` interface, allowing the class to be used in a non-generic context. It returns the enumerator that can iterate through the collection, delegating to the generic `GetEnumerator()` method. 

This class is a fundamental building block in the EdgeLoreMachineLearning namespace, providing a clear structure for managing training and validation datasets in machine learning applications.