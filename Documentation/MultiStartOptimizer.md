# MultiStartOptimizer

## Overview
The `MultiStartOptimizer` class is designed to perform multi-start optimization for a given optimization problem. It allows for multiple trials to find the optimal solution by initializing different starting points for each trial. The class can handle both minimization and maximization problems, converting them as necessary. It fits within a codebase that deals with optimization tasks, providing a systematic approach to exploring the solution space and improving the likelihood of finding a global optimum.

## Variables

- `_trials`: An integer that specifies the number of optimization trials to perform. It must be greater than or equal to 1.
- `_clip`: A float that defines the clipping bounds for the variable initialization during trials.
- `_variables`: A list of `Variable` objects that represent the optimization variables in the problem.

## Functions

- **MultiStartOptimizer(int trials = 1, float clip = 100.0f)**: 
  - Constructor that initializes the optimizer with a specified number of trials and a clip value. It throws an exception if the number of trials is less than 1.

- **Trials**: 
  - Property that gets or sets the number of trials. It throws an exception if the value is less than 1.

- **Clip**: 
  - Property that gets or sets the clip value.

- **OptimizationResult MultiStartSolve(Func<float[], Tuple<float[], object>> minimize, OptimizationProblem problem)**: 
  - The main method that performs the multi-start optimization. It takes a minimization function and an optimization problem as input, initializes starting points for each trial, and evaluates the objective function to find the best solution.

- **private float[] InitializeStartPoint(OptimizationProblem problem, int trial)**: 
  - Initializes a starting point for the optimization based on the trial number. It generates random values within the bounds defined for each variable for trials beyond the first.

- **private float EvaluateObjective(OptimizationProblem problem, float[] solution)**: 
  - Evaluates the objective function for a given solution and returns the objective value.

- **private OptimizationProblem ConvertToMinimization(OptimizationProblem problem)**: 
  - Converts the optimization problem to a minimization problem if it is not already, setting the `IsMaximization` property to false.

- **private OptimizationResult InterpretSolution(float[] xSolution, float fvalSolution, OptimizationProblem problem, object restSolution)**: 
  - Packages the results of the optimization into an `OptimizationResult` object, which contains the solution, objective value, status, and any additional results.

- **public object InvokeMethod(string methodName, params object[] parameters)**: 
  - Invokes a method by its name using reflection and returns the result. Throws an exception if the method is not found.

- **public object GetProperty(string propertyName)**: 
  - Retrieves the value of a property by its name using reflection. Throws an exception if the property is not found.

- **public void SetProperty(string propertyName, object value)**: 
  - Sets the value of a property by its name using reflection. Throws an exception if the property is not found.

### Nested Classes

- **Variable**: 
  - Represents a variable in the optimization problem, containing its name, lower bound, and upper bound.

- **OptimizationProblem**: 
  - Represents an optimization problem, including the list of variables, the objective function, and a flag indicating whether it is a maximization problem.

- **OptimizationResult**: 
  - Represents the result of an optimization attempt, including the solution, objective value, status, and raw results. It also includes a method to convert the result to a string representation.