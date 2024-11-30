# EdgeLoreMachineLearning

## Overview
The `EdgeLoreMachineLearning` script defines a set of interfaces for implementing supervised learning algorithms specifically for Support Vector Machines (SVM). These interfaces facilitate the learning process by providing methods for training models based on various input types and output formats. This modular design allows for flexibility and scalability in machine learning applications, enabling developers to create models that can handle different data types and learning scenarios.

## Variables
- **Model**: 
  - Type: `SupportVectorMachine<TKernel, TInput>`
  - Description: Represents the support vector machine model that is being learned. This property is used to access or modify the current model during the learning process.

## Functions

### ILinearSupportVectorMachineLearning
- Inherits from:
  - `ISupervisedLearning<SupportVectorMachine, double[], double>`
  - `ISupervisedLearning<SupportVectorMachine, double[], int>`
  - `ISupervisedLearning<SupportVectorMachine, double[], bool>`
  - `ISupportVectorMachineLearning`
- Description: This interface serves as a unified entry point for supervised learning algorithms that can handle multiple output types (double, int, bool) using a linear SVM.

### ISupportVectorMachineLearning
- Inherits from: `ISupportVectorMachineLearning<double[]>`
- Description: This interface extends the SVM learning algorithms to support generic input types, allowing for a more versatile implementation of SVM learning.

### ISupportVectorMachineLearning<TInput>
- Description: This generic interface defines methods for SVM learning algorithms that can operate on any specified input type `TInput`.
  
#### Methods:
- `double Run()`
  - Description: Runs the learning algorithm and returns the error rate of the model. This method is marked as obsolete and suggests using the `Learn` method instead.
  
- `double Run(bool computeError)`
  - Description: Runs the learning algorithm and optionally computes the error after training. This method is also marked as obsolete and suggests using the `Learn` method instead.
  
- `TModel Learn<TModel>(TInput[] inputs, double[] outputs, double[] weights = null) where TModel : SupportVectorMachine`
  - Description: Learns the model using the provided training inputs and outputs. Optionally, sample weights can be specified. This method returns the learned model of type `TModel`.

### ISupportVectorMachineLearning<TKernel, TInput>
- Description: This generic interface allows for SVM learning algorithms that specify both a kernel type `TKernel` and an input type `TInput`.

#### Methods:
- `SupportVectorMachine<TKernel, TInput> Model { get; set; }`
  - Description: Gets or sets the support vector machine being learned.

- `double Run()`
  - Description: Similar to the method in `ISupportVectorMachineLearning<TInput>`, this method runs the learning algorithm and returns the error rate of the model, but is marked as obsolete.

- `double Run(bool computeError)`
  - Description: Similar to the method in `ISupportVectorMachineLearning<TInput>`, this method runs the learning algorithm and optionally computes the error, but is marked as obsolete.

- `SupportVectorMachine<TKernel, TInput> Learn(TInput[] inputs, double[] outputs, double[] weights = null)`
  - Description: Learns the model using the provided inputs and outputs, returning the learned model. This method allows for the specification of weights for the samples.