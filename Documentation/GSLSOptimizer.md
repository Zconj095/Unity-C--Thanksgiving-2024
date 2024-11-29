# GSLSOptimizer

## Overview
The `GSLSOptimizer` class implements a Gaussian-Smoothed Line Search (GSLS) optimization algorithm. This optimizer is designed to minimize a given objective function by approximating gradients through Gaussian-smoothed sampling. It is particularly useful in scenarios where the objective function may be noisy or expensive to evaluate. The optimizer iteratively adjusts its parameters based on sampled points around a current estimate, refining its search for the optimal solution.

## Variables

- **MaxIterations**: The maximum number of iterations the optimizer will perform during the optimization process.
- **MaxEvaluations**: The maximum number of evaluations of the objective function allowed.
- **SamplingRadius**: The radius used to generate sample points around the current optimization point.
- **SampleSizeFactor**: A factor that determines the number of sample points to generate based on the dimensionality of the problem.
- **InitialStepSize**: The starting step size for the optimization process.
- **MinStepSize**: The minimum allowable step size to prevent the optimizer from making excessively small updates.
- **StepSizeMultiplier**: A factor by which the step size is adjusted during the optimization process.
- **ArmijoParameter**: A parameter used in the Armijo condition to determine whether to accept or reject a step.
- **MinGradientNorm**: The minimum norm of the gradient required to continue the optimization process.

- **evaluationCount**: A private variable that keeps track of the number of evaluations made to the objective function during the optimization.

## Functions

- **GSLSOptimizer**: 
  - Initializes an instance of the `GSLSOptimizer` with specified parameters or default values.

- **Minimize**: 
  - Takes an objective function, an initial point, and bounds for the decision variables as input. It performs the optimization process, returning the optimized parameters and the final objective value.

- **GenerateSampleSet**: 
  - Generates a set of sample points around the current optimization point on a sphere, ensuring that the points remain within the specified bounds.

- **RandomOnSphere**: 
  - Produces a random point on the surface of a unit sphere in n-dimensional space, which is used to generate directions for sampling.

- **ApproximateGradient**: 
  - Computes an approximation of the gradient based on sampled points and their corresponding objective values, aiding in the optimization process.