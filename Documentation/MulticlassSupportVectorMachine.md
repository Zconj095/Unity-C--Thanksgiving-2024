# MulticlassSupportVectorMachine

## Overview
The `MulticlassSupportVectorMachine` class implements a one-against-one multi-class Kernel Support Vector Machine (SVM) classifier specifically designed for use within the Unity game engine. This classifier is capable of handling multiple classes and utilizes a specified kernel function to compute the decision boundaries between classes. The class serves as a core component of the EdgeLoreMachineLearning namespace, providing the functionality to train and classify data points based on their features.

## Variables

- `models`: An array of objects that holds the SVM models for each pair of classes. It is structured as a two-dimensional array where `models[i][j]` represents the model that distinguishes class `i` from class `j`.

- `kernelType`: A `Type` object that represents the type of the kernel function used in the SVM. This is determined by the `kernelTypeName` provided during the instantiation of the class.

- `kernelMethod`: A `MethodInfo` object that represents the `Compute` method of the specified kernel type. This method is invoked to calculate the distance between input data and the decision boundary.

- `NumberOfInputs`: An integer representing the number of input features that the classifier expects. This is set during the initialization of the class.

- `NumberOfClasses`: An integer representing the total number of classes that the classifier can distinguish between. This is also set during the initialization of the class.

## Functions

- **Constructor: `MulticlassSupportVectorMachine(int inputs, int classes, string kernelTypeName)`**
  - Initializes a new instance of the `MulticlassSupportVectorMachine` class. It sets the number of inputs and classes, retrieves the specified kernel type, and initializes the SVM models. If the kernel type or the `Compute` method is not found, it logs an error.

- **`void InitializeMachines()`**
  - Sets up the SVM models for each pair of classes. It creates instances of the kernel type for each combination of classes (i.e., `models[i][j]` where `i` and `j` are distinct). This prepares the classifier for making predictions.

- **`int Compute(float[] input, out float output)`**
  - Accepts an input vector and computes the predicted class based on the trained models. It checks if the input vector size matches the expected number of inputs. It iterates through all pairs of classes and invokes the kernel's `Compute` method to determine the distance to the decision boundary. It returns the index of the predicted class and outputs the distance.

- **`object Clone()`**
  - Creates and returns a deep copy of the current `MulticlassSupportVectorMachine` instance. It clones each SVM model in the `models` array by invoking their respective `Clone` methods, ensuring that the cloned instance maintains the same structure and state as the original.