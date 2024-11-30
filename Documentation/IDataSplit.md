# IDataSplit Interface Documentation

## Overview
The `IDataSplit` interface defines a common structure for data splits within the EdgeLoreMachineLearning codebase. It is designed to facilitate the partitioning of datasets into subsets for various purposes, such as training, validation, or testing in machine learning applications. By implementing this interface, different types of data splits can be created, ensuring that they are iterable and have a specific index associated with their position in the original dataset.

## Variables
- **SplitIndex**: An integer property that represents the index of the split in relation to the original dataset. This allows users to identify the specific position of the split within the overall dataset.

## Functions
The `IDataSplit` interface inherits from `IEnumerable<DataSubset<TInput, TOutput>>`, which means it provides the following functionality:
- **IEnumerable<DataSubset<TInput, TOutput>>**: This allows the implementation of the interface to be enumerated, meaning users can iterate through the subsets of data that are produced by the split. Each subset consists of both input data (`TInput`) and output data (`TOutput`). 

This structure promotes flexibility and consistency across different implementations of data splits, making it easier to manage and utilize datasets in machine learning workflows.