# ADMMOptimizer

## Overview
The `ADMMOptimizer` class implements the Alternating Direction Method of Multipliers (ADMM) optimization algorithm. This class is designed to facilitate the optimization process by managing parameters and internal states necessary for the algorithm's execution. It allows users to set and retrieve parameters, manage the internal state, and execute the optimization process through the `Optimize` method. This class serves as a core component of a larger codebase focused on optimization tasks, providing a structured approach to handling various optimization parameters and states.

## Variables

- **_parameters**: A `Dictionary<string, object>` that stores the parameters used for the optimization process. Some default parameters include:
  - `RhoInitial`: Initial value for the penalty parameter (default: 10000.0).
  - `FactorC`: A scaling factor for the constraints (default: 100000.0).
  - `Beta`: A parameter that influences convergence (default: 1000.0).
  - `MaxIter`: Maximum number of iterations allowed (default: 10).
  - `Tol`: Tolerance level for convergence (default: 1e-4).
  - `MaxTime`: Maximum time allowed for optimization (default: double.PositiveInfinity).
  - `ThreeBlock`: Flag indicating if three-block updates are used (default: true).
  - `VaryRho`: Indicates how to vary the penalty parameter (default: 0).
  - `TauIncr`: Factor by which to increase `Rho` (default: 2.0).
  - `TauDecr`: Factor by which to decrease `Rho` (default: 2.0).
  - `MuRes`: Parameter for residual update (default: 10.0).
  - `MuMerit`: Parameter for merit function (default: 1000.0).
  - `WarmStart`: Flag to indicate if warm starting is used (default: false).

- **_state**: A `Dictionary<string, object>` that maintains the internal state of the optimizer, including:
  - `Rho`: Current value of the penalty parameter (initially set to `RhoInitial`).
  - `Iteration`: Current iteration number (initially set to 0).
  - `ElapsedTime`: Time elapsed during the optimization process (initially set to 0.0).
  - `Residual`: Current residual value (initially set to 100).
  - `DualResidual`: Current dual residual value (initially set to 0.0).
  - `Solution`: A list to store the solution values (initially an empty list).

## Functions

- **ADMMOptimizer(Dictionary<string, object> parameters = null)**: Constructor that initializes the optimizer with a set of parameters. If no parameters are provided, it defaults to a predefined set of parameters.

- **object GetParameter(string parameterName)**: Retrieves the value of a specified parameter. Throws a `MissingMemberException` if the parameter does not exist.

- **void SetParameter(string parameterName, object value)**: Updates the value of a specified parameter. Throws a `MissingMemberException` if the parameter does not exist.

- **object GetState(string stateName)**: Retrieves the value of a specified internal state. Throws a `MissingMemberException` if the state does not exist.

- **void SetState(string stateName, object value)**: Updates the value of a specified internal state. Throws a `MissingMemberException` if the state does not exist.

- **object InvokeMethod(string methodName, object[] parameters)**: Invokes a private method of the class by its name, passing the specified parameters. Throws a `MissingMethodException` if the method does not exist.

- **void Optimize()**: Executes the optimization process. It iterates until the maximum number of iterations is reached or the residual falls below the specified tolerance. During each iteration, it updates the iteration state and the penalty parameter.

- **private void UpdateIterationState()**: Updates the iteration count and simulates convergence by reducing the residual value by 10%.

- **private void UpdateRho()**: Adjusts the penalty parameter `Rho` based on the specified strategy (either by a fixed percentage or based on residual values).

- **override string ToString()**: Returns a string representation of the current parameters and state of the optimizer, useful for debugging and logging purposes.