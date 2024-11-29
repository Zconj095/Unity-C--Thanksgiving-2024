# NelderMeadOptimizer

## Overview
The `NelderMeadOptimizer` class implements the Nelder-Mead optimization algorithm, which is a method for unconstrained optimization using a simplex approach. This optimizer is designed to minimize a given objective function within Unity, making it suitable for various applications in game development and simulations where optimization is required. The class allows users to specify parameters such as maximum iterations, maximum function evaluations, convergence tolerance, and verbosity for debugging purposes.

## Variables

- **MaxIterations**: (int) The maximum number of iterations allowed for the optimization process. This limits how long the optimizer will attempt to find a solution.
  
- **MaxFunctionEvaluations**: (int) The maximum number of times the objective function can be evaluated during the optimization process. This ensures that the optimizer does not exceed a certain computational cost.

- **Tolerance**: (float) The convergence tolerance level. This is used to determine when the optimization has sufficiently converged to a solution based on the variance of function values.

- **Verbose**: (bool) A flag that enables or disables verbose output. When set to true, the optimizer will log detailed debugging information to the console.

## Functions

- **NelderMeadOptimizer(int maxIterations = 1000, int maxFunctionEvaluations = 1000, float tolerance = 1e-4f, bool verbose = false)**: 
  Initializes a new instance of the `NelderMeadOptimizer` class with specified parameters for maximum iterations, function evaluations, convergence tolerance, and verbosity.

- **(float[] OptimizedParams, float FinalValue) Minimize(Func<float[], float> objectiveFunction, float[] initialGuess, float stepSize = 1.0f)**: 
  Minimizes the provided objective function starting from an initial guess and using a specified step size for the initial simplex. It returns a tuple containing the optimized parameters and the final value of the objective function.

- **private float[][] InitializeSimplex(float[] initialGuess, float stepSize)**: 
  Initializes the simplex structure around the initial guess by creating a set of points, each offset by a specified step size.

- **private float[] ComputeCentroid(float[][] simplex, int dimensions)**: 
  Computes the centroid of the simplex, excluding the worst point. This centroid is used in the reflection and expansion steps of the optimization process.

- **private float[] Reflect(float[] worst, float[] centroid, float coefficient)**: 
  Reflects the worst point of the simplex around the centroid using a specified coefficient. This is a crucial step in the Nelder-Mead algorithm for exploring better parameter configurations.

- **private bool Converged(float[][] simplex, float[] functionValues)**: 
  Checks if the optimization process has converged based on the variance of the function values. It returns true if the standard deviation of the values is below the specified tolerance, indicating that further optimization is unlikely to yield significant improvements.