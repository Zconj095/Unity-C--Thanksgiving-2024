# AdamOptimizer

## Overview
The `AdamOptimizer` class implements the Adam optimization algorithm, a popular method for training machine learning models. This optimizer is designed to adaptively adjust the learning rate based on the first and second moments of the gradients, providing improved convergence properties. Additionally, it includes an AMSGrad variant, which helps to maintain the maximum of the second moment, potentially leading to better performance in certain scenarios. This class is intended for use within Unity, making it suitable for game development and other applications that utilize the Unity engine.

## Variables

- `maxIterations`: The maximum number of iterations to perform during optimization.
- `tolerance`: The convergence threshold; optimization stops when the norm of the gradients is below this value.
- `learningRate`: The initial learning rate used to update parameters.
- `beta1`: The exponential decay rate for the first moment estimate.
- `beta2`: The exponential decay rate for the second moment estimate.
- `noiseFactor`: A small constant added to the denominator to prevent division by zero.
- `epsilon`: A small constant to ensure numerical stability in calculations.
- `useAMSGrad`: A boolean indicating whether to use the AMSGrad variant of the Adam optimizer.
- `snapshotDirectory`: The directory where optimizer state snapshots are saved.

- `timeStep`: The current iteration step in the optimization process.
- `firstMoment`: An array holding the first moment estimates for each parameter.
- `secondMoment`: An array holding the second moment estimates for each parameter.
- `maxSecondMoment`: An array for the maximum second moment estimates (used only if AMSGrad is enabled).

## Functions

- `AdamOptimizer(...)`: Constructor that initializes the optimizer with specified parameters, including maximum iterations, tolerance, learning rate, decay rates, and the option to use AMSGrad. It also sets the snapshot directory for saving optimizer state.

- `Reset()`: Resets the internal state of the optimizer, including the time step and moment estimates.

- `Minimize(Func<float[], float> lossFunction, float[] initialParams, Func<float[], float[]> gradientFunction)`: Minimizes the provided loss function using the Adam optimization algorithm. It takes a loss function, initial parameters, and a gradient function as inputs, returning the optimized parameters along with the final loss value.

- `SaveSnapshot()`: Saves the current state of the optimizer (first and second moment estimates, time step) to a CSV file in the specified snapshot directory.

- `LoadSnapshot(string snapshotDirectory)`: Loads the optimizer's internal state from a CSV file in the specified snapshot directory. If the file does not exist, it throws a `FileNotFoundException`.

- `Norm(float[] vector)`: Computes the Euclidean norm of a given vector, used to assess convergence by evaluating the magnitude of the gradients.