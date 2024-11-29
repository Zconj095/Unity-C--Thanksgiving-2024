# GoemansWilliamsonOptimizer

## Overview
The `GoemansWilliamsonOptimizer` class implements the Goemans-Williamson algorithm for solving the maximum cut problem using Semidefinite Programming (SDP). The main function of this script is to take a problem defined by an adjacency matrix and a set of variables, and return an optimization result that includes the best cut found, its value, and additional metadata about the solution. This class fits within a larger codebase focused on optimization and graph theory, providing a method to efficiently solve combinatorial problems.

## Variables
- `_numCuts`: An integer representing the number of random cuts to generate during the optimization process.
- `_sortCuts`: A boolean indicating whether the cuts should be sorted based on their evaluated values.
- `_uniqueCuts`: A boolean indicating whether to retain only unique cuts in the final solution.
- `_random`: An instance of the `Random` class used to generate random numbers for creating random cuts.

## Functions
### `GetCompatibilityMsg(Dictionary<string, object> problem)`
Checks the compatibility of the provided problem by ensuring all variables are binary. It returns a message if there are any non-binary variables.

### `Solve(Dictionary<string, object> problem)`
The main method that solves the maximum cut problem. It performs the following steps:
1. Checks compatibility of the problem.
2. Extracts the adjacency matrix from the problem.
3. Solves the SDP using the extracted adjacency matrix.
4. Generates random cuts based on the SDP solution.
5. Evaluates the generated cuts against the adjacency matrix.
6. Sorts and deduplicates solutions if specified.
7. Creates samples from the evaluated solutions and returns an optimization result.

### `ExtractAdjacencyMatrix(Dictionary<string, object> problem)`
Extracts and constructs a symmetric adjacency matrix from the problem's data, ensuring it is suitable for optimization.

### `SolveMaxCutSDP(double[,] adjMatrix)`
Simulates the solution of the SDP for the maximum cut problem by creating a positive semi-definite matrix. This method currently returns a diagonal matrix with ones.

### `EvaluateCuts(double[,] cuts, double[,] adjMatrix)`
Evaluates each cut against the adjacency matrix to calculate its value. It returns a list of key-value pairs where each pair consists of a cut and its corresponding value.

### `GenerateRandomCuts(double[,] chi, int numVertices)`
Generates a specified number of random cuts based on the provided matrix `chi`, ensuring that the matrix is positive semi-definite before generating the cuts.

### `GetUniqueCuts(List<KeyValuePair<double[], double>> solutions)`
Filters the evaluated solutions to retain only unique cuts, using a dictionary to track already seen solutions.

### `CalculateCutValue(double[] cut, double[,] adjMatrix)`
Calculates the value of a given cut by summing the weights of edges that are cut by the partition defined by the cut.