# TNCOptimizer

## Overview
The `TNCOptimizer` class is designed to perform optimization using a truncated Newton method with a conjugate gradient approach. This method is particularly useful for minimizing a given objective function, which is often used in machine learning and numerical analysis. The optimizer allows for the specification of various parameters, such as maximum iterations, tolerances, and display options, making it flexible for different optimization scenarios. It fits within a larger codebase that may involve machine learning models or mathematical computations where optimization is required.

## Variables

- `_maxIterations`: An integer that defines the maximum number of iterations the optimizer will perform.
- `_display`: A boolean indicating whether to display the optimization progress in the console.
- `_accuracy`: A double that sets the desired accuracy level for the optimization process.
- `_fTolerance`: A double that sets the function tolerance for convergence.
- `_xTolerance`: A double that sets the parameter tolerance for convergence.
- `_gTolerance`: A double that defines the gradient tolerance for convergence.
- `_epsilon`: A double used for numerical gradient computation, representing a small perturbation.
- `_tolerance`: A nullable double that can be used to set a custom convergence tolerance.

## Functions

### `public TNCOptimizer(...)`
The constructor initializes the optimizer with specified parameters. It sets default values for tolerances and other options if they are not provided.

### `public OptimizerResult Minimize(...)`
This method performs the optimization process. It takes the following parameters:
- `objectiveFunction`: A function that accepts an array of doubles and returns a double representing the loss to be minimized.
- `initialPoint`: An array of doubles representing the starting point for the optimization.
- `gradientFunction`: An optional function that computes the gradient of the objective function.
- `bounds`: An optional array of tuples that define the lower and upper bounds for each parameter.

The method returns an instance of `OptimizerResult` containing the optimized parameters, final loss, and evaluation counts.

### `private double[] ComputeNumericalGradient(...)`
This method calculates the numerical gradient of the objective function at the given parameters. It uses finite differences to estimate the gradient by perturbing each parameter slightly and evaluating the loss function.

### `private double[] ComputeStep(...)`
This method computes the step direction for the optimization process based on the gradient. It scales the gradient by the `_xTolerance` to determine how much to adjust each parameter.

### `private double[] ApplyBounds(...)`
This method applies the specified bounds to the parameters after computing the step direction. It ensures that the updated parameters remain within the defined limits by clamping them to the specified bounds.

### `public class OptimizerResult`
This inner class encapsulates the results of the optimization process. It contains the following properties:
- `Parameters`: The optimized parameters as an array of doubles.
- `Loss`: The final loss value after optimization.
- `FunctionEvaluations`: The number of times the objective function was evaluated.
- `GradientEvaluations`: The number of times the gradient function was evaluated.
- `Iterations`: The total number of iterations performed during optimization.

The `OptimizerResult` constructor initializes these properties with the provided values.