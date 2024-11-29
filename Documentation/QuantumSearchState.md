# QuantumSearchState

## Overview
The `QuantumSearchState` class is designed to represent and manipulate the quantum state of a search algorithm using quantum principles. It initializes a uniform superposition of states, applies an oracle function to modify the amplitudes based on specific criteria, performs a diffusion operation to enhance the probability of certain states, and allows for measurement of the quantum state to determine the outcome of the search. This class fits into a larger quantum computing framework where quantum algorithms like Grover's algorithm may be implemented.

## Variables
- **Amplitudes**: An array of `Complex` numbers representing the amplitudes of each quantum state in the superposition. Each amplitude corresponds to a probability amplitude for measuring a specific state.

## Functions
- **QuantumSearchState(int size)**: Constructor that initializes the `Amplitudes` array with the specified size and calls the `InitializeUniformState` method to set up the initial state.

- **InitializeUniformState()**: Private method that populates the `Amplitudes` array with a uniform distribution of complex numbers, ensuring that the sum of the squares of the magnitudes is equal to one (normalization).

- **ApplyOracle(Func<int, bool> oracle)**: This method takes an oracle function as an argument. It iterates through the `Amplitudes` array and flips the sign of the amplitude for states that the oracle identifies as "target" states, effectively marking them for further processing.

- **ApplyDiffusion()**: This method computes the mean amplitude of the current state and reflects each amplitude around this mean. This operation enhances the probabilities of the target states while reducing the probabilities of the non-target states.

- **Measure()**: This function performs a measurement of the quantum state by generating a random value and calculating cumulative probabilities based on the squared magnitudes of the amplitudes. It returns the index of the state that is measured, which corresponds to the result of the quantum search. If no state is selected, it defaults to returning the last index of the amplitudes array.