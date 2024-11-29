# CplexOptimizer

## Overview
The `CplexOptimizer` class is designed to facilitate the optimization of mathematical problems using the CPLEX solver. It provides methods to check for CPLEX installation, set parameters, and solve optimization problems modeled as quadratic programs. The class interacts with other components of the codebase by converting problem definitions into a format compatible with CPLEX, solving the problem, and returning the results in a structured format.

## Variables
- **_disp**: A boolean value that determines whether to display additional information during the optimization process.
- **_cplexParameters**: A dictionary that holds parameters specific to the CPLEX solver, allowing customization of the optimization process.

## Functions
- **CplexOptimizer(bool disp = false, Dictionary<string, object> cplexParameters = null)**: Constructor that initializes an instance of the `CplexOptimizer` class with optional display settings and CPLEX parameters.

- **static bool IsCplexInstalled()**: Checks if the CPLEX solver is installed. Returns `true` if CPLEX is available.

- **bool Disp**: Property that gets or sets the display setting for the optimizer.

- **Dictionary<string, object> CplexParameters**: Property that gets or sets the CPLEX parameters.

- **string GetCompatibilityMsg(Dictionary<string, object> problem)**: Returns a message indicating compatibility of the problem with CPLEX. Currently, it assumes all problems modeled as quadratic programs are compatible.

- **OptimizationResult2 Solve(Dictionary<string, object> problem)**: Main method that solves the given optimization problem. It checks compatibility, converts the problem into CPLEX format, solves it, and returns the results.

- **private CplexModel ConvertToCplexModel(Dictionary<string, object> problem)**: Converts the provided problem dictionary into a CPLEX-compatible model, including the number of variables, objective function, and constraints.

- **private CplexSolution SolveWithCplex(CplexModel model)**: Simulates the solving process using CPLEX. It generates a mock solution and evaluates the objective value based on the model.

- **private double EvaluateObjective(Dictionary<string, object> problem, double[] x)**: Evaluates the objective function for a given solution vector `x` based on the problem definition.

- **private OptimizationResultStatus EvaluateFeasibility(Dictionary<string, object> problem, double[] x)**: Checks the feasibility of the solution vector `x` against the defined constraints of the problem and returns the result status.

- **object InvokeMethod(string methodName, object[] parameters)**: Dynamically invokes a method of the `CplexOptimizer` class using reflection, allowing for flexible method calls.

- **object GetProperty(string propertyName)**: Dynamically retrieves the value of a property using reflection.

- **void SetProperty(string propertyName, object value)**: Dynamically sets the value of a property using reflection.

## Additional Classes and Enums
- **CplexModel**: Represents a model compatible with CPLEX, containing the number of variables, objective function, and constraints.

- **CplexSolution**: Represents the solution returned by CPLEX, including the values of variables and the objective value.

- **OptimizationResult2**: Holds the results of the optimization process, including the solution vector, objective value, status, and variable names.

- **OptimizationResultStatus**: An enumeration that defines the possible statuses of an optimization result: `Success` and `Failure`.