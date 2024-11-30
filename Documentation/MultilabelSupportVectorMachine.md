# MultilabelSupportVectorMachine

## Overview
The `MultilabelSupportVectorMachine` class implements a multilabel classification model using support vector machines (SVM). It allows for the initialization of multiple models corresponding to different classes and provides functionality to make decisions and calculate probabilities for input data. This class is designed to work within the `EdgeLoreMachineLearning` namespace, facilitating machine learning tasks in Unity. It uses a scoring function to evaluate inputs and can handle different probability methods for multilabel classification.

## Variables
- `models`: A list of models of type `TModel` that represent the individual classifiers for each class.
- `method`: An instance of `MultilabelProbabilityMethod` that defines the method used to calculate probabilities for multilabel classification.
- `numberOfClasses`: An integer representing the total number of classes the model can classify.
- `cache`: A dictionary that caches scores for previously evaluated inputs to optimize performance.

## Properties
- `Method`: Gets or sets the probability method used by the model.

## Functions
- `MultilabelSupportVectorMachine(int classes, Func<TModel> modelInitializer)`: Constructor that initializes the number of classes, creates a list of models using the provided model initializer function, and initializes the cache.

- `bool[] Decide(TInput input, Func<TInput, int, double> scoringFunction)`: Determines the binary decisions for each class based on the input and a scoring function. It returns an array of booleans indicating whether each class is activated (true) or not (false).

- `double[] Probabilities(TInput input, Func<TInput, int, double> scoringFunction)`: Calculates the probabilities for each class based on the input and a scoring function. The probabilities are computed using the specified method (e.g., PerClass, SumsToOne, SumsToOneWithEmphasisOnWinner) and returns an array of doubles representing the probability for each class.

- `void Reset()`: Clears the cache of the machine and logs a message indicating that the cache has been reset.

- `void Dispose()`: Resets the cache and logs a message indicating that the machine has been disposed of, implementing the IDisposable interface for resource management.