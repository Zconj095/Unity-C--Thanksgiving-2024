# CognizantCortexReaderGroversAlgorithm

## Overview
The `CognizantCortexReaderGroversAlgorithm` class implements Grover's algorithm, a quantum algorithm designed for searching unsorted databases. This script allows users to mark specific states in a search space and then performs a search to find the state with the highest amplitude based on the marked states. It is part of a larger codebase that likely involves quantum computing simulations or applications within a Unity environment.

## Variables
- `searchSpaceSize`: An integer that represents the total number of states in the search space.
- `markedStates`: A list of integers that holds the states marked by the user for searching. These states are the targets of the search operation.

## Functions
- **CognizantCortexReaderGroversAlgorithm(int size)**: Constructor that initializes a new instance of the class. It takes an integer `size` as a parameter, which sets the `searchSpaceSize` and initializes the `markedStates` list.

- **void MarkState(int state)**: This method allows the user to mark a specific state for searching. It checks if the state is not already marked and adds it to the `markedStates` list if it is unmarked.

- **int PerformSearch()**: This method executes the Grover's search algorithm. It first checks if there are any marked states to search for. If none are marked, it logs a warning and returns -1. Otherwise, it performs a series of iterations to amplify the amplitudes of the marked states, diffuse the amplitudes across all states, and finally determines which state has the highest amplitude. It returns the index of the state with the highest amplitude.