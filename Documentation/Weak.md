# Weak Class Adapter

## Overview
The `Weak` class serves as an adapter for models that do not implement a `.Decide` function directly. It allows the integration of weak classifiers into the EdgeLore Machine Learning framework by utilizing reflection to invoke a specified decision-making method from the model. This class facilitates the decision-making process by providing a way to compute the output based on the model's decision function, which is particularly useful when working with various types of models that may not adhere to a standard interface.

## Variables

- **Model**: 
  - Type: `TModel`
  - Description: Represents the weak decision model that will be used for classification. This is a generic type that allows flexibility in the type of model being utilized.

- **DecisionFunction**: 
  - Type: `MethodInfo`
  - Description: Holds a reference to the decision function method of the model. This method is invoked to compute the classification decision based on the input provided.

## Functions

- **Weak(TModel model, string methodName)**: 
  - Description: Constructor that initializes a new instance of the `Weak` class. It takes a model and the name of the decision function method. It uses reflection to find the specified method in the model. If the model is null or the method cannot be found, it throws an exception.

- **int Compute(double[] input)**: 
  - Description: Computes the classifier decision for a given input vector by invoking the decision function using reflection. It returns an integer label that represents the model's classification decision. If the input is null or the decision function does not return an integer, it throws an exception.

- **bool Decide(double[] input)**: 
  - Description: Computes a binary decision (true/false) based on the model's output. It calls the `Compute` method and returns true if the decision label corresponds to the positive class (greater than zero), and false otherwise.

## Example Weak Model

- **ExampleWeakModel**: 
  - Description: This is a sample implementation of a weak model for testing purposes. It contains a decision function that classifies input based on the sum of its values.

- **int DecisionFunction(double[] input)**: 
  - Description: A sample decision function that classifies the input vector by calculating the sum of its values. It returns +1 if the average of the input values is greater than 0.5, and -1 otherwise. This function is an example of how the `Weak` class can be utilized with a specific model.