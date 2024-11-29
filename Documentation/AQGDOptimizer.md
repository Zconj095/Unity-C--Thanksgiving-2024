# AQGDOptimizer

## Overview
The `AQGDOptimizer` class implements the Analytic Quantum Gradient Descent (AQGD) optimization algorithm. It is designed to minimize a provided loss function using gradient descent techniques, with additional features such as momentum and step size scheduling. This optimizer is particularly useful in machine learning and deep learning contexts, where it can help improve convergence rates during training. The AQGD optimizer fits into the codebase by providing an efficient way to optimize parameters for various models, allowing developers to easily integrate it into their training loops.

## Variables
- `maxIterations`: A list of integers representing the maximum number of iterations for the optimizer at each epoch.
- `learningRates`: A list of floats representing the learning rates to be used during optimization.
- `momenta`: A list of floats representing the momentum coefficients for the optimization process.
- `tolerance`: A float indicating the convergence tolerance for objective function changes.
- `paramTolerance`: A float indicating the convergence tolerance for parameter changes.
- `averagingWindow`: An integer specifying the window size for averaging previous objective values to check for convergence.
- `prevParams`: An array of floats that stores the previous parameter values for convergence checks.
- `prevObjectiveValues`: A list of floats that holds the previous objective function values for convergence analysis.
- `prevGradients`: A list of float arrays that stores previous gradient values (though it's not used in the current implementation).
- `evaluationCount`: An integer that counts the total number of evaluations performed during the optimization process.

## Functions
- `AQGDOptimizer(int maxIterations = 1000, float learningRate = 1.0f, float momentum = 0.25f, float tolerance = 1e-6f, float paramTolerance = 1e-6f, int averagingWindow = 10)`: Constructor that initializes the optimizer with specified parameters including maximum iterations, learning rate, momentum, and tolerances.

- `private void ResetState()`: Resets the internal state of the optimizer, clearing previous parameters, objective values, gradients, and evaluation count.

- `public (float[] OptimizedParams, float FinalLoss) Minimize(Func<float[], float> lossFunction, float[] initialParams, Func<float[], float[]> gradientFunction = null)`: Minimizes the provided loss function using the AQGD algorithm. It takes a loss function, initial parameters, and an optional gradient function, returning optimized parameters and the final loss value.

- `private void UpdateParameters(ref float[] parameters, float[] gradient, ref float[] momentumVector, float stepSize, float momentumCoeff)`: Updates the model parameters and momentum vector using the computed gradients, learning rate, and momentum coefficient.

- `private bool CheckParameterConvergence(float[] parameters)`: Checks if the change in parameters is below the specified parameter tolerance to determine convergence.

- `private bool CheckObjectiveConvergence(float loss)`: Checks if the change in the average objective function value is below the specified tolerance to determine convergence.

- `private float[] ComputeNumericalGradient(Func<float[], float> lossFunction, float[] parameters)`: Computes the numerical gradient of the loss function using finite differences.

- `private static float Norm(float[] vector)`: Computes the Euclidean norm (magnitude) of a given vector.

- `private static float[] Difference(float[] vector1, float[] vector2)`: Computes the element-wise difference between two vectors.

- `private static float Average(List<float> values)`: Computes the average of a list of float values.

- `private static IEnumerable<(T1, T2)> Zip<T1, T2>(IEnumerable<T1> list1, IEnumerable<T2> list2)`: Zips two lists together, yielding pairs of corresponding elements from each list.