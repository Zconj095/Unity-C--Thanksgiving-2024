# CrossValidationValues<TModel>

## Overview
The `CrossValidationValues<TModel>` class is designed to store and manage the results of a machine learning model during the cross-validation process. Cross-validation is a technique used to evaluate the performance of a model by partitioning the data into subsets, training the model on some subsets, and validating it on others. This class encapsulates the training and validation values, their variances, and the fitted model itself, providing a structured way to access these metrics. It is a crucial part of the EdgeLoreMachineLearning codebase, facilitating the evaluation of model performance and helps in selecting the best model based on its cross-validation results.

## Variables
- **Model**: This variable holds the fitted model of type `TModel`. It represents the machine learning model that has been trained and validated.
- **TrainingValue**: A `double` that stores the performance metric (e.g., accuracy) obtained from the training dataset.
- **ValidationValue**: A `double` that stores the performance metric obtained from the validation dataset.
- **TrainingVariance**: A `double` that represents the variance of the training values, providing insight into the variability of the training performance.
- **ValidationVariance**: A `double` that represents the variance of the validation values, indicating the variability of the validation performance.

## Functions
- **CrossValidationValues(double trainingValue, double validationValue)**: This constructor initializes a new instance of `CrossValidationValues` with specified training and validation values. It sets the training and validation metrics while leaving variances uninitialized.

- **CrossValidationValues(double trainingValue, double trainingVariance, double validationValue, double validationVariance)**: This constructor initializes a new instance of `CrossValidationValues` with specified training and validation values along with their respective variances. It allows for a complete representation of the model's performance.

- **CrossValidationValues(TModel model, double trainingValue, double validationValue)**: This constructor initializes a new instance of `CrossValidationValues` with a fitted model and specified training and validation values. It allows the user to associate a model instance with its performance metrics.

- **CrossValidationValues(TModel model, double trainingValue, double trainingVariance, double validationValue, double validationVariance)**: This constructor initializes a new instance of `CrossValidationValues` with a fitted model, training and validation values, and their variances. It provides a comprehensive view of the model's performance.

- **static CrossValidationValues<TModel> Create(TModel model, double trainingValue, double trainingVariance, double validationValue, double validationVariance)**: This static factory method creates a new instance of `CrossValidationValues` with a fitted model and specified training and validation values along with their variances. It simplifies the creation process.

- **static CrossValidationValues<TModel> Create(TModel model, double trainingValue, double trainingVariance)**: This static factory method creates a new instance of `CrossValidationValues` with a fitted model and specified training values and variance, while setting the validation values and variance to zero. It provides a convenient way to create instances when validation data is not available.