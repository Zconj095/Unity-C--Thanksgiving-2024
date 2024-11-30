# BaseSupportVectorClassification

## Overview
The `BaseSupportVectorClassification` class serves as an abstract foundation for implementing Support Vector Machine (SVM) classifiers in the context of machine learning. It provides a structure for training an SVM model using input data and class labels, while also allowing for customization through kernel functions and complexity parameters. This class fits within the broader `EdgeLoreMachineLearning` namespace, which is likely focused on machine learning implementations for various applications.

## Variables
- `useKernelEstimation`: A boolean indicating whether kernel estimation should be used.
- `useComplexityHeuristic`: A boolean flag to determine if a heuristic for complexity should be employed.
- `useClassLabelProportion`: A boolean that specifies if class label proportions should be used in weight adjustment.
- `hasKernelBeenSet`: A boolean indicating whether a kernel has been explicitly set.
- `complexity`: A double representing the complexity parameter for the SVM.
- `positiveWeight`: A double indicating the weight assigned to positive class examples.
- `negativeWeight`: A double indicating the weight assigned to negative class examples.
- `Cpositive`: A double that holds the calculated cost for positive examples.
- `Cnegative`: A double that holds the calculated cost for negative examples.
- `kernel`: An instance of type `TKernel`, representing the kernel function used for learning.
- `model`: An instance of type `TModel`, representing the SVM model being learned.
- `Inputs`: An array of type `TInput` that holds the training inputs.
- `Outputs`: An array of integers that holds the training outputs (class labels).
- `C`: An array of doubles representing the cost values for each input vector.

## Functions
- `Learn(TInput[] x, bool[] y, double[] weights = null)`: This method initiates the learning process by validating inputs, creating the kernel and SVM model if not already set, and running the internal learning algorithm. It returns the trained SVM model.
  
- `InnerRun()`: An abstract method that must be implemented by derived classes to execute the main learning algorithm for the SVM.

- `Create(int inputs, TKernel kernel)`: An abstract method that must be implemented by derived classes to create a new instance of the SVM model.

- `CreateDefaultKernel()`: A virtual method that can be overridden to provide a default kernel if none is specified. Throws an exception if not overridden.

- `GetNumberOfInputs(TInput[] x)`: A private method that retrieves the number of inputs in the dataset. It throws exceptions if the dataset is empty or if it cannot determine the input dimensions.

- `ConvertLabelsToBinary(bool[] y)`: A private method that converts boolean class labels to binary integers, returning an array of integers.

- `InitializeComplexityWeights(bool[] y, double[] weights)`: A private method that initializes the complexity weights for training based on the input data and any provided weights.

- `CountTrue(bool[] array)`: A private method that counts the number of `true` values in a boolean array and returns the count.

- `AdjustClassWeights(int positives, int negatives)`: A private method that adjusts class weights based on the ratio of positive to negative examples.

- `EstimateComplexity()`: A private method that estimates the complexity parameter, currently returning a default value of 1.0.

- `ValidateInputs(TInput[] x, bool[] y)`: A private method that checks the validity of the input data, ensuring that inputs and outputs are not null and that they have the same length. Throws exceptions if validation fails.