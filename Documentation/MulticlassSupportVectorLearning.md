# MulticlassSupportVectorLearning

## Overview
The `MulticlassSupportVectorLearning` class is designed to facilitate the training of a multiclass support vector machine (SVM) using pairwise classification. It leverages a specified kernel and a model of type `MulticlassSupportVectorMachine` to perform the training process. This class fits within the broader context of machine learning implementations in the codebase, specifically focusing on the multiclass classification problem by breaking it down into binary classification tasks.

## Variables

- **kernel**: An instance of type `TKernel`, which represents the kernel function used for the SVM. It defines how the input data is transformed in the feature space.
  
- **model**: An instance of `MulticlassSupportVectorMachine<TKernel, TInput>`, which holds the collection of binary SVMs for each pair of classes.

- **Learner**: A function that takes a tuple of class identifiers and a `SupportVectorMachine` instance, returning an instance of `ISupervisedLearning`. This function is responsible for creating the learning algorithm used to train the binary SVM for the given class pair.

## Functions

- **MulticlassSupportVectorLearning(TKernel kernel)**: Constructor that initializes the `MulticlassSupportVectorLearning` instance with a specified kernel. It sets up a default learner that uses sequential minimal optimization for training.

- **MulticlassSupportVectorLearning(MulticlassSupportVectorMachine<TKernel, TInput> machine)**: Constructor that initializes the instance with an existing multiclass SVM model. It sets up a learner that retrieves the specific binary SVM for the provided class pair.

- **Train(TInput[] inputs, int[] outputs)**: This method trains the multiclass SVM model using the provided input data and corresponding output labels. It identifies unique classes, initializes the model, generates pairs of classes, filters the input data for each pair, and trains a binary SVM for each pair.

- **Predict(TInput input)**: This method predicts the class label for a given input using the trained multiclass SVM model. It aggregates votes from all binary SVMs to determine the most likely class.

- **GenerateClassPairs(HashSet<int> classes)**: A private method that generates all unique pairs of class identifiers from the provided set of classes. It yields each pair as a tuple.

- **FilterInputsForPair(TInput[] inputs, int[] outputs, int class1, int class2, out bool[] pairOutputs)**: A private method that filters the input data to only include samples belonging to the specified class pair. It also outputs a boolean array indicating whether each sample corresponds to class1.

The `MulticlassSupportVectorLearning` class is a key component in implementing multiclass classification using support vector machines, enabling the breakdown of complex classification tasks into simpler binary problems.