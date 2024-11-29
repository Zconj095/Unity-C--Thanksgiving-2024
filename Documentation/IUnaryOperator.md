# ReflectiveUnaryOperator.cs

## Overview
The `ReflectiveUnaryOperator` class implements the `IUnaryOperator` interface, which defines a common structure for unary fuzzy operations. This class allows for the dynamic invocation of methods from the `Mathf` class in Unity, enabling various fuzzy unary operations (such as NOT, VERY, LITTLE) to be executed based on the method name provided at runtime. This design provides flexibility in applying different mathematical operations to fuzzy membership values without hardcoding specific methods into the class.

## Variables
- `private readonly string methodName`: This variable stores the name of the method to be invoked from the `Mathf` class. It is initialized through the constructor and is used to dynamically call the corresponding mathematical operation.

## Functions
- `public ReflectiveUnaryOperator(string methodName)`: This is the constructor for the `ReflectiveUnaryOperator` class. It initializes the `methodName` variable with the name of the method that will be invoked later.

- `public float Evaluate(float membership)`: This method is part of the `IUnaryOperator` interface implementation. It takes a fuzzy membership value (a float between 0 and 1) as input and uses reflection to find and invoke the specified method in the `Mathf` class. If the method is found, it returns the result of the operation applied to the `membership` value. If the method is not found, it throws an `InvalidOperationException` indicating that the specified method does not exist.