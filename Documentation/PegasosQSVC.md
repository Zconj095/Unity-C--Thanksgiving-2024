# PegasosQSVC

## Overview
The `PegasosQSVC` class implements a Pegasos (Primal Estimated sub-GrAdient Solver for SVM) algorithm for binary classification using Support Vector Machines (SVM). This script is designed to fit a model to training data and then make predictions on new data. It is part of a larger codebase that likely involves machine learning and data processing, providing a mechanism to classify data into two distinct categories based on training samples.

## Variables
- **C**: A float that represents the regularization parameter. It controls the trade-off between maximizing the margin and minimizing the classification error.
- **numSteps**: An integer indicating the number of iterations for the training process.
- **precomputed**: A boolean indicating whether to use a precomputed kernel for training.
- **random**: An instance of `System.Random` used for generating random numbers, particularly for selecting training samples during the fitting process.
- **alphas**: A dictionary that maps the indices of training samples to their corresponding alpha values used in the SVM calculations.
- **xTrain**: A 2D array of floats representing the training feature data.
- **yTrain**: A 1D array of floats representing the labels for the training data.
- **nSamples**: An integer that stores the number of training samples.
- **labelPos**: An integer representing the positive label in the training data.
- **labelNeg**: An integer representing the negative label in the training data.
- **labelMap**: A dictionary that maps the original labels to +1 and -1 for internal processing.
- **fitStatus**: An enumerated type indicating whether the model is fitted or unfitted, with possible values `UNFITTED` and `FITTED`.

## Functions
- **Initialize(float C = 1.0f, int numSteps = 1000, bool precomputed = false, int? seed = null)**: Initializes the PegasosQSVC instance with specified parameters, including the regularization parameter, number of steps, precomputation flag, and an optional random seed. It also validates the input for `C` and resets the internal state.
  
- **Fit(float[,] X, float[] y)**: Fits the model to the provided training data `X` and labels `y`. It validates input dimensions, maps labels to +1 and -1, and runs the training loop to compute the alpha values for the support vectors.

- **Predict(float[,] X)**: Takes in new data `X` and returns an array of predictions (either the positive or negative label) based on the fitted model. It checks if the model has been fitted before making predictions.

- **ComputeWeightedKernelSum(int index, bool training, float[,] xPredict = null)**: Computes the weighted kernel sum for a given index. It aggregates contributions from all support vectors based on their alpha values and the kernel function.

- **ValidateInput(float[,] X, float[] y)**: Validates the input training data and labels to ensure they have the same number of samples and that only binary classification is supported.

- **ValidatePredictInput(float[,] X)**: Validates the input for prediction to ensure the model is fitted and that the dimensions match the training data if using a precomputed kernel.

- **ResetState()**: Resets the internal state of the model, clearing all training data and alpha values.

- **GetRow(float[,] matrix, int row)**: Retrieves a specific row from a 2D array (matrix) and returns it as a 1D array.

- **VectorDot(float[] vec1, float[] vec2)**: Computes the dot product of two vectors (arrays) and returns the result as a float.