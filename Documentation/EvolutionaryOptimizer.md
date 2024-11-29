# EvolutionaryOptimizer

## Overview
The `EvolutionaryOptimizer` class implements an evolutionary algorithm designed to optimize a population of solutions represented as weight matrices. It provides methods for initializing a population, mutating weights, performing crossover between parent solutions, and evolving the population based on fitness scores. This class is particularly useful in scenarios where optimization is required, such as in machine learning or game development, allowing for the generation of improved solutions over successive iterations.

## Variables

- `populationSize`: 
  - **Type**: `int`
  - **Description**: The number of individuals (weight matrices) in the population that the optimizer will maintain.

- `mutationRate`: 
  - **Type**: `float`
  - **Description**: The probability of mutation occurring for each weight in a matrix during the mutation process.

## Functions

- **Constructor**: 
  ```csharp
  public EvolutionaryOptimizer(int populationSize, float mutationRate)
  ```
  - **Description**: Initializes a new instance of the `EvolutionaryOptimizer` class with a specified population size and mutation rate.

- **InitializePopulation**: 
  ```csharp
  public List<float[,]> InitializePopulation(int size)
  ```
  - **Description**: Creates and returns a list of weight matrices, each of size `size x size`, initialized with random values between -1 and 1. This represents the starting population for the optimization process.

- **Mutate**: 
  ```csharp
  public float[,] Mutate(float[,] weights)
  ```
  - **Description**: Takes a weight matrix as input and returns a new matrix where some of the weights have been adjusted (mutated) based on the mutation rate. Each weight has a chance to be modified by a small random value.

- **Crossover**: 
  ```csharp
  public float[,] Crossover(float[,] parent1, float[,] parent2)
  ```
  - **Description**: Combines two parent weight matrices to create a child matrix. Each weight in the child matrix is randomly selected from one of the two parent matrices.

- **Evolve**: 
  ```csharp
  public List<float[,]> Evolve(List<float[,]> population, List<float> fitnessScores)
  ```
  - **Description**: Takes the current population and their corresponding fitness scores to create the next generation of solutions. It selects the top performers from the current population and generates new offspring through mutation and crossover until the population size is met.

- **GetTopPerformers**: 
  ```csharp
  private List<int> GetTopPerformers(List<float> scores, int count)
  ```
  - **Description**: Identifies and returns the indices of the top `count` performers based on their fitness scores. This function is used internally to select the best individuals from the population for reproduction.