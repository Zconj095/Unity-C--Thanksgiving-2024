# EstimatorGradientResult

## Overview
The `EstimatorGradientResult` class is designed to encapsulate the results of a gradient estimation process. It serves as a structured way to hold the gradients of expectation values, additional metadata related to the job, and the runtime options used during the job's execution. This class plays a crucial role in the larger codebase by providing a clear and organized way to manage the results of computational tasks, particularly in scenarios involving statistical or machine learning estimations.

## Variables

- **Gradients**: 
  - Type: `IReadOnlyList<float[,]>`
  - Description: This property holds the gradients of the expectation values calculated during the estimation process. It is a read-only list of two-dimensional arrays of floats, representing the computed gradients.

- **Metadata**: 
  - Type: `IReadOnlyList<Dictionary<string, object>>`
  - Description: This property contains additional information about the job that produced the gradients. It is a read-only list where each item is a dictionary mapping string keys to object values, allowing for flexible storage of various types of metadata.

- **Options**: 
  - Type: `GradientOptions`
  - Description: This property stores the runtime options that were utilized during the execution of the job. It provides context regarding the configuration and parameters used for the gradient estimation process.

## Functions

- **EstimatorGradientResult(IReadOnlyList<float[,]> gradients, IReadOnlyList<Dictionary<string, object>> metadata, GradientOptions options)**: 
  - Description: This is the constructor for the `EstimatorGradientResult` class. It initializes a new instance of the class with the specified gradients, metadata, and options. The constructor ensures that none of the parameters are null, throwing an `ArgumentNullException` if any of them are not provided. This guarantees that the object is always in a valid state after creation.