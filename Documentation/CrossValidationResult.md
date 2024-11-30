# CrossValidationResult.cs

## Overview
The `CrossValidationResult` class is designed to store and manage the results of k-Fold Cross-Validation for a machine learning model. This class is integral to the EdgeLoreMachineLearning codebase as it encapsulates the statistical outcomes of training and validation processes, allowing for efficient data handling and analysis. The class also provides functionality to save and load these results in a JSON format, facilitating data persistence and retrieval.

## Variables
- **Settings**: An instance of the `CrossValidation` class that holds the configuration settings for the cross-validation process.
- **Training**: An instance of `CrossValidationStatistics` that contains statistical data related to the training phase of the model.
- **Validation**: An instance of `CrossValidationStatistics` that contains statistical data related to the validation phase of the model.
- **Models**: A list of models of type `TModel` that were utilized during the cross-validation.
- **Tag**: An optional object that can be used to store additional information or metadata related to the cross-validation result.

## Functions
- **CrossValidationResult(CrossValidation owner, List<CrossValidationFoldResult<TModel>> models)**: Constructor that initializes a new instance of `CrossValidationResult`. It takes a `CrossValidation` instance and a list of `CrossValidationFoldResult` instances, calculates training and validation statistics, and populates the corresponding properties.

- **Save(string path)**: Serializes the `CrossValidationResult` instance into JSON format and writes it to a specified file path. This function allows for saving the results for later use.

- **Load(string path)**: Static method that reads a JSON file from a specified path and deserializes it into a `CrossValidationResult<TModel>` instance. If the file does not exist, it throws a `FileNotFoundException`.

## CrossValidationFoldResult Class
Additionally, the `CrossValidationFoldResult` class represents the results of a single fold during the cross-validation process. It contains several properties that track the performance metrics of the model for both training and validation phases.

### Variables
- **Model**: The instance of the model being evaluated.
- **TrainingValue**: The performance metric (e.g., accuracy) achieved during the training phase.
- **TrainingVariance**: The variance of the training performance metric, indicating the variability of the results.
- **TrainingCount**: The number of training samples used in the fold.
- **ValidationValue**: The performance metric achieved during the validation phase.
- **ValidationVariance**: The variance of the validation performance metric.
- **ValidationCount**: The number of validation samples used in the fold.

### Constructor
- **CrossValidationFoldResult(TModel model, double trainingValue, double trainingVariance, int trainingCount, double validationValue, double validationVariance, int validationCount)**: Initializes a new instance of `CrossValidationFoldResult` with the specified model and performance metrics for both training and validation phases.