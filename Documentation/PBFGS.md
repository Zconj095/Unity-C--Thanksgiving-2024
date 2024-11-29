# PBFGS

## Overview
The `PBFGS` class is a Unity MonoBehaviour script designed for optimization tasks using a variant of the BFGS (Broyden-Fletcher-Goldfarb-Shanno) algorithm. It allows users to minimize a given objective function by exploring the parameter space using multiple processes. This class is particularly useful for scenarios where optimization is needed, and it integrates seamlessly into the Unity engine, taking advantage of its capabilities for multi-threading and task management.

## Variables
- `_settings`: A dictionary that stores various settings related to the optimization process, including:
  - `maxFun`: Maximum number of function evaluations (default is 1000).
  - `ftol`: Tolerance for the optimization (default is 1e-8).
  - `iprint`: Print level for output (default is -1, meaning no output).
  - `maxProcesses`: The maximum number of processes to use for optimization; defaults to the number of available processors minus one.
  
- `lockObject`: An object used for thread synchronization to ensure that random point generation is thread-safe.

## Functions
- **PBFGS(int maxFun = 1000, double ftol = 1e-8, int iprint = -1, int? maxProcesses = null)**: Constructor that initializes the optimization settings with specified or default values.

- **SetSetting(string name, object value)**: Updates the value of a specified setting if it exists in the `_settings` dictionary. Throws an exception if the setting does not exist.

- **GetSetting(string name)**: Retrieves the value of a specified setting from the `_settings` dictionary. Throws an exception if the setting does not exist.

- **InvokePrivateMethod(string methodName, object[] parameters)**: Invokes a private method of the class using reflection. Throws an exception if the method does not exist.

- **Minimize(Func<double[], double> objectiveFunction, double[] initialPoint, Tuple<double, double>[]? bounds = null)**: Main function for minimizing the given objective function. It runs multiple optimization tasks in parallel based on the specified bounds or generates default bounds if none are provided. Returns the best optimization result.

- **private OptimizerResult RunOptimization(Func<double[], double> objectiveFunction, double[] initialPoint, Tuple<double, double>[]? bounds)**: Executes a single optimization iteration using a simulated L-BFGS-B logic. Returns an `OptimizerResult` containing the optimized parameters and the function value.

- **private double[] GenerateBounds(double value, int size)**: Generates an array of bounds initialized to the specified value and size.

- **private double[] GenerateRandomPointWithinBounds(double[] low, double[] high)**: Generates a random point within specified lower and upper bounds, ensuring thread safety through locking.

## Nested Class
- **OptimizerResult**: A class that holds the result of an optimization run, containing:
  - `X`: The optimized parameters.
  - `Fun`: The value of the objective function at the optimized parameters.
  - `Nfev`: The number of function evaluations performed during the optimization.