# UMDAOptimizer

## Overview
The `UMDAOptimizer` class implements the Univariate Marginal Distribution Algorithm (UMDA) for optimizing a given objective function. It aims to find the minimum value of the objective function by iteratively refining a population of candidate solutions. The optimizer utilizes a probabilistic model to generate new solutions based on the best-performing candidates from previous generations. This class is designed to be used within a larger codebase that requires optimization capabilities, particularly in scenarios where the objective function is complex or computationally expensive to evaluate.

## Variables

- **_maxIterations**: An integer representing the maximum number of iterations the optimizer will run.
- **_generationSize**: An integer that defines the number of candidate solutions in each generation.
- **_alpha**: A double value between 0 and 1 that determines the proportion of the best solutions to consider when updating the distribution parameters.
- **_callback**: An optional action that takes the current iteration count, the best individual solution, and the cost of that solution, allowing for external monitoring of the optimization process.
  
- **_population**: A two-dimensional array of doubles that holds the current population of candidate solutions.
- **_distributionParameters**: A two-dimensional array of doubles where the first row contains the means of the distribution, and the second row contains the standard deviations for generating new candidates.
- **_evaluations**: An array of doubles representing the evaluations (costs) of the current population based on the objective function.
- **_bestIndividual**: An array of doubles that holds the best candidate solution found during the optimization process.
- **_bestCost**: A double representing the cost (value of the objective function) of the best individual solution.
- **_staleIterations**: An integer counter that tracks the number of iterations without improvement in the best solution.

## Functions

- **UMDAOptimizer(int maxIterations, int generationSize, double alpha, Action<int, double[], double>? callback)**: Constructor that initializes the optimizer with the specified parameters. It validates the input values for generation size and alpha.

- **OptimizerResult Minimize(Func<double[], double> objectiveFunction, double[] initialPoint)**: The main method that performs the optimization. It takes an objective function and an initial point, evaluates the population, updates the best solution, and generates new candidates until the maximum iterations are reached or early termination conditions are met.

- **private void InitializeDistributionParameters(int numVariables)**: Initializes the distribution parameters (means and standard deviations) for generating new candidate solutions based on the number of variables.

- **private void GenerateInitialPopulation(int numVariables)**: Generates the initial population of candidate solutions using the specified number of variables.

- **private void EvaluatePopulation(Func<double[], double> objectiveFunction)**: Evaluates the current population of candidates using the provided objective function and stores the results in the evaluations array.

- **private void UpdateBestSolution()**: Updates the best individual solution and its cost if a better solution is found. It also resets the stale iterations counter.

- **private void UpdateDistributionParameters()**: Updates the distribution parameters based on the elite candidates (best performers) from the current population.

- **private void GenerateNewPopulation()**: Generates a new population of candidate solutions based on the updated distribution parameters.

- **private double[][] GeneratePopulation(int numVariables)**: Creates a new population by sampling from a normal distribution defined by the means and standard deviations.

- **private static double SampleNormal(System.Random random, double mean, double stdDev)**: Samples a value from a normal distribution using the Box-Muller transform.

- **private double InvokeFunctionUsingReflection(Func<double[], double> function, double[] parameters)**: Invokes the objective function using reflection, allowing for dynamic method invocation.

- **private OptimizerResult CreateResult()**: Creates and returns an `OptimizerResult` object containing the best parameters found, the corresponding loss, the total number of function evaluations, and the number of iterations.

### Nested Class

- **public class OptimizerResult**: A data structure that holds the results of the optimization, including the best parameters, the loss associated with those parameters, the number of function evaluations, and the total iterations performed.

### Static Class

- **public static class StatisticsExtensions**: Contains extension methods for statistical calculations, including a method to compute the standard deviation of a specific variable across a population of candidates.

This documentation provides a clear understanding of the `UMDAOptimizer` class, its purpose, variables, and functions, making it easier for developers to utilize and extend its capabilities within their projects.