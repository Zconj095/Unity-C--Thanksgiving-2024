# QNSPSA

## Overview
The `QNSPSA` class implements a Quantum Natural Stochastic Perturbation Stochastic Approximation (QNSPSA) optimizer for parameter optimization. It is designed to minimize a given loss function by iteratively updating parameters based on computed gradients and Hessians. This optimizer is particularly useful in scenarios requiring efficient exploration of complex parameter spaces, such as in machine learning and artificial intelligence applications. The class interacts with other components of the codebase by providing a structured way to optimize parameters based on a fidelity function.

## Variables
- **_fidelity**: A function that computes the fidelity between two sets of parameters. It is used to evaluate the quality of the parameters during optimization.
- **_maxIter**: An integer representing the maximum number of iterations the optimizer will perform.
- **_blocking**: A boolean indicating whether to stop the optimization if the loss does not improve beyond a certain threshold.
- **_allowedIncrease**: A nullable double that specifies the maximum allowed increase in loss during an iteration before blocking occurs.
- **_learningRate**: A double that determines the step size for parameter updates during optimization.
- **_perturbation**: A double that defines the magnitude of perturbations applied to parameters for gradient and Hessian calculations.
- **_resamplings**: An integer representing the number of times to sample during gradient and Hessian estimation.
- **_hessianDelay**: An integer that indicates how many iterations to wait before starting to compute the Hessian.
- **_regularization**: A nullable double that, if provided, adds a regularization term to the Hessian to prevent overfitting.

## Functions
- **QNSPSA**: Constructor that initializes the optimizer with the provided fidelity function and various configuration parameters.
  
- **Optimize**: 
  - Parameters:
    - `int parameterCount`: The number of parameters to optimize.
    - `Func<double[], double> lossFunction`: The loss function to minimize.
    - `double[] initialPoint`: The initial guess for the parameters.
  - Description: Executes the optimization process, iteratively updating parameters based on computed gradients and Hessians until the maximum number of iterations is reached or the loss does not improve.

- **InitializeHessian**: 
  - Parameters:
    - `int size`: The size of the Hessian matrix to initialize.
  - Description: Creates and returns an identity matrix of the specified size to serve as the initial Hessian.

- **ComputeGradient**: 
  - Parameters:
    - `Func<double[], double> lossFunction`: The loss function to evaluate.
    - `double[] parameters`: The current parameters being optimized.
    - `double[] deltas`: Perturbation values used to compute the gradient.
  - Description: Calculates the gradient of the loss function at the current parameters using finite differences.

- **ComputeHessian**: 
  - Parameters:
    - `double[] parameters`: The current parameters being optimized.
    - `double[] delta1`: First set of perturbations.
    - `double[] delta2`: Second set of perturbations.
  - Description: Computes an approximation of the Hessian matrix using the fidelity function and the provided perturbations.

- **ApplyRegularization**: 
  - Parameters:
    - `double[][] hessian`: The Hessian matrix to which regularization will be applied.
  - Description: Adds a regularization term to the diagonal of the Hessian if a regularization value is provided.

- **InvertMatrix**: 
  - Parameters:
    - `double[][] matrix`: The matrix to be inverted.
  - Description: Inverts the provided matrix using a simple method, assuming it is positive-definite.

- **MultiplyMatrixVector**: 
  - Parameters:
    - `double[][] matrix`: The matrix to multiply.
    - `double[] vector`: The vector to multiply with the matrix.
  - Description: Performs matrix-vector multiplication and returns the resulting vector.

- **GenerateRandomDeltas**: 
  - Parameters:
    - `int size`: The number of random deltas to generate.
  - Description: Creates an array of random values within a specified range for perturbation purposes.

- **AverageMatrices**: 
  - Parameters:
    - `List<double[][]> matrices`: A list of matrices to average.
  - Description: Computes the average of a list of matrices and returns the resulting matrix.

- **AverageVectors**: 
  - Parameters:
    - `List<double[]> vectors`: A list of vectors to average.
  - Description: Computes the average of a list of vectors and returns the resulting vector.

- **OptimizerResult**: 
  - Properties:
    - `double[] Parameters`: The optimized parameters after the optimization process.
    - `double Loss`: The final loss value after optimization.
    - `int FunctionEvaluations`: The total number of function evaluations performed.
    - `int Iterations`: The total number of iterations completed. 

This documentation serves as a comprehensive guide to understanding the structure and functionality of the `QNSPSA` class, enabling developers to effectively utilize and modify the optimizer within the codebase.