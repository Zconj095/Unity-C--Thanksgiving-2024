# BootstrapValues Class

## Overview
The `BootstrapValues` class is designed to encapsulate the results of training and validation for a single bootstrap iteration within the EdgeLore Machine Learning framework. This class serves as a data structure to store important metrics related to model performance, including training and validation values, their variances, and any additional user-defined information. It plays a crucial role in managing the results of machine learning experiments, allowing for better analysis and understanding of model behavior during training and validation.

## Variables

- `TrainingValue`: 
  - **Type**: `double`
  - **Description**: Represents the training value for the model. This value indicates how well the model has learned from the training data.

- `TrainingVariance`: 
  - **Type**: `double`
  - **Description**: Represents the variance of the training value, if available. This metric provides insight into the stability and reliability of the training value.

- `ValidationValue`: 
  - **Type**: `double`
  - **Description**: Represents the validation value for the model. This value indicates how well the model performs on unseen data.

- `ValidationVariance`: 
  - **Type**: `double`
  - **Description**: Represents the variance of the validation value, if available. Similar to training variance, it indicates the consistency of the validation value.

- `Tag`: 
  - **Type**: `object`
  - **Description**: A user-defined property that can hold any additional information relevant to the bootstrap iteration. This is useful for attaching metadata or contextual information.

## Functions

- **Constructor `BootstrapValues(double trainingValue, double validationValue)`**: 
  - **Description**: Initializes a new instance of the `BootstrapValues` class with specified training and validation values. This constructor does not include variance metrics, making it suitable for cases where only the core performance metrics are needed.

- **Constructor `BootstrapValues(double trainingValue, double trainingVariance, double validationValue, double validationVariance)`**: 
  - **Description**: Initializes a new instance of the `BootstrapValues` class with specified training and validation values, as well as their respective variances. This constructor is useful for scenarios where comprehensive performance metrics are required for deeper analysis.