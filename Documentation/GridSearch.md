# GridSearch

## Overview
The `GridSearch` class provides a systematic approach to automatically tune parameters for machine learning models using a grid search procedure. It allows users to define a range of parameter values and then evaluates different combinations of these parameters to identify the best-performing model based on specified evaluation criteria. This functionality is essential for optimizing machine learning models, ensuring they perform well on given datasets.

## Variables

- **bestPerformance**: A double that holds the best performance score found during the grid search. Initialized to `double.MaxValue` to ensure any actual performance score will be lower.
  
- **bestModel**: A variable of type `TModel` that stores the best model identified during the grid search process. It is initialized to the default value of `TModel`.

- **parameterCombinations**: A list of dictionaries that contains all possible combinations of parameter values generated from the provided ranges.

## Functions

- **Values<T>(params T[] values)**: 
  - Creates a grid search range of parameter values. This function takes an array of parameter values and returns a `GridSearchRange<T>` object that contains these values.

- **PerformSearch<TModel, TInput, TOutput>(Dictionary<string, GridSearchRange> ranges, Func<Dictionary<string, object>, TModel> createModel, Func<TModel, TInput[], TOutput[], double> evaluateModel, TInput[] inputs, TOutput[] outputs)**:
  - Executes the grid search over the specified parameter ranges to find the best model. It takes in a dictionary of parameter ranges, a function to create the model, a function to evaluate the model's performance, and the input/output data arrays. It returns a tuple containing the best model and its corresponding performance score.

- **GenerateParameterCombinations(Dictionary<string, GridSearchRange> ranges)**:
  - Generates all possible combinations of parameters from the given ranges. It takes a dictionary of parameter ranges and returns a list of dictionaries, where each dictionary represents a unique combination of parameters.

- **GenerateCombinationsRecursive(List<string> keys, List<object[]> values, Dictionary<string, object> current, List<Dictionary<string, object>> combinations, int depth)**:
  - A private method that recursively generates all parameter combinations. It takes the keys of the parameter ranges, their corresponding values, the current combination being built, a list to store all combinations, and the current depth level of recursion. When the depth matches the number of keys, it adds the current combination to the list of combinations.