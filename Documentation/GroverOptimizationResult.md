# GroverOptimizer Script

## Overview
The `GroverOptimizer` script implements a quantum-inspired optimization algorithm based on Grover's search technique. It aims to find the optimal solution to problems that can be represented in a Quadratic Unconstrained Binary Optimization (QUBO) format. The main function of the script is to perform iterative optimization, evaluating potential solutions and searching for the one that minimizes an objective function defined by the problem. The results of the optimization are encapsulated in the `GroverOptimizationResult` class, which holds the best solution found, its corresponding value, and other relevant metrics.

## Variables
- **_numValueQubits**: An integer representing the number of qubits used for the value in the optimization problem.
- **_numKeyQubits**: An integer representing the number of qubits used for the key in the optimization problem.
- **_numIterations**: An integer that defines the number of iterations for the optimization process.
- **_circuitResults**: A dictionary that stores the results of the optimization circuit, mapping strings (result identifiers) to their corresponding double values.
- **_converters**: A list of functions that convert double arrays into other formats, used for processing the results of the optimization.

## Functions
- **GetCompatibilityMsg(Dictionary<string, object> problem)**: Checks if the provided problem can be converted to a QUBO format. It returns an empty string if compatible or an error message if not.

- **Solve(Dictionary<string, object> problem)**: The main function that performs the optimization. It first checks compatibility, initializes variables for tracking the best solution, and enters a loop to iteratively search for the optimum key. It evaluates potential solutions, updates operation counts, and eventually returns a `GroverOptimizationResult` containing the best solution found.

- **EvaluateObjective(double key, Dictionary<string, object> problem, double threshold)**: Evaluates the objective function for a given key based on the linear and quadratic terms defined in the problem. It calculates the value by summing contributions from both terms and subtracting the threshold.

- **GetRandomKey(int numBits)**: Generates a random integer key within the range defined by the number of bits (qubits) specified.

- **ConvertToBinaryArray(int key, int numBits)**: Converts an integer key to a binary array representation, where each bit corresponds to a qubit. The result is an array of doubles, with each element being 0.0 or 1.0 based on the bit value.