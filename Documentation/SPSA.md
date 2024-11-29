# SPSA Script Documentation

## Overview
The `SPSA` (Simultaneous Perturbation Stochastic Approximation) class is designed to perform optimization using the SPSA algorithm. This algorithm is particularly useful for optimizing functions that are expensive to evaluate or have noisy outputs. The `Minimize` method takes an objective function and an initial point as inputs, iteratively adjusting the parameters to minimize the loss returned by the objective function. This class fits within a broader codebase that may involve machine learning, simulations, or other computational tasks requiring optimization.

## Variables

- `_maxIter`: (int) The maximum number of iterations allowed for the optimization process.
- `_blocking`: (bool) A flag indicating whether to block iterations that do not improve the loss beyond a specified threshold.
- `_allowedIncrease`: (double?) The maximum allowed increase in loss for the next iteration before blocking occurs.
- `_trustRegion`: (bool) A flag indicating whether to enforce a trust region for parameter updates.
- `_learningRate`: (double) The rate at which parameters are updated during optimization.
- `_perturbation`: (double) The scale of perturbations applied to the parameters during sampling.
- `_lastAvg`: (int) The number of recent parameter sets to average for the final output.
- `_resamplings`: (int) The number of times to resample the perturbations for each iteration.
- `_perturbationDims`: (int?) The dimensions of perturbations, if specified.
- `_secondOrder`: (bool) A flag indicating whether to use second-order information (Hessian) in the optimization process.
- `_regularization`: (double) A small value added to the diagonal of the Hessian for stability.
- `_hessianDelay`: (int) The number of iterations to wait before using the Hessian for updates.

- `_nfev`: (int) A counter for the number of function evaluations performed.
- `_smoothedHessian`: (double[][]) A smoothed version of the Hessian matrix used in optimization.

## Functions

- `SPSA(int maxIter = 100, bool blocking = false, double? allowedIncrease = null, bool trustRegion = false, double learningRate = 0.1, double perturbation = 0.01, int lastAvg = 1, int resamplings = 1, int? perturbationDims = null, bool secondOrder = false, double regularization = 0.01, int hessianDelay = 0)`: Constructor that initializes the SPSA optimizer with specified parameters.

- `double[] AverageVectors(double[][] vectors)`: Computes the average of a set of vectors.

- `OptimizerResult Minimize(Func<double[], double> objectiveFunction, double[] initialPoint)`: Executes the SPSA optimization process. It takes an objective function and an initial point, returning an `OptimizerResult` containing the optimized parameters, final loss, function evaluations, and iterations.

- `(double, double[], double[][]) SamplePoint(Func<double[], double> objectiveFunction, double[] parameters, double[] delta1, double[] delta2 = null)`: Samples a point in the parameter space to compute the loss, gradient, and Hessian (if applicable).

- `double[][] InitializeHessian(int size)`: Initializes a Hessian matrix of a given size, starting as an identity matrix.

- `double[] GenerateRandomPerturbation(int size)`: Generates a random perturbation vector of specified size.

- `double[][] RegularizeHessian(double[][] hessian)`: Adds a regularization term to the diagonal of the Hessian matrix to improve numerical stability.

- `double[][] AverageMatrices(double[][][] matrices)`: Computes the average of a set of matrices.

- `double[] SolveLinearSystem(double[][] matrix, double[] vector)`: Solves a linear system of equations represented by the given matrix and vector.

- `class OptimizerResult`: A nested class that encapsulates the result of the optimization process, including optimized parameters, final loss, the number of function evaluations, and the number of iterations.