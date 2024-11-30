# SupportVectorReduction

## Overview
The `SupportVectorReduction` class is a specialized implementation for reducing support vectors in a Support Vector Regression (SVR) model. It inherits from the abstract class `SupportVectorReductionBase`, which provides the core functionality for reducing the number of support vectors based on their weights and the Gram matrix. This reduction process is crucial for optimizing the model's performance and efficiency, particularly in scenarios where the number of support vectors is large. The class fits into the broader EdgeLoreMachineLearning codebase, which focuses on machine learning techniques in Unity.

## Variables
- **Model**: A protected variable of type `TModel`, which represents the Support Vector Regression model being reduced.
- **threshold**: A private variable of type `double` initialized to `1e-12`. This variable determines the threshold for retaining non-zero weights during the reduction process.
- **Threshold**: A public property that provides access to the `threshold` variable, allowing it to be modified externally.

## Functions
- **SupportVectorReduction(SupportVectorRegressionSVM<SupportVectorRegressionIKernel<double[]>, double[]> machine)**: Constructor that initializes the `SupportVectorReduction` instance with a specified SVR model. It throws an `ArgumentNullException` if the provided model is null.

- **Reduce()**: This method performs the reduction of support vectors. It logs the process of reducing support vectors, creates the Gram matrix, performs row-reduction to obtain the reduced row echelon form, identifies independent vectors, retains non-zero weights, and updates the model's support vectors and weights accordingly.

- **CreateGramMatrix(TInput[] supportVectors, TKernel kernel)**: This private method generates a Gram matrix for the given support vectors using the specified kernel. It returns a two-dimensional array representing the Gram matrix.

- **ReducedRowEchelonForm(double[][] matrix)**: This private method performs the row reduction of the input matrix to obtain its reduced row echelon form (RREF). It returns a tuple containing the RREF matrix and an array of pivot indices.

- **RetainNonZero(double[] array, double threshold)**: This private method filters the input array to retain indices of elements that are greater than the specified threshold. It returns an array of indices corresponding to non-zero weights.

- **Extract<T>(T[] array, int[] indices)**: This private method extracts elements from the input array based on the provided indices. It returns a new array containing only the extracted elements.