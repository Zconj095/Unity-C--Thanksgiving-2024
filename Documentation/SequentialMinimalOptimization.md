# SequentialMinimalOptimization

## Overview
The `SequentialMinimalOptimization` class is a Unity MonoBehaviour that implements the Sequential Minimal Optimization (SMO) algorithm, which is commonly used for training Support Vector Machines (SVMs). This script is part of the `EdgeLoreMachineLearning` namespace and is designed to facilitate the learning process of SVMs by optimizing the parameters based on the provided input data. The class utilizes reflection to inspect its own fields at runtime, allowing for dynamic adjustments and debugging.

## Variables
- **tolerance**: A double value representing the tolerance level for optimization (default is 1e-2).
- **epsilon**: A double value used to determine the threshold for stopping optimization (default is 1e-6).
- **shrinking**: A boolean indicating whether to use shrinking heuristics during optimization.
- **alpha**: An array of doubles representing the Lagrange multipliers for the SVM optimization.
- **activeExamples**: A HashSet of integers that contains the indices of the active training examples.
- **nonBoundExamples**: A HashSet of integers containing the indices of training examples that are not at the bounds of the optimization.
- **atBoundsExamples**: A HashSet of integers containing the indices of training examples that are at the bounds of the optimization.
- **i_lower**: An integer representing the lower index bound for the optimization.
- **i_upper**: An integer representing the upper index bound for the optimization.
- **b_upper**: A double value representing the upper bound for the bias term in the SVM.
- **b_lower**: A double value representing the lower bound for the bias term in the SVM.
- **errors**: An array of doubles that holds the error values for each training sample.
- **strategy**: An enumeration value of type `SelectionStrategy` that determines the strategy for selecting pairs of examples for optimization (default is `WorstPair`).
- **maxChecks**: An integer representing the maximum number of checks during optimization (default is 100).
- **kernelFunction**: A Func delegate that simulates the kernel function used in SVM calculations, defaulting to a dot product.
- **fields**: An array of FieldInfo objects that contains information about the non-public fields of the class, utilized for reflection.

## Functions
- **Start()**: Unity's lifecycle method that initializes the class by calling the `Init()` method.
  
- **Init()**: Initializes the active, non-bound, and at-bounds examples. Sets the kernel function to the dot product and retrieves the non-public fields of the class using reflection.

- **Epsilon**: Property that gets or sets the epsilon value with validation to ensure it is greater than zero.

- **Tolerance**: Property that gets or sets the tolerance value with validation to ensure it is greater than zero.

- **Strategy**: Property that gets or sets the selection strategy for the optimization process.

- **Learn(double[][] inputs, int[] outputs, double[] weights = null)**: Initiates the learning process for the SVM using the provided input samples and output labels. It also modifies the size of the `alpha` array using reflection.

- **RunLearning(double[][] inputs, int[] outputs)**: Executes the learning process by iterating through the input samples, invoking the `ComputeExample` method for each sample.

- **ComputeExample(int index, double[][] inputs, int[] outputs)**: Computes the SVM optimization for a specific sample index. It evaluates the kernel function and calculates the error for the sample.

- **DotProduct(double[] a, double[] b)**: Calculates and returns the dot product of two arrays of doubles.

- **PrintFieldValues()**: Outputs the current values of the fields of the class at runtime, utilizing reflection for debugging purposes.