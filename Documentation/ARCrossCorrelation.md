# ARCrossCorrelation

## Overview
The `ARCrossCorrelation` class provides methods to compute the cross-correlation between two signals and to validate whether the correlation meets a specified threshold. This functionality is particularly useful in applications involving signal processing, such as audio analysis or pattern recognition, where determining the similarity between two signals is essential. The methods in this class allow for easy integration into a broader codebase that handles signal processing tasks.

## Variables
- **signal1**: An array of floats representing the first signal for correlation computation.
- **signal2**: An array of floats representing the second signal for correlation computation.
- **referenceSignal**: An array of floats representing the reference signal used for validation against the test signal.
- **testSignal**: An array of floats representing the signal being tested for correlation with the reference signal.
- **threshold**: A float representing the minimum correlation value that must be met for the test signal to be considered a valid match against the reference signal.
- **length**: An integer representing the length of the input signals, used to ensure they are of the same size.
- **sum**: A float used to accumulate the product of corresponding elements from the two signals during the correlation computation.
- **correlation**: A float representing the computed correlation value between the reference and test signals.

## Functions
- **ComputeCorrelation(float[] signal1, float[] signal2)**: 
  - This method calculates the cross-correlation between two input signals. It first checks if both signals have the same length and throws an exception if they do not. It then computes the sum of the products of corresponding elements from both signals and normalizes the result by dividing by the length of the signals. The method returns the normalized correlation value as a float.

- **ValidateWithCorrelation(float[] referenceSignal, float[] testSignal, float threshold)**: 
  - This method validates whether the correlation between a reference signal and a test signal meets a specified threshold. It calls the `ComputeCorrelation` method to obtain the correlation value, logs the correlation to the console, and returns a boolean indicating whether the correlation is greater than or equal to the threshold.