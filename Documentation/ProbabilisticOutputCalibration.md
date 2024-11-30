# ProbabilisticOutputCalibration

## Overview
The `ProbabilisticOutputCalibration` class is designed to perform probabilistic output calibration for linear models within the Unity environment. It utilizes reflection to access and modify the model's internal parameters (weights and threshold) based on the calibration process. The primary function of this class is to enhance the output probabilities of a model by adjusting its parameters using a probabilistic framework, thereby improving its predictive accuracy.

## Variables
- **_model (TModel)**: The machine learning model that is being calibrated.
- **_distances (double[])**: An array that stores the distances computed from the input data during training.
- **_targets (double[])**: An array that holds the target probabilities corresponding to the output labels.
- **_maxIterations (int)**: The maximum number of iterations allowed for the calibration process (default is 100).
- **_minStepSize (double)**: The minimum step size for the optimization algorithm (default is 1e-10).
- **_sigma (double)**: A small constant used for numerical stability in the optimization process (default is 1e-12).
- **_tolerance (double)**: The tolerance level for convergence in the optimization process (default is 1e-5).

## Functions
- **ProbabilisticOutputCalibration(TModel model)**: Constructor that initializes the calibration class with the specified model.

- **int MaxIterations { get; set; }**: Property for getting and setting the maximum number of iterations for the calibration process.

- **double MinStepSize { get; set; }**: Property for getting and setting the minimum step size for the optimization process.

- **double Sigma { get; set; }**: Property for getting and setting the sigma value used for numerical stability.

- **double Tolerance { get; set; }**: Property for getting and setting the tolerance level for convergence.

- **TModel Train(TInput[] inputs, bool[] outputs)**: Main method that performs the calibration process. It initializes the calibration, computes the necessary parameters, and updates the model based on the training inputs and outputs.

- **private double ComputeLogLikelihood(double A, double B)**: Calculates the log-likelihood of the model given the parameters A and B. This function is critical for evaluating the model's fit to the data.

- **private double[] ComputeGradient(double A, double B, out double[] hessian)**: Computes the gradient and Hessian matrix for the optimization process. This information is used to determine the direction and magnitude of updates to the parameters.

- **private void UpdateNewtonDirection(double[] gradient, double[] hessian, out double dA, out double dB)**: Updates the Newton direction for the optimization process based on the computed gradient and Hessian.

- **private void ApplyCalibration(double A, double B)**: Applies the calibration results to the model by updating its weights and threshold using reflection. It also invokes a method on the model to set it to a probabilistic mode.