# MinimumEigenOptimizer

## Overview
The `MinimumEigenOptimizer` class is designed to solve optimization problems by finding the minimum eigenvalue of a given problem using a specified minimum eigenvalue solver. It ensures compatibility between the problem and the solver, converts the problem into a suitable format (QUBO), and generates an Ising Hamiltonian to compute the minimum eigenvalue. The results of the optimization include the best solution found, its corresponding function value, and additional information about the solver's performance.

## Variables
- `_minEigenSolver`: An object representing the minimum eigenvalue solver used for computations. It must support auxiliary operators.
- `_penalty`: A nullable double representing a penalty value that can be applied during optimization.
- `_converters`: A list of objects used to convert the problem into a format suitable for the solver.

## Functions
- **Constructor: `MinimumEigenOptimizer(object minEigenSolver, double? penalty = null, List<object> converters = null)`**
  - Initializes the optimizer with a specified minimum eigenvalue solver, an optional penalty, and an optional list of converters. It checks if the solver supports auxiliary operators.

- **Property: `MinEigenSolver`**
  - Gets or sets the minimum eigenvalue solver. If a new solver is set, it checks for compatibility with auxiliary operators.

- **Method: `GetCompatibilityMsg(object problem)`**
  - Returns a compatibility message indicating whether the provided problem is compatible with the optimizer.

- **Method: `Solve(object problem)`**
  - Solves the given optimization problem by checking compatibility, converting it to QUBO, generating the Ising Hamiltonian, and invoking the internal solve method.

- **Method: `ConvertProblem(object problem)`**
  - Converts the provided problem using each converter in the `_converters` list.

- **Method: `ToIsing(object problem)`**
  - Converts the problem into an Ising Hamiltonian and retrieves the associated offset.

- **Method: `SolveInternal(object operatorObj, double offset, object convertedProblem, object originalProblem)`**
  - Computes the minimum eigenvalue using the solver and extracts solutions if available. It handles cases with no valid operator and returns results accordingly.

- **Method: `EigenvectorToSolutions(object eigenstate, object problem)`**
  - Converts the eigenstate into a list of solution samples.

- **Method: `CreateResult(List<SolutionSample> rawSamples, object eigenResult, double offset, object originalProblem)`**
  - Creates an `OptimizationResult` object based on the best solution found and its corresponding function value.

- **Method: `CreateEmptyResult(object originalProblem, double offset, object eigenResult)`**
  - Creates an `OptimizationResult` object representing a failure to find a valid solution.

- **Method: `AdjustSamples(List<SolutionSample> rawSamples, object originalProblem)`**
  - Adjusts the raw samples to match the original problem space.

- **Method: `TransformSample(SolutionSample sample, object originalProblem)`**
  - Transforms a sample's values to align with the original problem's requirements.

- **Method: `GetNumVariables(object problem)`**
  - Retrieves the number of variables in the given problem.

- **Method: `InvokeMethod(object instance, string methodName, object[] parameters)`**
  - Invokes a specified method on an instance using reflection.

- **Method: `InvokeStaticMethod(string typeName, string methodName, object[] parameters)`**
  - Invokes a specified static method on a type using reflection.

- **Method: `HasAttribute(object obj, string attributeName)`**
  - Checks if the given object has a specified attribute.

- **Method: `GetAttribute(object obj, string attributeName)`**
  - Retrieves the value of a specified attribute from the given object.

### Nested Classes
- **Class: `OptimizationResult`**
  - Represents the result of the optimization process, including the solution, function value, status, and raw samples.

- **Class: `SolutionSample`**
  - Represents a sample solution with its corresponding values and status.