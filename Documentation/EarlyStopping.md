# EarlyStopping Script Documentation

## Overview
The `EarlyStopping` class is designed to implement early stopping in machine learning model training. Early stopping is a technique used to prevent overfitting by halting the training process once the model performance stops improving on a validation dataset. This script manages the training iterations, tracks the performance of the model, and stores relevant metrics based on the specified storage mode. It fits within the codebase as a utility for optimizing machine learning model training processes.

## Variables

- **MaxIterations**: An integer representing the maximum number of iterations the training process is allowed to run.
- **Tolerance**: A double value that defines the threshold for determining whether the improvement in performance is significant enough to continue training.
- **History**: A dictionary that maps iteration indices to `CrossValidationValues2<TModel>` instances, storing the performance metrics for each iteration.
- **MinValidationValue**: A key-value pair that holds the iteration index and the corresponding `CrossValidationValues2<TModel>` instance with the minimum validation value observed.
- **MaxValidationValue**: A key-value pair that holds the iteration index and the corresponding `CrossValidationValues2<TModel>` instance with the maximum validation value observed.
- **Mode**: An enumeration value of type `ModelStorageMode` that specifies how models should be stored during training (e.g., all models, only minimum, or only maximum).
- **IterationFunction**: A function delegate that takes an integer (iteration index) and returns a `CrossValidationValues2<TModel>` instance, allowing for custom evaluation logic during each iteration.

## Functions

- **Compute()**: 
  - This method executes the early stopping algorithm. It iterates up to `MaxIterations`, invoking the `IterationFunction` to retrieve performance metrics for each iteration. It checks for convergence based on the `Tolerance` and updates the history and minimum/maximum validation values accordingly. It returns `true` if training converged and `false` if the maximum number of iterations was reached without convergence.

- **UpdateMinAndMaxValidationValues(int iteration, CrossValidationValues2<TModel> value)**:
  - This private method updates the minimum and maximum validation values observed during the training iterations. It takes the current iteration index and the corresponding `CrossValidationValues2<TModel>` instance as parameters. It checks if the current validation value is less than the previously stored minimum or greater than the previously stored maximum, updating the respective key-value pairs if necessary.

- **Clone(bool includeModel)**:
  - This method belongs to the `CrossValidationValues2<TModel>` class and creates a clone of the current instance. It takes a boolean parameter `includeModel` that specifies whether to include the model in the cloned instance or not. If `includeModel` is true and the model is not null, it performs a deep clone of the model; otherwise, it sets the model to its default value.

By providing clear descriptions of variables and functions, this documentation aims to facilitate understanding of the `EarlyStopping` class and its role in managing machine learning model training.