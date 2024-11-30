# SplitSetStatistics

## Overview
The `SplitSetStatistics` class is designed to encapsulate summary statistics for a validation trial involving a machine learning model. It provides essential information about the model's performance, including the performance statistic, variance, sample size, and standard deviation. This class is useful in the context of machine learning experiments where models are evaluated based on their predictive performance over a split dataset.

Additionally, the `SplitSetStatistics2` class is a non-generic version of `SplitSetStatistics`, allowing for the creation of summary statistics without specifying a model type. It also includes a static method to facilitate the creation of `SplitSetStatistics` instances with a specified model type.

## Variables

- **Model**: 
  - Type: `TModel`
  - Description: The machine learning model generated during the validation trial.

- **Value**: 
  - Type: `double`
  - Description: The performance statistic value gathered during the validation run.

- **Variance**: 
  - Type: `double`
  - Description: The variance of the performance statistic during the validation run.

- **Size**: 
  - Type: `int`
  - Description: The number of samples used to compute the performance statistic.

- **StandardDeviation**: 
  - Type: `double`
  - Description: The standard deviation of the performance statistic, calculated as the square root of the variance.

- **Tag**: 
  - Type: `object`
  - Description: A user-defined tag for additional information associated with the statistics.

## Functions

### SplitSetStatistics(TModel model, int size, double value, double variance)
- **Description**: Constructor that initializes a new instance of the `SplitSetStatistics` class with the specified model, sample size, performance statistic value, and variance.

### SplitSetStatistics2(object model, int size, double value, double variance)
- **Description**: Constructor that initializes a new instance of the `SplitSetStatistics2` class, which is a non-generic version of `SplitSetStatistics`, with the specified parameters.

### Create<TModel>(TModel model, int size, double value, double variance)
- **Description**: Static method that creates and returns a new instance of `SplitSetStatistics<TModel>`, allowing for the specification of the model type. This method simplifies the instantiation of `SplitSetStatistics` for different model types.