# GurobiOptimizer

## Overview
The `GurobiOptimizer` class is designed to facilitate optimization using the Gurobi optimization solver. It provides methods to set up and solve quadratic programming problems by defining variables, constraints, and coefficients. This class is an integral part of the optimization codebase, allowing users to interact with Gurobi's functionalities in a structured way. It manages the configuration parameters for the optimization process and handles the parsing of problem data.

## Variables
- **_disp**: A boolean that indicates whether to display output from the optimization process.
- **_parameters**: A dictionary that holds configuration parameters for the Gurobi solver, such as output settings and non-convex optimization options.
- **_variables**: A list that stores instances of the `Variable` class, representing the decision variables in the optimization problem.
- **_constraints**: A list of functions that represent the constraints for the optimization problem.
- **_linearCoefficients**: An array of doubles that holds the coefficients for the linear part of the objective function.
- **_quadraticCoefficients**: A two-dimensional array of doubles that holds the coefficients for the quadratic part of the objective function.

## Functions
- **GurobiOptimizer(bool disp = false)**: Constructor that initializes a new instance of the `GurobiOptimizer` class. It sets the display option and initializes the parameters, variables, and constraints.

- **Disp**: A property that gets or sets the display option for the optimizer. When set, it updates the corresponding parameter in `_parameters`.

- **IsGurobiInstalled()**: A static method that checks if Gurobi is installed. For this implementation, it always returns true.

- **GetCompatibilityMsg(Dictionary<string, object> problem)**: Returns a message indicating compatibility with the provided optimization problem. In this case, it returns an empty string, indicating compatibility with any valid quadratic program.

- **InvokeMethod(string methodName, params object[] parameters)**: Invokes a method by name with the provided parameters. It throws an exception if the method is not found.

- **GetProperty(string propertyName)**: Retrieves the value of a property by name. It throws an exception if the property is not found.

- **SetProperty(string propertyName, object value)**: Sets the value of a property by name. It throws an exception if the property is not found.

- **Solve(Dictionary<string, object> problem)**: Main method that takes a dictionary representing the optimization problem, checks compatibility, parses the problem data, and initiates the optimization process. Returns an `OptimizationResult`.

- **ParseProblem(Dictionary<string, object> problem)**: Parses the input problem data, extracts variable names, bounds, coefficients, and constraints, and populates the internal structures.

- **Optimize()**: Initiates the optimization process, simulates the optimization, and returns the result as an `OptimizationResult`.

- **SolveOptimization()**: Simulates the optimization process by calculating a simple midpoint solution for the decision variables based on their bounds.

- **CalculateObjective(double[] solution)**: Computes the objective value based on the provided solution. It calculates contributions from both linear and quadratic components.

### Inner Classes
- **Variable**: Represents a decision variable in the optimization problem, holding its name, lower bound, and upper bound.

- **OptimizationResult**: Represents the result of the optimization process, containing the solution, objective value, variable names, and status. It also includes a `ToString` method for easy representation of the result.