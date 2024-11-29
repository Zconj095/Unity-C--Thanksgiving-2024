# QBayesian

## Overview
The `QBayesian` class is designed to perform Quantum Bayesian Inference using rejection sampling techniques. It utilizes a quantum circuit object, which is managed dynamically through reflection, to simulate quantum sampling and apply Grover operators for evidence-based sampling. This class is integrated into a larger codebase focused on quantum computing, providing a framework for probabilistic reasoning in quantum systems.

## Variables
- `_numQubits`: An integer representing the total number of qubits in the quantum circuit.
- `_limit`: An integer that sets the maximum number of iterations allowed during the rejection sampling process.
- `_threshold`: A float that defines the convergence threshold for the sampling process.
- `_label2qidx`: A dictionary mapping register labels (strings) to their corresponding qubit indices (integers).
- `_label2qubit`: A dictionary mapping register labels (strings) to their corresponding qubits (integers).
- `_samples`: A dictionary that stores sample probabilities obtained from rejection sampling (keyed by label).
- `_converged`: A boolean indicating whether the sampling process has converged.
- `_sampler`: A function that simulates quantum sampling using reflection.
- `_circuit`: An object representing the quantum circuit, managed dynamically through reflection.

## Functions
- **QBayesian(object circuit, int limit = 10, float threshold = 0.9f)**: Constructor that initializes the `QBayesian` instance with the provided quantum circuit, limit for iterations, and convergence threshold. It also sets up the mappings for qubit registers.

- **Dictionary<string, float> SimulateSampler(object circuit)**: Simulates quantum sampling by generating a random probability distribution over two possible states. It normalizes the results to ensure they sum to 1.

- **object GetGroverOperator(Dictionary<string, int> evidence)**: Dynamically retrieves and creates an instance of a Grover operator based on the provided evidence. This method simulates the generation of the Grover operator.

- **Dictionary<string, float> RejectionSampling(Dictionary<string, int> evidence)**: Performs rejection sampling for Quantum Bayesian Inference. It samples without evidence if none is provided or applies the Grover operator iteratively until convergence or the iteration limit is reached.

- **float Inference(Dictionary<string, int> query, Dictionary<string, int> evidence = null)**: Performs inference on a given query, optionally using evidence. It calculates the probability of the query based on the available samples.

- **static T GetProperty<T>(object obj, string propertyName)**: A utility method that dynamically retrieves the value of a specified property from an object using reflection.

- **static void InvokeMethod(object obj, string methodName, params object[] parameters)**: A utility method that dynamically invokes a specified method on an object using reflection.

## Public Properties
- **bool Converged**: Indicates whether the sampling process has converged.
- **int Limit**: Gets or sets the limit for iterations during rejection sampling.
- **float Threshold**: Gets or sets the convergence threshold for sampling.
- **Dictionary<string, float> Samples**: Provides access to the stored sample probabilities.
- **object Circuit**: Returns the quantum circuit object being managed.