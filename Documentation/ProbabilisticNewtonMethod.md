# ProbabilisticNewtonMethod

## Overview
The `ProbabilisticNewtonMethod` class implements a logistic regression learning algorithm using L2-regularized L2-loss, functioning as a probabilistic support vector machine. It extends the abstract base class `BaseProbabilisticNewtonMethod`, which provides the foundational structure for executing the optimization algorithm. This class is designed to handle the initialization and execution of the learning process, making it an integral part of the EdgeLoreMachineLearning codebase.

## Variables
- `z`: An array of doubles representing intermediate variables used in the optimization process.
- `D`: An array of doubles that stores the diagonal elements of the Hessian matrix.
- `g`: An array of doubles that contains the gradient values computed during optimization.
- `Hs`: An array of doubles representing the Hessian matrix values.
- `biasIndex`: An integer indicating the index of the bias term in the model parameters.
- `tolerance`: A double specifying the convergence criteria for the optimization process (default is 0.01).
- `maxIterations`: An integer that sets the maximum number of iterations for the optimization process (default is 1000).
- `Model`: An instance of the model type `TModel`, which is a derived class of `Svmnewton`.
- `Kernel`: An instance of the kernel type `TKernel`, which defines the kernel function used in the model.
- `Inputs`: An array of type `TInput` containing the input data for the model.
- `Outputs`: An array of integers representing the target outputs associated with the input data.
- `C`: An array of doubles representing the regularization parameters.

## Functions
- `ProbabilisticNewtonMethod()`: Constructor that initializes a new instance of the `ProbabilisticNewtonMethod` class.
  
- `Initialize(int inputs, NewtonLinear kernel)`: 
  - Creates and initializes a new `Svmnewton` instance with the specified number of inputs and sets the kernel.

- `RunAlgorithm()`: 
  - Executes the optimization algorithm, iterating through the maximum number of iterations to update model weights based on the computed gradient and Hessian.

- `InitializeParameters()`: 
  - Initializes the parameters required for the optimization process, including setting up the arrays for intermediate calculations.

- `ReflectObjective()`: 
  - Uses reflection to invoke the private `Objective` method, returning the current objective value of the optimization.

- `ReflectGradient()`: 
  - Uses reflection to invoke the private `Gradient` method, returning the computed gradient.

- `ReflectHessian(double[] gradient)`: 
  - Uses reflection to invoke the private `Hessian` method, returning the computed Hessian based on the provided gradient.

- `UpdateModelWeights(double[] gradient, double[] hessian)`: 
  - Updates the model weights based on the computed gradient and Hessian values.

- `ConvergenceReached(double[] gradient)`: 
  - Checks if the optimization has converged by evaluating the sum of the absolute values of the gradient against the tolerance.

- `ReflectGetLength(TKernel kernel, TInput[] inputs)`: 
  - Uses reflection to invoke the `GetLength` method of the kernel to determine the length of the input data.

Overall, the `ProbabilisticNewtonMethod` class provides a structured approach to implementing a probabilistic learning algorithm, leveraging the abstract base class for core functionality while allowing for specific model initialization and execution.