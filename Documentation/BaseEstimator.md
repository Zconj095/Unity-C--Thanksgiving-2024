# BaseEstimator

## Overview
The `BaseEstimator` class serves as a foundational component for implementing various estimation algorithms in a machine learning context. It provides methods for computing loss, gradients, and updating parameters, which are essential for optimization processes such as gradient descent. This class is designed to be extended by specific estimators that will implement their unique loss functions and optimization strategies, making it a crucial part of the overall codebase for machine learning algorithms.

## Variables
- **Name**: A string representing the name of the estimator. It is used for identification and debugging purposes.
- **MaxIterations**: An integer that specifies the maximum number of iterations allowed for the optimization process. This helps prevent infinite loops during training.
- **LearningRate**: A float that determines the step size at each iteration while moving towards a minimum of the loss function. It affects how quickly the model learns.

## Functions
- **Constructor**: 
  - `BaseEstimator(string name, int maxIterations, float learningRate)`
    - Initializes a new instance of the `BaseEstimator` class with the provided name, maximum iterations, and learning rate. It performs validation checks to ensure that the name is not null or empty, and that both `maxIterations` and `learningRate` are positive numbers.

- **ComputeLoss**: 
  - `float ComputeLoss(List<float> parameters, List<float> data)`
    - Calculates the loss based on the given parameters and data. It uses a simple linear model to predict outcomes and computes the Mean Squared Error (MSE) as the loss metric. Throws exceptions if parameters or data are null.

- **ComputeGradients**: 
  - `List<float> ComputeGradients(List<float> parameters, List<float> data)`
    - Computes the gradients of the loss function with respect to the parameters. It iterates over the data to calculate how much each parameter needs to change to reduce the loss. Throws exceptions if parameters or data are null.

- **UpdateParameters**: 
  - `List<float> UpdateParameters(List<float> parameters, List<float> gradients)`
    - Updates the parameters by applying the calculated gradients adjusted by the learning rate. It enforces that both parameters and gradients are non-null and of the same length. Throws an exception if the conditions are not met.

- **Optimize**: 
  - `List<float> Optimize(List<float> parameters, List<float> data)`
    - Executes the optimization process by repeatedly calculating gradients, updating parameters, and computing the loss for a set number of iterations (up to `MaxIterations`). It also checks for convergence based on a predefined threshold. It prints the loss at each iteration to monitor the optimization progress. Throws exceptions if parameters or data are null.