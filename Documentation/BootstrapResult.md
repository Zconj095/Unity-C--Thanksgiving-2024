# BootstrapResult Class Documentation

## Overview
The `BootstrapResult` class is designed to store the results of a Bootstrap computation within the `EdgeLoreMachineLearning` namespace. It encapsulates performance statistics for both training and validation sets, as well as the 0.632 bootstrap estimate. This class interacts with the `Bootstrap` class to generate and manage the results of the bootstrap runs, making it an essential component of the machine learning process in this codebase.

## Variables

- **Settings**: 
  - Type: `Bootstrap`
  - Description: The Bootstrap object that generated the results stored in this instance.

- **Training**: 
  - Type: `CrossValidationStatistics`
  - Description: Performance statistics calculated for the training dataset.

- **Validation**: 
  - Type: `CrossValidationStatistics`
  - Description: Performance statistics calculated for the validation dataset.

- **Estimate**: 
  - Type: `double`
  - Description: The 0.632 bootstrap estimate, calculated based on the mean performance of both training and validation sets.

## Functions

- **BootstrapResult(Bootstrap owner, BootstrapValues[] models)**: 
  - Description: Constructor that initializes a new instance of the `BootstrapResult` class. It takes a `Bootstrap` object and an array of `BootstrapValues` models to compute the training and validation statistics, as well as the bootstrap estimate.

- **void Save(string path)**: 
  - Description: Saves the current instance of `BootstrapResult` to a specified file in JSON format. It serializes the object and writes it to the provided file path.

- **static BootstrapResult Load(string path)**: 
  - Description: Loads a `BootstrapResult` instance from a specified file. It reads the JSON content from the file, deserializes it, and returns the corresponding `BootstrapResult` object. If the file does not exist, it throws a `FileNotFoundException`.