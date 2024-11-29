# SparseCodingSimulation

## Overview
The `SparseCodingSimulation` script is designed to simulate the generation and encoding of stimuli in a sparse coding framework. It creates a specified number of random stimuli within a defined range, encodes these stimuli based on their deviation from the average, and visualizes both the original and encoded stimuli in the Unity console. This script is part of a larger codebase that may involve neural network simulations, data analysis, or visualizations related to cognitive modeling.

## Variables
- `numberOfStimuli`: An integer that defines how many stimuli will be generated. Default value is set to 100.
- `stimulusRange`: A float that sets the range within which the stimuli values will be randomly generated. Default value is 10.0f.
- `sparsenessThreshold`: A float that determines the threshold for encoding stimuli. If the absolute difference from the average exceeds this threshold, the stimulus is considered significant and is encoded; otherwise, it is set to zero.
- `stimuli`: An array of floats that holds the generated stimuli values.
- `encodedStimuli`: An array of floats that contains the encoded values based on the generated stimuli.

## Functions
- `Start()`: This is a Unity lifecycle method that initializes the simulation by generating stimuli, encoding them, and visualizing the results when the script starts.

- `GenerateStimuli()`: This function populates the `stimuli` array with random float values within the range defined by `stimulusRange`. It iterates through the number of stimuli specified by `numberOfStimuli`.

- `EncodeStimuli()`: This function encodes the generated stimuli by calculating the difference between each stimulus and the average stimulus. If the absolute difference exceeds the `sparsenessThreshold`, it retains the difference; otherwise, it assigns a value of zero.

- `GetAverageStimulus()`: This helper function calculates and returns the average value of the generated stimuli by summing all stimuli and dividing by the total number of stimuli.

- `VisualizeStimuli()`: This function formats the original and encoded stimuli into strings and outputs them to the Unity console using `Debug.Log()`, allowing for a visual representation of the results.