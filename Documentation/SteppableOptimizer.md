# SteppableOptimizer

## Overview
The `SteppableOptimizer` class serves as an abstract base class for implementing optimization algorithms that can be executed in discrete steps. It provides a framework for optimizing a given function starting from an initial point, while allowing for the optional inclusion of gradient information and bounds on the parameters. This class is designed to be extended by specific optimization strategies, enabling developers to create tailored optimization solutions while sharing common functionality.

## Variables

- **State**: An instance of `OptimizerState` that holds the current state of the optimizer, including the current parameters, function, gradient, and counts of function evaluations, gradient evaluations, and iterations.

- **MaxIterations**: An integer that specifies the maximum number of iterations the optimizer is allowed to perform during the optimization process.

## Functions

- **SteppableOptimizer(int maxIterations = 100)**: 
  - Constructor that initializes the optimizer with a specified maximum number of iterations (default is 100).

- **void Start(Func<double[], double> function, double[] initialPoint, Func<double[], double[]>? gradient = null, Tuple<double, double>[]? bounds = null)**: 
  - Initializes the optimizer's state with the provided function, initial point, optional gradient function, and optional bounds.

- **protected abstract void InitializeState(Func<double[], double> function, double[] initialPoint, Func<double[], double[]>? gradient, Tuple<double, double>[]? bounds)**: 
  - An abstract method that must be implemented by derived classes to set up the optimizer's state based on the provided parameters.

- **abstract AskData Ask()**: 
  - An abstract method that must be implemented by derived classes to generate and return the data needed for the next optimization step.

- **abstract void Tell(AskData askData, TellData tellData)**: 
  - An abstract method that must be implemented by derived classes to process the results of an evaluation based on the ask and tell data.

- **abstract TellData Evaluate(AskData askData)**: 
  - An abstract method that must be implemented by derived classes to evaluate the ask data and return the corresponding tell data.

- **void Step()**: 
  - Executes a single optimization step by asking for data, evaluating it, and then telling the optimizer about the results.

- **OptimizerResult Minimize(Func<double[], double> function, double[] initialPoint, Func<double[], double[]>? gradient = null, Tuple<double, double>[]? bounds = null)**: 
  - The main method that starts the optimization process. It initializes the state and continues to step through the optimization until the stopping criteria are met.

- **protected virtual void OnIterationEnd()**: 
  - A virtual method that can be overridden by derived classes to execute additional logic at the end of each iteration.

- **protected virtual bool ShouldContinue()**: 
  - A virtual method that determines whether the optimization process should continue based on the current iteration count and the maximum allowed iterations.

- **abstract OptimizerResult CreateResult()**: 
  - An abstract method that must be implemented by derived classes to create and return the result of the optimization process.

### Nested Classes

- **OptimizerState**: 
  - Contains the current state of the optimizer, including the current parameter values (X), the function to be optimized (Fun), the optional gradient function (Jac), and counters for function evaluations (Nfev), gradient evaluations (Njev), and iterations (Nit).

- **AskData**: 
  - Holds the data required for the optimization step, including the current function values (XFun) and gradient values (XJac).

- **TellData**: 
  - Contains the results of the evaluation step, including the evaluated function value (EvalFun) and the evaluated gradient values (EvalJac).

- **OptimizerResult**: 
  - Represents the final result of the optimization process, including the optimized parameters, loss value, and counts of function and gradient evaluations as well as iterations.