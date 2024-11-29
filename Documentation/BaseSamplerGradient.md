# BaseSamplerGradient

## Overview
The `BaseSamplerGradient` class serves as an abstract foundation for implementing gradient computation workflows in a dynamic sampling environment. It is designed to facilitate the processing of gradient circuits, parameter values, and associated options. This class is intended to be extended by other classes that provide specific implementations for computing gradients. The class manages the lifecycle of a dynamic sampler and caches gradient circuits to optimize performance.

## Variables
- **Sampler**: An object representing the dynamic sampler used for gradient computations. It is initialized in the constructor and cannot be null.
- **gradientCircuitCache**: A dictionary that caches gradient circuits keyed by their unique identifiers. This helps in avoiding redundant computations for the same circuit.
- **defaultOptions**: An object that holds the default gradient options. It is initialized to a new instance of `GradientOptions` if no options are provided during instantiation.

## Functions
- **BaseSamplerGradient(object sampler, object options = null)**: Constructor that initializes the `Sampler` and sets up the `gradientCircuitCache`. It also assigns default options if none are provided.

- **object Run(List<object> circuits, List<List<float>> parameterValues, List<List<object>> parameters = null)**: Main method to execute the gradient computation workflow. It validates input arguments, preprocesses the circuits and parameters, computes gradients, and then postprocesses the results.

- **protected abstract object ComputeGradients(List<object> circuits, List<List<float>> parameterValues, List<List<object>> parameters)**: An abstract method that must be implemented by derived classes to define how gradients are computed based on the provided circuits and parameters.

- **private Tuple<List<object>, List<List<float>>, List<List<object>>> Preprocess(List<object> circuits, List<List<float>> parameterValues, List<List<object>> parameters)**: Prepares the input data for gradient computation by processing circuits and their associated parameters. It caches gradient circuits if they have not been processed before.

- **private object Postprocess(object rawResults, List<object> originalCircuits, List<List<float>> parameterValues, List<List<object>> parameters)**: Processes the raw gradient results to prepare the final output. It structures the gradients and metadata for each circuit.

- **private void ValidateArguments(List<object> circuits, List<List<float>> parameterValues, List<List<object>> parameters)**: Validates the input arguments to ensure that the number of circuits matches the number of parameter sets and that parameters correspond correctly to their circuits.

- **private static T GetProperty<T>(object obj, string propertyName)**: A utility method that dynamically retrieves a property value from an object using reflection. It throws an exception if the property is not found.

- **private static T InvokeMethod<T>(object obj, string methodName, params object[] parameters)**: A utility method that dynamically invokes a method on an object using reflection. It throws an exception if the method is not found.

- **private static Type GetTypeByName(string typeName)**: A utility method that retrieves a type by its name from the currently loaded assemblies. It throws an exception if the type is not found.