# EdgeLoreMachineLearning

## Overview
The `EdgeLoreMachineLearning` codebase provides a framework for implementing binary Support Vector Machines (SVMs) with different kernel functions. The main purpose of the script is to define interfaces and a concrete implementation for SVMs and kernel functions, allowing for the creation of machine learning models that can classify data based on input features. This framework can be extended with various kernel types to adapt to different classification problems.

## Variables

### IISVMSupportVectorMachine<TInput>
- **Weights**: A collection of weights used by the SVM to determine the importance of each support vector during classification.
- **SupportVectors**: An array of input data points that are critical for defining the decision boundary of the SVM.
- **Threshold**: A bias term that helps in adjusting the decision boundary of the SVM.
- **Kernel**: An instance of a kernel function that defines the transformation applied to the input data for classification.
- **IsProbabilistic**: A boolean flag indicating whether the SVM has been calibrated to provide probabilistic outputs.

### ISVMIKernel<TInput>
- **Compute**: A method that takes two input vectors and computes the kernel function, returning a scalar value that represents the similarity between the two vectors.

### ISVMLinearKernel
- **Compute**: A method that computes the dot product of two input vectors, which is the implementation of the linear kernel function.

## Functions

### IISVMSupportVectorMachine<TInput>
- **Compress()**: This method compresses all support vectors into a single parameter vector if the SVM is using a linear kernel. This is useful for simplifying the model and reducing its complexity.

### ISVMIKernel<TInput>
- **Compute(TInput a, TInput b)**: This method is responsible for calculating the result of the kernel function for two input vectors `a` and `b`. The specific implementation of this method will depend on the type of kernel being used.

### ISVMLinearKernel
- **Compute(double[] a, double[] b)**: This method computes the dot product of two vectors `a` and `b`. It checks if the vectors are of the same length and sums the product of their corresponding elements, returning the result as a scalar value. If the vectors are not of the same length, it throws an `ArgumentException`.