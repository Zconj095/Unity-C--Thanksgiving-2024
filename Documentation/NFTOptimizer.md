# NFTOptimizer

## Overview
The `NFTOptimizer` class is designed to minimize a specified objective function using an optimization algorithm. It provides a flexible framework for performing iterative optimization, allowing users to set parameters such as maximum iterations, function evaluations, and verbosity for debugging. This class fits into a larger codebase that may involve numerical optimization tasks, particularly in scenarios such as game development or computational simulations where optimization is necessary.

## Variables
- `MaxIterations`: The maximum number of iterations the optimizer will perform. Default is 1000.
- `MaxFunctionEvaluations`: The maximum number of function evaluations allowed during the optimization process. Default is 1024.
- `ResetInterval`: The interval at which the optimizer recalculates the initial guess (`z0`). Default is 32.
- `Epsilon`: A small constant used to prevent division by zero errors. Default is `1e-32f`.
- `Verbose`: A boolean flag that, when set to true, enables detailed logging of the optimization process for debugging purposes.

## Functions
### Constructor
- `NFTOptimizer(int maxIterations = 1000, int maxFunctionEvaluations = 1024, int resetInterval = 32, float epsilon = 1e-32f, bool verbose = false)`
  - Initializes a new instance of the `NFTOptimizer` class with specified parameters for optimization.

### Minimize
- `(float[] OptimizedParams, float FinalValue) Minimize(Func<float[], float> objectiveFunction, float[] initialGuess)`
  - Minimizes the provided objective function starting from an initial guess for the parameters.
  - **Parameters:**
    - `objectiveFunction`: A function that takes an array of floats (parameters) and returns a float (the function value to minimize).
    - `initialGuess`: An array of floats representing the starting point for the optimization process.
  - **Returns:** A tuple containing:
    - `OptimizedParams`: The optimized parameters after the minimization process.
    - `FinalValue`: The final value of the objective function at the optimized parameters.