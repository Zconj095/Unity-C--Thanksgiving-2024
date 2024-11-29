# OptimizerResult

## Overview
The `OptimizerResult` class is a Unity MonoBehaviour that manages the results of an optimization routine. It provides functionality to store and retrieve various properties related to the optimization process, such as the final point of minimization, function values, gradients, and evaluation counts. This class is designed to facilitate the optimization workflow within a larger codebase, allowing for dynamic property management and numerical gradient computation.

## Variables
- `properties`: A dictionary that stores key-value pairs for various properties related to the optimization process. The keys represent the names of the properties, and the values hold the corresponding data. The properties include:
  - `"x"`: The final point of minimization (array of floats).
  - `"fun"`: The final value of the objective function at the minimum point.
  - `"jac"`: The final gradient of the objective function at the minimum point.
  - `"nfev"`: The total number of function evaluations performed during optimization.
  - `"njev"`: The total number of gradient evaluations performed.
  - `"nit"`: The total number of iterations completed during the optimization process.

## Functions
- `OptimizerResult()`: Constructor that initializes the `properties` dictionary with default values set to `null`.

- `object GetProperty(string propertyName)`: Retrieves the value of a specified property from the `properties` dictionary. Throws an exception if the property does not exist.

- `void SetProperty(string propertyName, object value)`: Sets the value of a specified property in the `properties` dictionary. Throws an exception if the property does not exist.

- `Dictionary<string, object> GetAllProperties()`: Returns a copy of all properties stored in the `properties` dictionary.

- `void PrintProperties()`: Prints all property values to the console, replacing `null` values with the string "null".

- `object InvokeMethod(string methodName, params object[] parameters)`: Invokes a private method dynamically using reflection. Throws an exception if the method does not exist.

- `private void ExamplePrivateMethod()`: An example of a private method that can be invoked dynamically. It logs a message to the console.

- `static float[] GradientNumDiff(float[] xCenter, Func<float[], float> func, float epsilon)`: Computes the numerical gradient of a given function at a specified point using finite differences. Returns an array of gradient values.

- `static Func<float[], float> WrapFunction(Func<float[], float> func, object[] args)`: Wraps a function with additional arguments, allowing for dynamic invocation with injected parameters.

- `string Settings()`: Returns a formatted string representation of the current optimizer settings, including all properties and their values.

- `void Minimize(Func<float[], float> objectiveFunction, float[] initialPoint, float epsilon)`: Simulates the optimization routine by computing the gradient of the objective function at the initial point and updating the properties accordingly. It logs the start and completion of the optimization process.

- `void Start()`: Unity's Start method that initializes the `OptimizerResult` instance. It demonstrates the usage of dynamic properties, performs an example optimization, and invokes a private method dynamically.