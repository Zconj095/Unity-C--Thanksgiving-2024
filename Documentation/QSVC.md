# QSVC

## Overview
The `QSVC` class implements a Quantum Support Vector Classifier (QSVC) for binary classification tasks. It utilizes a quantum kernel function to compute the decision boundary for classifying data points. The class provides methods for fitting the model to training data and making predictions on new data points. It is designed to integrate with Unity's MonoBehaviour, allowing for easy use within Unity applications. The QSVC serves as a foundational component for implementing quantum machine learning techniques within the codebase.

## Variables
- **quantumKernel**: A function that takes two 2D float arrays as input and returns a 2D float array. This function computes the quantum kernel values necessary for the SVC algorithm.
- **supportVectors**: A 2D float array that stores the support vectors used in the model after fitting.
- **coefficients**: A 1D float array that holds the coefficients corresponding to each support vector.
- **intercept**: A float that represents the intercept term of the decision function.
- **labels**: A 1D integer array that stores the unique class labels found in the training data.

## Functions
- **QSVC(Func<float[,], float[,], float[,]> quantumKernel = null)**: Constructor for the QSVC class. It initializes the quantum kernel function. If no kernel is provided, a default quantum kernel is used.

- **void Fit(float[,] X, int[] y)**: Fits the QSVC model to the training data. It validates the input data, checks for binary classification, and stores the support vectors, coefficients, and intercept.

- **int[] Predict(float[,] X)**: Makes predictions on new data points. It requires that the model has been fitted and returns an array of predicted class labels.

- **float ComputeDecisionValue(float[] sample)**: Computes the decision value for a given sample using the support vectors and their corresponding coefficients.

- **float QuantumKernelFunction(float[] x1, float[] x2)**: Calculates the quantum kernel value for two input samples by invoking the quantum kernel function.

- **float[,] DefaultQuantumKernel(float[,] X1, float[,] X2)**: A default implementation of a quantum kernel that uses a dot product kernel for simplicity. It generates a kernel matrix for the provided input matrices.

- **float DotProduct(float[] vec1, float[] vec2)**: Computes the dot product of two vectors.

- **float[] GetRow(float[,] matrix, int row)**: Retrieves a specific row from a 2D float array and returns it as a 1D float array.

- **float[,] To2DArray(float[] vector)**: Converts a 1D float array into a 2D float array with a single row.