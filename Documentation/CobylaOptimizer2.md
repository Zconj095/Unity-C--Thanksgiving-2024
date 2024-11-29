# CobylaOptimizer

## Overview
The `CobylaOptimizer` class is designed to perform optimization using the COBYLA (Constrained Optimization BY Linear Approximations) algorithm. It allows users to define parameters, add constraints, and solve optimization problems involving continuous variables. This class fits within a larger codebase that likely deals with optimization tasks, providing a structured approach to finding optimal solutions while considering various constraints.

## Variables
- `_parameters`: A dictionary that holds various optimization parameters such as the initial step size, convergence criteria, maximum function evaluations, and other settings.
- `_constraints`: A list that stores functions representing constraints that the optimization process must adhere to.

## Functions
- **CobylaOptimizer**: Constructor that initializes the optimizer with default values for various parameters such as `rhobeg`, `rhoend`, `maxfun`, and others. It also initializes the constraints list.
  
- **GetParameter(string name)**: Retrieves the value of a specified parameter from the `_parameters` dictionary. Throws a `MissingMemberException` if the parameter is not found.
  
- **SetParameter(string name, object value)**: Sets the value of a specified parameter in the `_parameters` dictionary. Throws a `MissingMemberException` if the parameter is not found.
  
- **AddConstraint(Func<double[], double> constraint)**: Adds a constraint function to the `_constraints` list for later evaluation during the optimization process.
  
- **InvokeMethod(string methodName, object[] parameters)**: Uses reflection to invoke a private method by its name, passing the specified parameters. Throws a `MissingMethodException` if the method is not found.
  
- **GetCompatibilityMsg(Dictionary<string, object> problem)**: Checks if the optimization problem contains discrete variables and returns a compatibility message if so. The COBYLA optimizer only supports continuous variables.
  
- **Solve(Dictionary<string, object> problem)**: Main method to execute the optimization process. It checks compatibility, transforms the problem for minimization, adds constraints, and performs the optimization. Throws an `InvalidOperationException` if there are compatibility issues.
  
- **TransformToMinimization(Dictionary<string, object> problem)**: Converts a maximization problem into a minimization format by negating the objective function if necessary.
  
- **AddBoundsConstraints(Dictionary<string, object> problem)**: Adds bounds constraints to the `_constraints` list based on the provided problem's bounds.
  
- **AddProblemConstraints(Dictionary<string, object> problem)**: Adds additional constraints from the provided problem to the `_constraints` list.
  
- **PerformOptimization(Dictionary<string, object> problem)**: Executes the COBYLA optimization algorithm. It initializes the starting point, iteratively adjusts values, checks constraints, and returns the optimization result.
  
- **ToString()**: Returns a string representation of the optimizer, including its parameters and the count of constraints.

## OptimizationResult Class
- **X**: An array representing the optimized variable values.
- **ObjectiveValue**: The value of the objective function at the optimized variable values.
- **ToString()**: Returns a string representation of the optimization result, displaying the solution and the objective value.