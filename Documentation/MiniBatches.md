# MiniBatches Script Documentation

## Overview
The `MiniBatches` script is part of the `EdgeLoreMachineLearning` namespace and is designed to facilitate the creation and management of mini-batches for machine learning tasks. Mini-batches are subsets of the training dataset that are used to train models in smaller, more manageable portions, which can improve training efficiency and convergence. This script provides functionality to create mini-batches from input and output data, handle weights, and manage shuffling strategies for data processing.

## Variables
- **ShuffleMethod**: An enumeration that defines the strategies for shuffling the data:
  - `None`: No shuffling is performed.
  - `OnlyOnce`: The data is shuffled once before the first epoch.
  - `EveryEpoch`: The data is shuffled at the beginning of each epoch.

- **Inputs**: An array of input data used for creating mini-batches.

- **Weights**: An array of weights corresponding to the input data, which can be used to give different importance to samples during training.

- **ShuffledIndices**: An array of indices representing the order of the input data after shuffling.

- **MiniBatchSize**: An integer that defines the size of each mini-batch (default is 32).

- **Shuffle**: A `ShuffleMethod` that determines how the input data is shuffled (default is `OnlyOnce`).

- **MaxIterations**: An integer that specifies the maximum number of iterations for generating mini-batches (default is 0, meaning no limit).

- **MaxEpochs**: An integer that specifies the maximum number of epochs for generating mini-batches (default is 0, meaning no limit).

## Functions
- **Create<TInput, TOutput>(TInput[] input, TOutput[] output, double[] weights = null, int batchSize = 32, int maxIterations = 0, int maxEpochs = 0, ShuffleMethod shuffle = ShuffleMethod.EveryEpoch)**: 
  - A static method that creates and returns a new instance of `Batches<TInput, TOutput>`. It takes arrays of input and output data, optional weights, batch size, maximum iterations, maximum epochs, and a shuffle method as parameters.

- **GetEnumerator()**: 
  - Returns an enumerator that iterates through the mini-batches. It initializes a new batch, manages shuffling based on the specified method, and yields mini-batches until the maximum iterations or epochs are reached.

- **GenerateRange(int length)**: 
  - A private method that generates a sequential array of integers from 0 to `length - 1`. This is used to create indices for the input data.

- **ShuffleArray(int length)**: 
  - A private method that shuffles an array of integers representing indices. It uses the Fisher-Yates shuffle algorithm to randomize the order of indices for the input data.

- **InitBatch()**: 
  - An abstract method that must be implemented by derived classes to initialize and return a new instance of a batch.

- **MiniBatchesDataSubset(int batchSize)**: 
  - A constructor for the `MiniBatchesDataSubset` class that initializes the `Inputs` and `Weights` arrays based on the specified batch size.

- **MiniBatchesDataSubset(int batchSize)** (overloaded):
  - A constructor for the `MiniBatchesDataSubset<TInput, TOutput>` class that initializes the `Outputs` array in addition to the `Inputs` and `Weights` arrays.

This documentation provides a clear understanding of the purpose, structure, and functionality of the `MiniBatches` script, making it easier for developers to utilize and extend the codebase for machine learning applications.