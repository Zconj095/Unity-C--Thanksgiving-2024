# QuantumDynamicProcessor

## Overview
The `QuantumDynamicProcessor` class is responsible for processing quantum state data and calculating various statistical measures such as dynamic variance, cross-correlation, and flux interjections. This class is designed to be used within the Unity game engine and provides methods that can be utilized to analyze quantum states represented in matrix form. The results of these computations can be useful in simulations or visualizations related to quantum mechanics or dynamic systems.

## Variables
- **quantumStates**: A 2D array of floats representing the quantum states to be analyzed.
- **rowWise**: A boolean flag indicating whether computations should be performed row-wise (true) or column-wise (false). Defaults to true.
- **means**: An array of floats that holds the computed means of the quantum states, used for variance calculations.
- **variance**: A float that accumulates the calculated variance value across the quantum states.
- **diffSum**: A float used to accumulate the squared differences between each quantum state value and the mean.
- **crossCorrelation**: An array of floats that stores the computed cross-correlation values between two sets of means.
- **interjectionSum**: A float that accumulates the sum of the products of corresponding elements from two flux matrices.

## Functions
- **ComputeDynamicVariance(float[,] quantumStates, bool rowWise = true)**: 
  - Computes the dynamic variance of the provided quantum states. It calculates the mean of the states and then computes the variance based on the selected direction (row-wise or column-wise).
  
- **ComputeCrossCorrelation(float[,] matrixA, float[,] matrixB, bool rowWise = true)**: 
  - Calculates the cross-correlation between the means of two matrices (matrixA and matrixB). It multiplies the corresponding means from both matrices to produce a cross-correlation array.
  
- **ComputeFluxInterjection(float[,] fluxA, float[,] fluxB)**: 
  - Computes the flux dynamic cross interjection by calculating the overlap of two flux matrices (fluxA and fluxB). It sums the products of corresponding elements and normalizes the result by the total number of elements.

- **Start()**: 
  - Unity's built-in method that initializes the class. It creates example matrices for quantum states and demonstrates the use of the computation methods by calculating and logging the dynamic variance, cross-correlation, and flux interjection results to the console.