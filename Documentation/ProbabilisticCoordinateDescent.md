# ProbabilisticCoordinateDescent

## Overview
The `ProbabilisticCoordinateDescent` class implements an L1-regularized logistic regression learning algorithm, which is a type of probabilistic Support Vector Machine (SVM). This class is part of the `EdgeLoreMachineLearning` namespace and extends the abstract class `BaseProbabilisticCoordinateDescent`. It is designed to optimize weights for a model using a coordinate descent approach, specifically tailored for probabilistic SVMs. The optimization process iteratively refines model weights based on the provided inputs and kernel function, ultimately creating a trained model that can make predictions.

## Variables
- **MaximumNewtonIterations**: An integer that specifies the maximum number of iterations for the Newton optimization process (default is 100).
- **Tolerance**: A double that sets the convergence tolerance for the optimization process (default is 0.01).
- **Kernel**: An instance of the kernel type (`TKernel`), which defines how the input data is transformed for the learning algorithm.
- **Model**: An instance of the model type (`TModel`), which represents the trained SVM model.
- **Inputs**: An array of input data (`TInput[]`) that the model will use for training.
- **C**: An array of doubles representing regularization parameters for the optimization.

## Functions
- **Create(int inputs, PROBABLISTICLINEAR kernel)**: This method creates an instance of the `PCDSVM` model with the specified number of inputs and kernel. It is called during the initialization of the `ProbabilisticCoordinateDescent` class.

- **InnerRun()**: This method contains the main logic for the optimization process. It initializes necessary variables, performs iterations to compute weights, and updates the model with the optimized weights and support vectors. It logs the progress of the optimization.

- **TransposeAndAugment(double[][] input)**: This helper method transposes the input data matrix and adds an additional row of ones to account for the bias term in the model. It returns the augmented matrix.

- **OptimizeWeights(double[][] inputs, double[] exp_wTx, double[] exp_wTx_new, double[] tau, double[] D, double[] C, ref double[] weights, int biasIndex)**: This method optimizes the weights based on the input data and computed values. It calculates gradients and updates the weights iteratively.

- **CalculateGradient(double[] input, double expWTx, double tau, double C)**: This method computes the gradient for weight optimization based on the input data, the exponential of the weighted input, the tau value, and the regularization parameter C.

## Additional Classes and Interfaces
- **BaseProbabilisticCoordinateDescent<TModel, TKernel, TInput>**: An abstract base class that defines the structure for probabilistic coordinate descent algorithms, including properties for maximum iterations, tolerance, kernel, model, inputs, and regularization parameters.

- **PCDSVM<TKernel, TInput>**: A class that represents the SVM model, holding properties such as weights, support vectors, threshold, and a method for making predictions.

- **PROBABLISTICLINEAR**: A structure implementing the `PCDILinear` interface, which defines how to compute the linear function for given weights and inputs.

- **PCDIKernel<TInput>**: An interface that defines the structure for kernel functions used in the SVM.

- **PCDILinear<TInput>**: An interface that extends `PCDIKernel`, adding methods for getting the length of an input and creating a vector from the input.