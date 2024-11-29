# HyperbolicTuning

## Overview
The `HyperbolicTuning` class is designed to implement hyperbolic distance calculations and a corresponding loss function for fine-tuning a fractal deep learning network within a Unity environment. This class provides methods to compute the hyperbolic distance between two vectors, calculate the hyperbolic loss based on predictions and targets, and contains helper methods to support these calculations. It serves as a utility for optimizing model performance, particularly in scenarios where hyperbolic geometry is applicable.

## Variables
- **fractalNetwork**: An instance of the `FractalDeepLearning` class, which represents a neural network structure defined by the specified layer sizes.
- **input**: A float array representing the input values to the neural network for prediction.
- **target**: A float array representing the target values that the neural network aims to predict.
- **prediction**: A float array that stores the output from the neural network after a forward pass with the input data.
- **loss**: A float variable that holds the computed hyperbolic loss value based on the prediction and target arrays.

## Functions
- **HyperbolicDistance(float[] x, float[] y)**: Calculates the hyperbolic distance between two input vectors `x` and `y`. It first computes the magnitudes of both vectors and checks if they are within the unit hyperbolic space. If valid, it calculates and returns the hyperbolic distance using the formula derived from hyperbolic geometry.

- **HyperbolicLoss(float[] prediction, float[] target, float regularization, float[] parameters)**: Computes the hyperbolic loss for a given prediction and target. It calculates the hyperbolic distance between the prediction and target vectors, adds a regularization term based on the magnitude of the provided parameters, and returns the total loss.

- **VectorMagnitude(float[] v)**: A helper method that calculates and returns the Euclidean magnitude (length) of the vector `v` by summing the squares of its components and taking the square root.

- **VectorSquaredDifference(float[] x, float[] y)**: A helper method that computes the squared difference between two vectors `x` and `y`. It iterates through the elements of both vectors, calculates the squared difference for each element, and returns the total sum of these squared differences.

- **Start()**: Unity's lifecycle method that is called before the first frame update. It initializes a fractal deep learning network, defines example input and target arrays, performs a forward pass to get predictions, and calculates the hyperbolic loss based on the predictions and targets. It also logs the prediction results and the computed loss to the Unity console.