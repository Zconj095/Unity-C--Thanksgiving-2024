# GradientDescentOptimizer

## Overview
The `GradientDescentOptimizer` class implements a gradient descent optimization algorithm. It is designed to find the minimum of a given objective function by iteratively adjusting its parameters based on the computed gradients. This optimizer supports learning rate scheduling, allowing for dynamic adjustment of the learning rate during the optimization process, and can utilize callbacks to monitor progress at each iteration. It fits within a codebase that requires optimization functionalities, especially in machine learning or data analysis tasks where minimizing a cost function is essential.

## Variables
- **MaxIterations**: An integer that specifies the maximum number of iterations the optimizer will run before stopping.
- **Tolerance**: A float that defines the convergence tolerance, determining when the optimization process should cease if changes in parameters are minimal.
- **LearningRateSchedule**: A function that takes an integer (the current iteration count) and returns a float (the learning rate for that iteration). This allows users to customize how the learning rate changes over time.
- **Callback**: An action that can be invoked at each iteration to report the current state of the optimization, including the iteration count, current parameters, current objective value, and the current step size.
- **currentParams**: A float array holding the current parameters being optimized.
- **currentStepSize**: A float representing the size of the last step taken in parameter space, used to determine convergence.
- **iterationCount**: An integer that keeps track of the number of iterations completed during the optimization process.

## Functions
- **GradientDescentOptimizer(int maxIterations = 100, float initialLearningRate = 0.01f, float tolerance = 1e-6f, Action<int, float[], float, float> callback = null)**: Constructor that initializes the optimizer with the specified maximum iterations, initial learning rate, convergence tolerance, and an optional callback for monitoring progress.

- **SetLearningRateSchedule(Func<int, float> schedule)**: Sets a custom learning rate schedule function that determines the learning rate for each iteration, allowing for flexibility in how the learning rate evolves during optimization.

- **(float[] OptimizedParams, float FinalValue) Minimize(Func<float[], float> objectiveFunction, float[] initialParams, Func<float[], float[]> gradientFunction)**: This method performs the optimization process. It takes an objective function to minimize, initial parameters for optimization, and a gradient function that computes the gradient of the objective function. It returns a tuple containing the optimized parameters and the final value of the objective function after optimization. 

Overall, the `GradientDescentOptimizer` is a versatile tool for performing optimization tasks, with features that enhance its usability and adaptability for various scenarios.