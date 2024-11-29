# ISRES Class Documentation

## Overview
The `ISRES` class extends the `NLoptOptimizer` class, providing a specific implementation of the ISRES optimization algorithm. It allows for the optimization of functions with a specified maximum number of evaluations. This class utilizes reflection to dynamically invoke methods and access or modify properties, enhancing its flexibility and usability within the broader codebase.

## Variables
- **maxEvals**: An integer that defines the maximum number of evaluations allowed for the optimization process. It is set during the instantiation of the `ISRES` class and is passed to the base class `NLoptOptimizer`.

## Functions
- **ISRES(int maxEvals = 1000)**: Constructor for the `ISRES` class. It initializes the optimizer with a specified maximum number of evaluations, defaulting to 1000 if no value is provided.

- **protected override NLoptOptimizerType GetNloptOptimizer()**: This method overrides a base class method to return the specific optimizer type for ISRES (`NLoptOptimizerType.GN_ISRES`).

- **public object InvokeMethod(string methodName, object[] parameters)**: This method uses reflection to dynamically invoke a method by its name, passing an array of parameters. If the method is not found, it throws a `MissingMethodException`.

- **public object GetProperty(string propertyName)**: This method uses reflection to dynamically access a property by its name. If the property does not exist, it throws a `MissingMemberException`.

- **public void SetProperty(string propertyName, object value)**: This method uses reflection to dynamically set the value of a property by its name. If the property is not found, it throws a `MissingMemberException`.