# FanChenLinSupportVectorRegression

## Overview
The `FanChenLinSupportVectorRegression` class implements a support vector regression (SVR) algorithm using a specified model and kernel. It is part of the `EdgeLoreMachineLearning` namespace and is designed to facilitate machine learning tasks in Unity. The primary function of this class is to train a support vector regression model based on input data and corresponding output values, optimizing the model parameters to best fit the data.

## Variables

- **_model**: An instance of type `TModel`, representing the regression model being trained.
- **_kernel**: An instance of type `TKernel`, representing the kernel used for the regression.
- **_alpha**: An array of doubles that stores the Lagrange multipliers for the support vectors.
- **_tolerance**: A double that defines the tolerance level for the optimization process (default is 0.001).
- **_shrinking**: A boolean that indicates whether to use the shrinking heuristic during optimization (default is true).
- **_rho**: A double that represents the threshold value used in the regression (default is 1e-3).

## Functions

- **FanChenLinSupportVectorRegression(TModel model, TKernel kernel)**: 
  Constructor that initializes the support vector regression with a specified model and kernel.

- **double Tolerance**: 
  Property that gets or sets the tolerance value for the optimization process.

- **bool Shrinking**: 
  Property that gets or sets whether the shrinking heuristic is used during optimization.

- **double Rho**: 
  Property that gets or sets the threshold value used in the regression model.

- **double[] Train(TInput[] inputs, double[] outputs, double[] complexity)**: 
  Method that trains the support vector regression model using the provided input data, output values, and complexity constraints. It sets up the optimization problem, executes the optimization, and updates the model with the support vectors and weights.

- **private double KernelFunction(TInput x, TInput y)**: 
  Method that computes the kernel function value between two input instances. It uses reflection to invoke the kernel function defined in the kernel instance.

- **private void UpdateModel(TInput[] inputs, double[] alpha, double rho)**: 
  Method that updates the regression model with the identified support vectors, their corresponding weights, and the threshold value.

## FanChenLinQuadraticOptimizer

### Overview
The `FanChenLinQuadraticOptimizer` class is responsible for minimizing the quadratic optimization problem associated with the support vector regression. It works in conjunction with the `FanChenLinSupportVectorRegression` class to find optimal values for the Lagrange multipliers.

### Variables

- **_size**: An integer representing the size of the optimization problem.
- **_qFunction**: A function that computes the quadratic terms of the optimization problem.
- **_linearTerm**: An array of doubles representing the linear terms in the optimization problem.
- **_signs**: An array of integers representing the signs associated with the optimization variables.
- **Solution**: An array of doubles that stores the solution to the optimization problem (the Lagrange multipliers).
- **UpperBounds**: An array of doubles that defines the upper bounds for the optimization variables.
- **Tolerance**: A double that specifies the tolerance for the optimization process (default is 0.001).
- **Shrinking**: A boolean that indicates whether the shrinking heuristic is applied during optimization.
- **Rho**: A double that stores the threshold value after minimization.

### Functions

- **FanChenLinQuadraticOptimizer(int size, Func<int, int[], int, double[], double[]> qFunction, double[] linearTerm, int[] signs)**: 
  Constructor that initializes the optimizer with the size of the problem, the quadratic function, linear terms, and signs.

- **bool Minimize()**: 
  Method that performs the minimization of the quadratic optimization problem. It currently contains a simplified implementation suitable for Unity and returns a success status.