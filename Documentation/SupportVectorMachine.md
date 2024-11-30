# SupportVectorMachine.cs

## Overview
The `SupportVectorMachine` class is a generic implementation of a Support Vector Machine (SVM) algorithm, which is a supervised machine learning model used for classification tasks. This class is designed to work with various kernel functions, allowing for flexibility in how the input data is processed. The `SupportVectorMachine` class integrates with the broader codebase by providing a robust framework for training and making predictions based on input data, utilizing support vectors and weights.

## Variables
- **Kernel**: An instance of the kernel function used for computing the similarity between input vectors.
- **SupportVectors**: An array of input vectors that define the decision boundary of the SVM.
- **Weights**: An array of weights associated with each support vector, influencing their importance in the decision-making process.
- **Threshold**: A bias term added to the decision function, helping to adjust the decision boundary.
- **IsProbabilistic**: A boolean flag indicating whether the SVM is configured to provide probabilistic outputs.
- **NumberOfInputs**: An integer representing the number of input features for the SVM.
- **NumberOfClasses**: An integer representing the number of classes for classification, defaulting to 2.

## Functions
- **SupportVectorMachine(int inputs, TKernel kernel)**: Constructor that initializes a new instance of the `SupportVectorMachine` class with the specified number of input features and a kernel function. Throws an exception if the kernel is null.

- **SetSupportVectors(TInput[] supportVectors, double[] weights, double threshold)**: Method to set the support vectors, weights, and threshold for the SVM. Throws exceptions if the input arrays are null or of different lengths.

- **Decide(TInput input)**: Method that computes a decision for a given input vector based on the support vectors and weights. Returns a boolean indicating the class assignment.

- **ComputeScores(TInput[] inputs)**: Method that computes scores for a batch of input vectors, returning an array of scores based on the support vectors and weights.

- **Compress()**: Method that compresses the SVM for linear kernels by adjusting the support vectors and weights. Throws an exception if the kernel is not a linear kernel.

- **ToWeights()**: Method that converts the SVM into an array of weights for linear kernels, including the threshold as the first element. Throws an exception if the kernel is not a linear kernel.

- **Clone()**: Method that creates a deep copy of the current instance of the `SupportVectorMachine`, preserving its state.

- **DebugPrint()**: Utility method that prints the details of the SVM, including the number of inputs, threshold, support vector count, weights count, and whether it is probabilistic.