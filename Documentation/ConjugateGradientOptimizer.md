# ConjugateGradientOptimizer

## Overview
The `ConjugateGradientOptimizer` class implements the Conjugate Gradient (CG) method, which is an iterative algorithm used for solving optimization problems. This optimizer is designed to minimize a given objective function by iteratively updating the solution parameters based on the computed gradients. It is particularly useful in scenarios where the objective function is large and complex, as it efficiently converges to an optimal solution. This class fits within a codebase that may involve optimization tasks, such as machine learning, physics simulations, or any computational problems requiring minimization.

## Variables
- `maxIterations`: An integer representing the maximum number of iterations the optimizer will perform before stopping.
- `gradientTolerance`: A float that defines the tolerance level for the gradient norm, which determines when the optimization process should consider convergence.
- `epsilon`: A small float value used for approximating the gradient in numerical computations.
- `displayConvergence`: A boolean that, when set to true, enables the display of convergence messages in the console for each iteration.

## Functions
- **Constructor**: 
  - `ConjugateGradientOptimizer(int maxIterations = 20, float gradientTolerance = 1e-5f, float epsilon = 1.49e-8f, bool displayConvergence = false)`
    - Initializes an instance of the `ConjugateGradientOptimizer` with specified parameters for maximum iterations, gradient tolerance, epsilon for numerical gradient calculations, and whether to display convergence messages.

- **Minimize**: 
  - `(float[] OptimizedParams, float FinalValue) Minimize(Func<float[], float> objectiveFunction, float[] initialPoint, Func<float[], float[]> gradientFunction = null)`
    - Minimizes the provided objective function starting from an initial guess for the solution. It optionally accepts a gradient function for more accurate gradient calculations. Returns the optimized parameters and the minimum value of the objective function.

- **LineSearch**: 
  - `private float LineSearch(Func<float[], float> objectiveFunction, float[] x, float[] direction)`
    - Performs a backtracking line search to determine the optimal step size (alpha) for updating the solution parameters during the optimization process.

- **ComputeNumericalGradient**: 
  - `private float[] ComputeNumericalGradient(Func<float[], float> objectiveFunction, float[] x)`
    - Computes the numerical gradient of the objective function at a given point using central differences.

- **Norm**: 
  - `private static float Norm(float[] vector)`
    - Calculates the Euclidean norm (magnitude) of a given vector.

- **DotProduct**: 
  - `private static float DotProduct(float[] vector1, float[] vector2)`
    - Computes the dot product of two vectors.

- **NegateVector**: 
  - `private static float[] NegateVector(float[] vector)`
    - Returns a new vector that is the negation of the input vector.

- **ScaleVector**: 
  - `private static float[] ScaleVector(float[] vector, float scalar)`
    - Scales each element of the vector by a given scalar and returns the resulting vector.

- **AddVectors**: 
  - `private static float[] AddVectors(float[] vector1, float[] vector2)`
    - Adds two vectors element-wise and returns the resulting vector.