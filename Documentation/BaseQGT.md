# BaseQGT

## Overview
The `BaseQGT` class serves as an abstract base class for computing Quantum Geometric Tensor (QGT) matrices from a set of circuits and their corresponding parameter values. It provides a framework for validation, preprocessing, computation, and postprocessing of QGT data, while allowing derived classes to implement specific computation methods. This class is integral to the broader codebase as it establishes the foundational structure for any QGT computation, ensuring consistency and reusability across different implementations.

## Variables
- `Estimator`: An object that serves as the estimator for the QGT computation. It is initialized through the constructor and cannot be null.
- `PhaseFix`: A boolean indicating whether phase fixing is applied during computations. It defaults to true.
- `DerivativeType`: A string that specifies the type of derivative to be used in computations. It defaults to "COMPLEX".
- `qgtCircuitCache`: A dictionary that caches circuits related to QGT computations to avoid redundant processing.
- `gradientCircuitCache`: A dictionary that caches gradient circuits, facilitating efficient access during computations.

## Functions
- `BaseQGT(object estimator, bool phaseFix = true, string derivativeType = "COMPLEX")`: Constructor that initializes the `Estimator`, `PhaseFix`, and `DerivativeType` properties, and sets up the caches for circuits.

- `object Run(List<object> circuits, List<List<float>> parameterValues, List<List<object>> parameters = null)`: The main method that orchestrates the QGT computation. It validates the input arguments, preprocesses the circuits and parameters, computes the QGT using an abstract method, and postprocesses the results.

- `protected abstract object ComputeQGT(List<object> circuits, List<List<float>> parameterValues, List<List<object>> parameters)`: An abstract method that must be implemented by derived classes to compute the QGT based on the provided circuits and parameters.

- `private Tuple<List<object>, List<List<float>>, List<List<object>>> Preprocess(List<object> circuits, List<List<float>> parameterValues, List<List<object>> parameters)`: This method preprocesses the input circuits and parameters, creating gradient circuits and organizing data for computation.

- `private object Postprocess(object rawResults, List<object> originalCircuits, List<List<float>> parameterValues, List<List<object>> parameters)`: This method takes the raw results of the QGT computation and organizes them into a structured format, including the QGT matrices and associated metadata.

- `private void ValidateArguments(List<object> circuits, List<List<float>> parameterValues, List<List<object>> parameters)`: Validates the input arguments to ensure that the number of circuits matches the number of parameter sets and that each circuit's parameters align with the provided values.

- `private object CreateGradientCircuit(object circuit)`: Creates a new `GradientCircuit` instance based on the provided circuit using reflection to access its constructor.

- `private T GetProperty<T>(object obj, string propertyName)`: A helper method that retrieves the value of a specified property from an object using reflection.

- `private T InvokeMethod<T>(object obj, string methodName, params object[] args)`: A helper method that invokes a specified method on an object using reflection and returns the result.

### Inner Helper Class
- `public class GradientCircuit`: Represents a gradient circuit, containing the circuit itself and its parameters. It initializes its properties based on the provided circuit object.

- `private T GetProperty<T>(object obj, string propertyName)`: A method similar to the one in `BaseQGT`, retrieving property values for the `GradientCircuit` class using reflection.