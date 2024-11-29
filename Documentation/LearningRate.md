# LearningRate Class Documentation

## Overview
The `LearningRate` class is designed to manage and generate learning rate values for machine learning algorithms. It can accept a single float value, an enumerable collection of float values, or a function that returns an enumerator of float values. This flexibility allows users to specify how learning rates are generated during the training process. The class provides methods to retrieve the current learning rate and to handle exceptions effectively.

## Variables
- **_generator**: An enumerator of type `IEnumerator<float>`. This variable is responsible for iterating through the learning rate values based on the input provided during the instantiation of the `LearningRate` class.
- **_currentValue**: A nullable float (`float?`) that stores the current learning rate value. It is updated each time a new value is retrieved from the generator.

## Functions
- **LearningRate(object learningRate)**: Constructor that initializes the learning rate generator based on the type of input provided. It supports a single float value, an enumerable of float values, or a function that returns an enumerator. If the input type is invalid, it throws an `ArgumentException`.

- **float Current**: A property that retrieves the current learning rate value. If no value is available, it throws an `InvalidOperationException`.

- **float Send()**: This method advances the generator to the next learning rate value and updates `_currentValue`. If the generator has no more values to provide, it throws an `InvalidOperationException`.

- **void Throw(Type exceptionType, string message = null)**: This method allows for throwing a specified exception type with an optional message. If the provided type is not an exception type, it throws an `ArgumentException`. If the message is not provided, it creates an exception instance without a message.

- **static IEnumerable<float> Constant(float value)**: A private static method that returns an infinite enumerable of a constant float value. This method is used when a single float value is specified as the learning rate.