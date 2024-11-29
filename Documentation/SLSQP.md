# SLSQP

## Overview
The `SLSQP` class implements the Sequential Least Squares Programming (SLSQP) optimization algorithm. It is designed to minimize a given objective function while optionally satisfying equality and inequality constraints. This class fits into the codebase as a numerical optimization tool, allowing developers to find optimal solutions for various mathematical problems through iterative adjustments of input parameters.

## Variables
- `_options`: A dictionary that stores various configuration options for the optimizer, such as maximum iterations, display options, function tolerance, and epsilon for numerical gradient computation.
- `_maxEvalsGrouped`: An integer representing the maximum number of evaluations that can be grouped together during optimization.
- `_tol`: A nullable double that specifies the tolerance for convergence criteria.

## Functions
- **Constructor `SLSQP(int maxIter = 100, bool disp = false, double ftol = 1e-06, double? tol = null, double eps = 1.4901161193847656e-08, Dictionary<string, object>? options = null, int maxEvalsGrouped = 1)`**
  - Initializes the SLSQP optimizer with specified options for maximum iterations, display preferences, function tolerance, numerical precision, and additional options.

- **`OptimizerResult Minimize(Func<double[], double> objectiveFunction, double[] initialPoint, Func<double[], double[]>? gradientFunction = null, Tuple<double, double>[]? bounds = null, Func<double[], double[]>? equalityConstraints = null, Func<double[], double[]>? inequalityConstraints = null)`**
  - Executes the minimization process. It takes an objective function, an initial point, optional gradient function, bounds, and constraints. It iteratively updates the current point based on computed gradients and checks if constraints are satisfied.

- **`private double[] ComputeNumericalGradient(Func<double[], double> objectiveFunction, double[] x)`**
  - Computes the numerical gradient of the objective function at a given point `x` using finite differences.

- **`private double[] ComputeStep(double[] gradient)`**
  - Calculates the step to take in the direction of the negative gradient, scaled by the function tolerance.

- **`private bool AreConstraintsSatisfied(double[] constraints, bool isEquality = true)`**
  - Checks if the provided constraints are satisfied according to the specified type (equality or inequality), returning a boolean result.

- **`public class OptimizerResult`**
  - A nested class that holds the results of the optimization, including the optimized parameters, the final loss value, and the number of iterations performed.

- **`public object InvokePrivateMethod(string methodName, object[] parameters)`**
  - Allows invoking a private method of the SLSQP class using reflection, given the method name and parameters. This can be useful for testing or debugging purposes.