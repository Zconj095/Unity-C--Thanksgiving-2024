# ProbabilisticDualCoordinateDescent

## Overview
The `ProbabilisticDualCoordinateDescent` script implements a probabilistic dual coordinate descent optimization algorithm, which is a machine learning technique used for training models. This script extends the abstract class `BaseProbabilisticDualCoordinateDescent`, providing a specific implementation for a dual coordinate linear model (`DUALCOORDINATELINEAR`). The main function of this script is to run the learning algorithm that optimizes the model's parameters based on input data and a defined kernel function. It fits into the codebase as a key component for implementing probabilistic machine learning models, leveraging dual coordinate descent to improve efficiency and accuracy.

## Variables
- `weights`: Stores the weights of the model, initialized during the optimization process.
- `biasIndex`: An integer representing the index of the bias in the weights array.
- `epsilon`: A double value set to 0.1, representing the tolerance level for convergence.
- `maxIterations`: An integer set to 1000, representing the maximum number of iterations for the optimization algorithm.
- `Model`: The trained model instance of type `TModel`.
- `Kernel`: An instance of the kernel used for the model, of type `TKernel`.
- `Inputs`: An array of input data used for training the model.
- `Outputs`: An array of output labels corresponding to the input data.
- `C`: An array of double values representing the upper bounds for the dual variables.

## Functions
- `Create(int inputs, DUALCOORDINATELINEAR kernel)`: Creates and returns a new instance of `DUALSMV` initialized with the specified number of inputs and kernel.
  
- `RunAlgorithm()`: Executes the learning algorithm, performing optimization steps until convergence is reached or the maximum number of iterations is achieved.

- `InitializeAlphaAndWeights(double[] alpha, double[] xTx, int samples)`: Initializes the dual variables (alpha) and weights based on the input samples and kernel function.

- `PerformOptimizationStep(double[] alpha, double[] xTx, int samples)`: Executes a single optimization step by shuffling the input indices and optimizing the dual problem for each input.

- `OptimizeDualProblem(double[] alpha, double[] xTx, TInput input, int index)`: Optimizes the dual problem for a specific input, updating the alpha variables and weights based on the computed gradient.

- `UpdateAlphaAndWeights(double[] alpha, int index, double step, double upperBound)`: Updates the dual variable alpha and the weights based on the computed step and upper bound.

- `UpdateWeights(double[] alpha, int index, double delta = 0)`: Updates the weights of the model using the kernel's product operation.

- `ConvergenceReached()`: Checks if the convergence criterion is met based on the current weights.

- `FinalizeModel()`: Finalizes the model by creating it and extracting the weights and bias after the optimization is complete.

- `SwapIndices(int i, int j)`: Swaps the input data at indices `i` and `j`.

- `ShuffleIndices(int samples)`: Randomly shuffles the indices of the input samples to ensure a varied optimization process.

- `CreateEmptyTInput(int size)`: Creates an instance of `TInput` with the specified size using reflection.