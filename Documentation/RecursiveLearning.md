# RecursiveLearning

## Overview
The `RecursiveLearning` script is designed to facilitate the training of a neural network layer using a set of data samples. It integrates a feedback mechanism to adjust the weights of the neural network based on the predictions made by the model. This script is a crucial component of a larger codebase that likely involves machine learning and neural network functionality, providing the means to iteratively improve the model's performance through training.

## Variables
- **data**: A two-dimensional array of floats (`float[][]`), representing the training samples. Each inner array corresponds to a single sample of input data.
- **layer**: An instance of the `RecursiveLayer` class, which represents a layer in the neural network that will process the input data and generate predictions.
- **feedback**: An instance of the `CyberneticFeedback` class, responsible for adjusting the weights of the neural network based on the calculated gradients.
- **learningRate**: A float value that determines the step size at which the model updates its weights during training. A higher learning rate may lead to faster convergence but could also risk overshooting the optimal solution.

## Functions
- **Train(float[][] data, RecursiveLayer layer, CyberneticFeedback feedback, float learningRate)**: This public method orchestrates the training process. It iterates over each sample in the provided data, generates predictions using the specified neural network layer, computes the gradients based on the difference between the target and predicted values, and then adjusts the weights of the model using the feedback mechanism.

- **private float[] ComputeGradients(float[] target, float[] prediction)**: This private method calculates the gradients for each output based on the target values and the predictions made by the neural network. It computes the difference between the predicted and target values, scales it by a factor of 2, and returns an array of gradients that will be used to update the weights in the feedback process.