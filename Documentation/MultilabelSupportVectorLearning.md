# MultilabelSupportVectorLearning

## Overview
The `MultilabelSupportVectorLearning` class is designed to facilitate the training of a multilabel support vector machine (SVM) model using a specified kernel and input data. It serves as a wrapper around the `MultilabelSupportVectorMachine` class, allowing for the training of multiple binary classifiers, each corresponding to a class in the multilabel dataset. This class fits into the broader codebase by providing a structured approach to handling multilabel classification problems, utilizing the support vector machine algorithm.

## Variables
- `kernel`: An instance of type `TKernel`, which implements the `IKernel<TInput>` interface. This variable holds the kernel function used for the SVM.
- `model`: An instance of `MultilabelSupportVectorMachine<TKernel, TInput>`, which represents the multilabel SVM model being trained.
- `Learner`: A function that takes a tuple containing a class index and a support vector machine instance, and returns an instance of `ISupervisedLearning<SupportVectorMachine<TKernel, TInput>, TInput, bool>`. This function is responsible for creating a learner for each binary SVM.

## Functions
- **Constructor `MultilabelSupportVectorLearning(TKernel kernel)`**
  - Initializes a new instance of the `MultilabelSupportVectorLearning` class using the specified kernel. It sets up the `Learner` function to create a new instance of `SequentialMinimalOptimization` for training.

- **Constructor `MultilabelSupportVectorLearning(MultilabelSupportVectorMachine<TKernel, TInput> machine)`**
  - Initializes a new instance of the `MultilabelSupportVectorLearning` class with an existing multilabel SVM model. The `Learner` function is set up to use the specified machine for training.

- **`Train(TInput[] inputs, int[][] outputs)`**
  - Trains the multilabel SVM model using the provided input data and corresponding binary outputs. It creates a new `MultilabelSupportVectorMachine`, iterates through each class, extracts binary outputs, and trains the corresponding SVM for each class. Returns the trained model.

- **`Predict(TInput input)`**
  - Takes an input instance and uses the trained multilabel SVM model to generate predictions. Returns an array of boolean values indicating the predicted classes for the input.

- **`private bool[] ExtractBinaryOutputs(int[][] outputs, int classIndex)`**
  - A helper method that converts the multi-class outputs into binary outputs for a specific class index. It returns an array of boolean values where `true` indicates the presence of the class and `false` indicates its absence.