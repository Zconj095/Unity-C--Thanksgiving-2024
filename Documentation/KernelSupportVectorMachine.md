# KernelSupportVectorMachine

## Overview
The `KernelSupportVectorMachine` class implements a Unity-compatible Support Vector Machine (SVM) that utilizes kernel functions for classification tasks. This class is a part of the `EdgeLoreMachineLearning.VectorMachines` namespace, which is designed to facilitate machine learning tasks within Unity. The SVM is capable of handling a specified number of input features and can compute decisions based on support vectors, weights, and a threshold. It also supports cloning and debugging functionalities for easier management and inspection of its parameters.

## Variables
- **Kernel**: An instance of `IKernel<double[]>` that defines the kernel function used for computing the decision boundary.
- **NumberOfInputs**: An integer representing the number of input features that the SVM expects.
- **SupportVectors**: A jagged array of doubles (`double[][]`) that stores the support vectors used in the SVM model.
- **Weights**: An array of doubles (`double[]`) representing the weights assigned to each support vector.
- **Threshold**: A double value that serves as the decision threshold for classification.
- **IsProbabilistic**: A boolean indicating whether the SVM is configured to provide probabilistic outputs.

## Functions
- **KernelSupportVectorMachine(IKernel<double[]> kernel, int inputs)**: Constructor that initializes a new instance of `KernelSupportVectorMachine` with a specified kernel function and the number of input features. It throws an exception if the kernel is null or if the number of inputs is non-positive.

- **void SetSupportVectors(double[][] supportVectors, double[] weights, double threshold)**: Sets the support vectors, their corresponding weights, and the decision threshold for the SVM. It validates that neither support vectors nor weights are null and that their lengths match.

- **double Compute(double[] input)**: Computes the decision value for a given input vector. It ensures that the input vector's size matches the expected number of inputs and calculates the decision based on the support vectors, weights, and threshold.

- **KernelSupportVectorMachine Clone()**: Creates and returns a new instance of `KernelSupportVectorMachine` that is a clone of the current instance, preserving its parameters such as support vectors, weights, threshold, and probabilistic configuration.

- **void DebugPrint()**: Outputs the current parameters of the SVM to the Unity console for debugging purposes, including the number of inputs, threshold, probabilistic status, and counts of support vectors and weights.