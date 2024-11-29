# NLoptOptimizer

## Overview
The `NLoptOptimizer` class is designed to facilitate optimization tasks using various algorithms provided by the NLopt library. It allows users to minimize a given objective function while adhering to specified bounds and settings. The class integrates with the broader codebase by providing a structured way to perform optimization, encapsulating the complexity of different optimization strategies under a unified interface.

## Variables
- `_options`: A dictionary that holds configuration options for the optimizer, including the maximum number of evaluations.
- `_optimizerNames`: A dictionary mapping `NLoptOptimizerType` values to their corresponding string representations, representing different optimization algorithms.
- `_defaultMaxEvals`: An integer that defines the default maximum number of evaluations for the optimizer, set to 1000.

## Functions
- `NLoptOptimizer(int maxEvals = 1000)`: Constructor that initializes the optimizer with a user-defined maximum number of evaluations. It also populates the `_optimizerNames` dictionary with available optimizer types.

- `Dictionary<string, string> GetSupportLevel()`: Returns a dictionary indicating the support level of various features, such as gradients and bounds, for the optimizer.

- `Dictionary<string, object> Settings`: A property that retrieves the current settings of the optimizer, particularly the maximum number of evaluations.

- `OptimizerResult2 Minimize(Func<double[], double> objectiveFunction, double[] initialPoint, List<(double?, double?)> bounds = null)`: The main method for minimizing the provided objective function. It accepts the function to minimize, an initial point, and optional bounds. It performs the optimization and returns an `OptimizerResult2` object containing the results.

- `protected virtual NLoptOptimizerType GetNloptOptimizer()`: A protected method that must be implemented by subclasses to specify which NLopt optimizer to use. It currently throws a `NotImplementedException`.

- `private double[] Optimize(string optimizerName, double[] initialPoint, double[] lowerBounds, double[] upperBounds, Func<double[], double> objectiveFunction, object maxEvals)`: A private method that simulates the optimization process using the specified optimizer. It currently contains mock logic that simply adjusts the initial point.