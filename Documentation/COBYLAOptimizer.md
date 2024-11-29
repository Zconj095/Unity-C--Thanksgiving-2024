# COBYLAOptimizer

## Overview
The `COBYLAOptimizer` class implements the Constrained Optimization By Linear Approximation (COBYLA) algorithm. This optimizer is designed to solve constrained optimization problems where the derivative of the objective function is unknown. It iteratively adjusts a set of parameters to minimize a given objective function while adhering to specified constraints. This class fits within a broader codebase that may involve numerical optimization tasks, particularly in scenarios where traditional gradient-based methods are not applicable.

## Variables
- `maxIterations`: An integer that defines the maximum number of iterations the optimizer will perform before stopping.
- `rhoBegin`: A float representing the initial step size for the optimization process.
- `tolerance`: A float that sets the threshold for convergence; the optimization process will stop when changes fall below this value.
- `displayConvergence`: A boolean that indicates whether to print convergence messages to the console during the optimization process.

## Functions
- **COBYLAOptimizer(int maxIterations = 1000, float rhoBegin = 1.0f, float tolerance = 1e-6f, bool displayConvergence = false)**
  - Constructor that initializes the optimizer with specified parameters for iterations, step size, tolerance, and convergence display option.

- **(float[] OptimizedParams, float FinalValue) Minimize(Func<float[], float> objectiveFunction, float[] initialPoint, List<Func<float[], float>> constraints)**
  - This method minimizes the provided objective function starting from an initial guess, while respecting the given constraints. It returns the optimized parameters and the final value of the objective function.

- **float ComputeConstraintViolation(float[] point, List<Func<float[], float>> constraints)**
  - A private method that calculates the total constraint violation for a given point. It sums the violations for all constraints, where a violation occurs when a constraint returns a negative value.

- **void AdjustForConstraints(float[] point, List<Func<float[], float>> constraints, float rho)**
  - A private method that adjusts the current point to satisfy the constraints using a linear approximation. If a constraint is violated, it modifies the point by moving it in the direction that reduces the violation based on the step size `rho`.