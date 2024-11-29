# SamplerGradientResult

## Overview
The `SamplerGradientResult` class is designed to encapsulate the results of a sampling operation that involves gradients of sample probabilities. It serves as a structured way to present the gradients, additional job-related information, and runtime options used during the execution of a sampling job. This class is part of a broader codebase that likely deals with sampling and optimization tasks, providing necessary outputs that can be utilized for further processing and analysis.

## Variables
- **Gradients**: 
  - Type: `IReadOnlyList<IReadOnlyList<Dictionary<int, float>>>`
  - Description: This property holds the gradients of the sample probabilities, structured as a list of lists of dictionaries. Each dictionary maps an integer (likely an index or identifier) to a float value representing the gradient.

- **Metadata**: 
  - Type: `IReadOnlyList<object>`
  - Description: This property contains additional information about the job that generated the gradients. It is stored as a read-only list of objects, allowing for flexible storage of various types of metadata.

- **Options**: 
  - Type: `GradientOptions`
  - Description: This property holds the runtime options used during the execution of the sampling job. It provides configuration settings that may influence how the sampling operation is performed.

## Functions
- **SamplerGradientResult(IReadOnlyList<IReadOnlyList<Dictionary<int, float>>> gradients, IReadOnlyList<object> metadata, GradientOptions options)**:
  - Description: This is the constructor for the `SamplerGradientResult` class. It initializes a new instance of the class with the specified gradients, metadata, and options. It ensures that none of the parameters are null by throwing an `ArgumentNullException` if any of them are not provided.

### GradientOptions Class
- **UseOptimization**:
  - Type: `bool`
  - Description: This property indicates whether optimization should be used during the sampling process. It is set to true by default, suggesting that optimization is generally preferred unless specified otherwise.