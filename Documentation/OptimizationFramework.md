# OptimizationFramework

## Overview
The `OptimizationFramework` script is designed to facilitate optimization problems by providing a structure for defining optimization problems, running optimization trials, and managing results. It includes classes for representing optimization problems, results, and samples, as well as an optimizer class that performs the optimization process. This script fits into a larger codebase focused on solving mathematical optimization problems, particularly within the Unity game development environment.

## Variables

### OptimizationResultStatus
- **SUCCESS**: Indicates the optimization was successful.
- **FAILURE**: Indicates the optimization failed.
- **INFEASIBLE**: Indicates the optimization problem is infeasible.

### SolutionSample
- **Variables**: An array of floats representing the variables of the solution.
- **ObjectiveValue**: A float representing the value of the objective function for the solution.
- **Probability**: A float representing the probability of the solution being selected.
- **Status**: The status of the solution, represented by the `OptimizationResultStatus` enum.

### OptimizationResult
- **Variables**: An array of floats representing the best solution found.
- **ObjectiveValue**: A float representing the best objective value found.
- **Status**: The status of the optimization result, represented by the `OptimizationResultStatus` enum.
- **Samples**: A list of `SolutionSample` objects representing all samples collected during the optimization process.

### OptimizationProblem
- **ObjectiveFunction**: A function that takes an array of floats and returns a float, representing the objective function to be optimized.
- **VariableCount**: An integer representing the number of variables in the optimization problem.
- **LowerBounds**: An array of floats representing the lower bounds for each variable.
- **UpperBounds**: An array of floats representing the upper bounds for each variable.

### Optimizer
- **_maxTrials**: An integer representing the maximum number of trials to run during the optimization process.
- **_clip**: A float representing the clipping value for the variable initialization.

## Functions

### SolutionSample.ToString()
- **Description**: Returns a string representation of the `SolutionSample`, including its variables, objective value, probability, and status.

### OptimizationResult.ToString()
- **Description**: Returns a string representation of the `OptimizationResult`, including its objective value, status, and variables.

### Optimizer.Optimize(OptimizationProblem problem, Func<float[], float[], (float[], float)> minimizer)
- **Description**: Executes the optimization process for a given `OptimizationProblem` using a specified minimization function. It initializes start points, performs trials, and collects samples, ultimately returning the best solution found.

### Optimizer.InitializeStartPoint(OptimizationProblem problem, int trial)
- **Description**: Initializes the starting point for the optimization based on the trial number and the problem's variable bounds. The first trial starts at zero, while subsequent trials generate random starting points within the specified bounds.

### Optimizer.InvokeMethod(string methodName, params object[] parameters)
- **Description**: Invokes a method of the `Optimizer` class by name, using reflection. If the method does not exist, it throws a `MissingMethodException`.

### Optimizer.GetProperty(string propertyName)
- **Description**: Retrieves the value of a property of the `Optimizer` class by name, using reflection. If the property does not exist, it throws a `MissingMemberException`.

### Optimizer.SetProperty(string propertyName, object value)
- **Description**: Sets the value of a property of the `Optimizer` class by name, using reflection. If the property does not exist, it throws a `MissingMemberException`.