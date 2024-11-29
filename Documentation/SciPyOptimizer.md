# SciPyOptimizer

## Overview
The `SciPyOptimizer` class is designed to perform optimization tasks similar to those found in the SciPy library, tailored for use within a Unity environment. This class allows users to minimize a given objective function using various optimization methods, while also supporting features like gradient calculations and bounding constraints. It serves as a bridge between Unity and optimization techniques, enabling developers to integrate complex mathematical optimization into their games or applications.

## Variables
- **_method**: A string representing the optimization method to be used, converted to lowercase for consistency.
- **_options**: A dictionary that holds various optional parameters for the optimization process, such as maximum iterations and learning rates.
- **_maxEvalsGrouped**: An integer indicating the maximum number of evaluations that can be grouped together during optimization, ensuring that it is at least 1.
- **_supportLevels**: A dictionary that describes the support levels for features like gradients and bounds, indicating whether they are supported, ignored, or required.

## Functions
- **SciPyOptimizer(string method, Dictionary<string, object> options = null, int maxEvalsGrouped = 1)**: Constructor that initializes the optimizer with a specified method, optional parameters, and a maximum number of evaluations grouped.

- **Dictionary<string, string> GetSupportLevels()**: Returns the support levels for various features (gradient, bounds, initial point) as a dictionary.

- **OptimizerResult Minimize(Func<double[], double> objectiveFunction, double[] initialPoint, Func<double[], double[]>? gradientFunction = null, Tuple<double, double>[]? bounds = null)**: Executes the optimization process to minimize the given objective function, starting from an initial point. It optionally accepts a gradient function and bounds for the optimization.

- **OptimizerResult ExecuteOptimization(Func<double[], double> objectiveFunction, double[] initialPoint, Func<double[], double[]>? gradientFunction, Tuple<double, double>[]? bounds)**: Contains the core logic for performing the optimization loop, evaluating the objective function, updating the current point, and checking for convergence.

- **double[] ComputeNumericalGradient(Func<double[], double> objectiveFunction, double[] x)**: Computes the numerical gradient of the objective function at a given point using finite differences.

- **bool IsGradientSupported()**: Checks if the specified optimization method supports gradient calculations.

- **bool IsBoundsSupported()**: Checks if the specified optimization method supports bounding constraints.

- **class OptimizerResult**: A nested class that serves as a data structure to hold the results of the optimization, including the optimized parameters, the function value at that point, the number of iterations, and the number of function evaluations.

- **object InvokePrivateMethod(string methodName, object[] parameters)**: Allows invocation of private methods within the class using reflection, enabling access to non-public functionality.