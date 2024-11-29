# ESCH Optimizer Script

## Overview
The `ESCH` script defines an optimizer class that extends the `NLoptOptimizer` base class. Its main function is to implement the Efficient Stochastic Covariance Hessian (ESCH) optimization algorithm, which is used for minimizing a specified objective function. The script also includes a `Program` class that demonstrates how to instantiate the `ESCH` optimizer and use it to minimize a simple objective function: the sum of the squares of the input variables. This script is part of a larger codebase that likely deals with numerical optimization tasks.

## Variables
- `maxEvals`: An integer that specifies the maximum number of evaluations allowed for the optimizer. It is initialized with a default value of 1000 but can be set to a different value when creating an instance of the `ESCH` class.
- `optimizer`: An instance of the `ESCH` class, representing the optimizer that will perform the minimization.
- `result`: An object that holds the results of the optimization, including the optimized variable values (`X`), the function value at the optimized point (`Fun`), and the number of function evaluations (`Nfev`).

## Functions
### ESCH(int maxEvals)
- **Description**: Constructor for the `ESCH` class that initializes the optimizer with a specified maximum number of evaluations. It calls the base class constructor to set this value.

### NLoptOptimizerType GetNloptOptimizer()
- **Description**: A protected override method that returns the specific optimizer type for the `ESCH` class. In this case, it returns `NLoptOptimizerType.GN_ESCH`, indicating the use of the ESCH algorithm.

### static void Main(string[] args)
- **Description**: The entry point of the program. It demonstrates the usage of the `ESCH` optimizer by creating an instance, specifying an objective function (sum of squares), providing an initial guess for the variables, and defining the bounds for the optimization. It then executes the optimization and prints the results to the console.