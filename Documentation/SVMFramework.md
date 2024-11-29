# SVMFramework

## Overview
The `SVMFramework` class implements a simple Support Vector Machine (SVM) model for classification tasks. The main function of this class is to classify input data based on predefined support vectors and a bias value. This class fits within a codebase that may be focused on machine learning or data classification, providing a straightforward way to classify new data points based on learned parameters.

## Variables
- **SupportVectors**: An array of floats that represents the support vectors used in the classification process. These vectors are essential for determining the decision boundary of the SVM.
- **Bias**: A float that represents the bias term added to the classification output. This term helps to adjust the decision boundary of the SVM.

## Functions
- **SVMFramework(float[] supportVectors, float bias)**: Constructor that initializes a new instance of the `SVMFramework` class with the provided support vectors and bias value.
  
- **float Classify(float[] input)**: This method takes an input array and classifies it using the support vectors and bias. It first checks if the dimensions of the input match those of the support vectors. If they do not, it throws an exception. If they do, it calculates the dot product of the input and support vectors, adds the bias, and returns the classification result.