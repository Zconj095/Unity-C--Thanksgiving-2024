# LBFGSBOptimizer

## Overview
The `LBFGSBOptimizer` class implements the Limited-memory Broyden-Fletcher-Goldfarb-Shanno (L-BFGS-B) optimization algorithm, which is used for minimizing a scalar function of multiple variables. This optimizer is particularly useful for functions that are expensive to evaluate and have a large number of dimensions. The class allows users to set various parameters such as maximum iterations, tolerance levels, and verbosity for debugging. The `Minimize` method is the core function that performs the optimization by iteratively updating the parameters based on the provided objective and gradient functions.

## Variables
- `MaxFunctionEvaluations`: The maximum number of function evaluations allowed during optimization.
- `MaxIterations`: The maximum number of iterations allowed for the optimization process.
- `Tolerance`: The convergence tolerance level for the gradient; if the maximum gradient is below this value, the optimization stops.
- `Epsilon`: A small value used for approximating the gradient when the analytical gradient is not provided.
- `Verbose`: A boolean flag that, when set to true, enables logging of the optimization process for debugging purposes.

### Constants
- `MinStepSize`: The minimum step size allowed during the line search to prevent excessively small steps.

## Functions
- **LBFGSBOptimizer**: Constructor that initializes the optimizer with specified parameters for maximum function evaluations, iterations, tolerance, epsilon, and verbosity.

- **Minimize**: 
  - Parameters:
    - `objectiveFunction`: A function that computes the value of the objective to minimize.
    - `gradientFunction`: A function that computes the gradient of the objective function.
    - `initialPoint`: An array of initial parameter values for the optimization.
    - `bounds`: A tuple containing lower and upper bounds for the parameters.
  - Returns: A tuple containing the optimized parameters and the final objective value.
  - Description: This method performs the optimization process, iteratively updating the parameters until convergence criteria are met or maximum evaluations/iterations are reached.

- **ApproximateGradient**: 
  - Parameters:
    - `objectiveFunction`: The objective function used to compute the gradient.
    - `point`: The current point at which to evaluate the gradient.
  - Returns: An array representing the approximated gradient at the specified point.
  - Description: This method calculates the gradient using finite differences if the analytical gradient is not provided.

- **ComputeSearchDirection**: 
  - Parameters:
    - `gradient`: The current gradient vector.
    - `s`: A matrix storing previous steps.
    - `y`: A matrix storing previous gradients.
    - `rho`: An array storing the curvature values.
    - `memoryIndex`: The current index for storing new values in memory.
  - Returns: An array representing the search direction for the next step.
  - Description: This method computes the search direction based on the gradient and the stored values from previous iterations.

- **LineSearch**: 
  - Parameters:
    - `objectiveFunction`: The objective function used for evaluating points during the line search.
    - `currentPoint`: The current point in the optimization process.
    - `gradient`: The current gradient vector.
    - `direction`: The search direction to follow.
  - Returns: A float representing the optimal step size found during the line search.
  - Description: This method performs a line search to determine the best step size along the search direction.

- **UpdateMemory**: 
  - Parameters:
    - `previousPoint`: The previous point in the optimization.
    - `currentPoint`: The current point in the optimization.
    - `gradient`: The current gradient vector.
    - `s`: A matrix storing previous steps.
    - `y`: A matrix storing previous gradients.
    - `rho`: An array storing the curvature values.
    - `memoryIndex`: The current index for storing new values in memory.
  - Description: This method updates the memory of previous steps and gradients, which is essential for the L-BFGS-B algorithm.

- **DotProduct**: 
  - Parameters:
    - `a`: The first vector.
    - `b`: The second vector.
  - Returns: A float representing the dot product of the two vectors.
  - Description: This method calculates the dot product of two vectors.

### Extensions
- **ArrayExtensions**: A static class that provides utility methods for working with two-dimensional float arrays.
  - **GetRow**: Retrieves a specified row from a two-dimensional array and returns it as a one-dimensional array.
  - **SetRow**: Sets the values of a specified row in a two-dimensional array based on a given one-dimensional array.