# BaseKNearestNeighbors<TModel, TInput, TDistance>

## Overview
The `BaseKNearestNeighbors` class is an abstract implementation of the K-Nearest Neighbors (KNN) algorithm, designed to facilitate machine learning tasks within the EdgeLoreMachineLearning namespace. This class serves as a base for creating specific KNN models by allowing the definition of how to learn from input data and retrieve nearest neighbors. It manages inputs, outputs, the distance metric, and the cancellation token for processing, providing a framework that can be extended by derived classes to implement specific behaviors.

## Variables
- **k**: An integer representing the number of nearest neighbors to consider. It is initialized to 5 by default.
- **inputs**: An array of type `TInput`, holding the input data points used for neighbor searches.
- **outputs**: An array of integers representing the classification labels corresponding to the input data points.
- **distance**: An instance of type `TDistance`, representing the distance metric used to evaluate the proximity between input points.
- **cancellationToken**: An object used for managing cancellation of tasks, initialized through reflection to create an instance of `System.Threading.CancellationToken`.

## Functions
- **Distance**: Property that gets or sets the distance metric used in the KNN algorithm.
- **K**: Property that gets or sets the number of neighbors (k). It throws an exception if the value is less than or equal to zero.
- **Token**: Property that gets or sets the cancellation token. It ensures that the token is of the correct type, throwing an exception if it is not.
- **Inputs**: Property that gets or sets the input data points. This property is protected, allowing derived classes to set it.
- **Outputs**: Property that gets or sets the output labels. This property is also protected for use by derived classes.
- **GetNearestNeighbors**: An abstract method that must be implemented by derived classes to retrieve the nearest neighbors for a given input and output their labels.
- **Learn**: An abstract method that must be implemented by derived classes to train the model using provided input data and corresponding labels.
- **CheckArgs**: A static internal method that validates the parameters for learning and neighbor retrieval. It checks for null values, mismatched lengths, out-of-range k values, and unsupported weights.
- **GetNumberOfInputs**: An internal method that determines the number of inputs based on the provided array. It checks for consistency in the length of input lists.
- **Score**: A virtual method that calculates the score for a specific input and class index. It retrieves scores using the `Scores` method.
- **Scores**: A virtual method that computes the scores for a given input by invoking a private method named `ComputeScore` through reflection. It returns an array of scores or an empty array if the method cannot be invoked.