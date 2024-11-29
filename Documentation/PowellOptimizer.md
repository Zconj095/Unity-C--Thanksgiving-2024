# PowellOptimizer

## Overview
The `PowellOptimizer` class is a Unity MonoBehaviour that implements the Powell optimization algorithm. This algorithm is used for finding the minimum of a multivariable function by performing sequential one-dimensional minimizations along each vector direction. The class allows users to set various optimization parameters and provides a method to minimize a given objective function, returning the results of the optimization process. This class fits within a larger codebase that likely involves numerical optimization tasks, particularly in game development or simulation contexts.

## Variables
- `_options`: A dictionary that holds various optimization parameters such as maximum iterations, maximum function evaluations, display options, and tolerance levels.

## Functions
- **Constructor**: 
  - `PowellOptimizer(int? maxiter = null, int maxfev = 1000, bool disp = false, double xtol = 0.0001, double? tol = null)`
    - Initializes the optimizer with specified options for maximum iterations, maximum function evaluations, display settings, step tolerance, and other parameters.

- **SetOption**: 
  - `void SetOption(string key, object value)`
    - Updates the value of an existing option in the `_options` dictionary. If the key does not exist, it throws an `ArgumentException`.

- **GetOption**: 
  - `object GetOption(string key)`
    - Retrieves the value of a specified option from the `_options` dictionary. If the key does not exist, it throws an `ArgumentException`.

- **InvokePrivateMethod**: 
  - `object InvokePrivateMethod(string methodName, object[] parameters)`
    - Invokes a private method of the class using reflection. If the method does not exist, it throws an `ArgumentException`.

- **Minimize**: 
  - `OptimizerResult Minimize(Func<double[], double> objectiveFunction, double[] initialPoint, Tuple<double, double>[]? bounds = null)`
    - Executes the Powell optimization algorithm on the provided objective function, starting from an initial point. It iterates until it reaches the maximum number of iterations or function evaluations, or until convergence is detected. Returns an `OptimizerResult` containing the optimized point, the function value at that point, the number of iterations, and the number of function evaluations.

- **OptimizeOneDirection**: 
  - `private double[] OptimizeOneDirection(Func<double[], double> objectiveFunction, double[] point, int direction, double xtol)`
    - Performs one-dimensional optimization along a specified direction from a given point. It evaluates the objective function at various steps in that direction and updates the point based on the best found value.

## Nested Class
- **OptimizerResult**: 
  - A class to encapsulate the results of the optimization process. It contains:
    - `double[] X`: The optimized point in the search space.
    - `double Fun`: The function value at the optimized point.
    - `int Iterations`: The total number of iterations performed during the optimization.
    - `int FunctionEvaluations`: The total number of times the objective function was evaluated.