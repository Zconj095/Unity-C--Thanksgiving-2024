# BaseSupportVectorRegression

## Overview
The `BaseSupportVectorRegression` class serves as an abstract base class for implementing Support Vector Machine (SVM) regression learning algorithms. It provides the foundational structure and common functionality necessary for training SVM models, allowing derived classes to implement specific learning algorithms. This class is part of the `EdgeLoreMachineLearning` namespace, which likely contains other machine learning algorithms and utilities, facilitating a cohesive framework for machine learning within Unity.

## Variables

- `inputs`: An array of input data of type `TInput` used for training the regression model.
- `outputs`: An array of output values (targets) corresponding to the input data, represented as an array of doubles.
- `sampleWeights`: An array of doubles representing the individual weights assigned to each sample in the training set.
- `complexity`: A double representing the cost parameter \( C \), which controls the trade-off between achieving a low training error and a low testing error.
- `epsilon`: A double representing the insensitivity zone for the regression model, defaulting to a small value (`1e-3`).
- `kernel`: An instance of the kernel function of type `TKernel`, which is used to transform the input data into a higher-dimensional space.
- `hasKernelBeenSet`: A boolean flag indicating whether the kernel has been explicitly set by the user.
- `useKernelEstimation`: A boolean flag indicating whether to estimate kernel parameters from the data.
- `useComplexityHeuristic`: A boolean flag indicating whether the complexity parameter \( C \) should be automatically computed.

## Functions

- **Complexity**: 
  - Getter and setter for the `complexity` variable. Throws an exception if the value is non-positive.

- **Epsilon**: 
  - Getter and setter for the `epsilon` variable. Throws an exception if the value is negative.

- **Weights**: 
  - Getter and setter for the `sampleWeights` variable.

- **UseComplexityHeuristic**: 
  - Getter and setter for the `useComplexityHeuristic` variable.

- **UseKernelEstimation**: 
  - Getter and setter for the `useKernelEstimation` variable.

- **Kernel**: 
  - Getter and setter for the `kernel` variable. Sets the `hasKernelBeenSet` flag to true and disables kernel estimation when a kernel is assigned.

- **Model**: 
  - A property to get or set the model being learned.

- **Learn(TInput[] x, double[] y, double[] weights = null)**: 
  - Main method for training the model using provided inputs and outputs. It validates the inputs, initializes the kernel and model, and runs the internal learning algorithm.

- **InnerRun()**: 
  - An abstract method that derived classes must implement to execute the specific learning algorithm.

- **CreateModel(int inputDimensions, TKernel kernel)**: 
  - An abstract method that derived classes must implement to create an instance of the model.

- **InitializeKernel(TInput[] x)**: 
  - Private method that initializes the kernel. If no kernel has been set, it creates a default kernel. If kernel estimation is enabled and the kernel has not been set, it estimates the kernel parameters from the input data.

- **InitializeModel(TInput[] x)**: 
  - Private method that initializes the model. If the model has not been set, it creates a new model based on the input dimensions and kernel.

- **ValidateInputs(TInput[] x, double[] y)**: 
  - Private method that checks if the input data and output data are valid (not null and of the same length). Throws exceptions for invalid inputs.

- **CreateDefaultKernel()**: 
  - Private method that should be overridden in derived classes to provide a default kernel. Throws an exception if not implemented.

- **EstimateKernel(TKernel kernel, TInput[] x)**: 
  - Private method that should be overridden in derived classes to estimate kernel parameters. Throws an exception if not implemented.

- **GetInputDimensions(TInput[] x)**: 
  - Private method that determines the dimensions of the input data. Throws exceptions for empty or invalid input data.