# OneClassSupportVectorLearning

## Overview
The `OneClassSupportVectorLearning` class implements a one-class Support Vector Machine (SVM) learning algorithm. It is designed to train a model using a set of input data, which is particularly useful for anomaly detection scenarios where the goal is to identify outliers in a dataset. The class takes a kernel function as a parameter, which defines the similarity measure between data points. This class fits within the EdgeLoreMachineLearning namespace, indicating its role in machine learning tasks.

## Variables
- `kernel`: An instance of type `TKernel`, which implements the `OneClassLearningIKernel<TInput>` interface. It defines the kernel function used in the SVM algorithm.
- `nu`: A double value representing the upper bound on the fraction of training errors and a lower bound on the fraction of support vectors. Its default value is 0.5.
- `tolerance`: A double value that sets the tolerance level for the optimization process. Its default value is 0.01.
- `shrinking`: A boolean that indicates whether to use shrinking heuristics during optimization. Its default value is true.
- `alpha`: An array of double values that represent the Lagrange multipliers for each training example.
- `supportVectors`: A list of input data of type `TInput` that are identified as support vectors after training.
- `weights`: A list of double values that correspond to the weights of the support vectors.
- `threshold`: A double value that serves as the decision boundary for classification.

## Functions
- **Constructor: `OneClassSupportVectorLearning(TKernel kernel)`**
  - Initializes a new instance of the `OneClassSupportVectorLearning` class with a specified kernel.

- **Method: `SetNu(double value)`**
  - Sets the value of `nu`. Throws an exception if the value is not between 0 and 1.

- **Method: `SetTolerance(double value)`**
  - Sets the value of `tolerance`. Throws an exception if the value is less than or equal to 0.

- **Method: `SetShrinking(bool value)`**
  - Configures whether to use shrinking heuristics during the optimization process.

- **Method: `Train(TInput[] inputs)`**
  - Trains the one-class SVM using the provided input data. Initializes the `alpha` array, solves the quadratic optimization problem, and extracts support vectors and their corresponding weights.

- **Method: `SolveQuadraticProblem(TInput[] inputs)`**
  - Performs the quadratic optimization to determine the values of `alpha`. It utilizes a `QuadraticOptimizer` to minimize the objective function.

- **Method: `ExtractSupportVectors(TInput[] inputs)`**
  - Extracts the support vectors and their weights from the trained model based on the `alpha` values.

## Related Classes
- **`OneClassSupportVectorMachine<TKernel, TInput>`**
  - Represents the trained one-class SVM model and provides a method for making predictions on new input data.

- **`OneClassLearningIKernel<TInput>`**
  - An interface that defines the contract for kernel functions used in one-class SVMs.

- **`LinearKernel`**
  - Implements the `OneClassLearningIKernel` interface with a linear kernel function.

- **`QuadraticOptimizer`**
  - A utility class that handles the optimization process required for training the SVM. It minimizes the quadratic objective function and calculates the resulting Lagrange multipliers.