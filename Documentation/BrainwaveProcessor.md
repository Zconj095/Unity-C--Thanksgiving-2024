# BrainwaveProcessor

## Overview
The `BrainwaveProcessor` class is responsible for analyzing brainwave data by processing signals and calculating the power of different frequency bands. This functionality is essential for applications in neuroscience, mental health, and user experience design, where understanding brain activity is crucial. The class uses a dictionary to define frequency bands and their corresponding ranges, allowing it to compute the power of each band from the provided signal data.

## Variables

- **frequencyBands**: A read-only dictionary that maps frequency band names (strings) to their corresponding frequency ranges (tuples of floats). The bands included are:
  - **delta**: (0.5 Hz to 4 Hz)
  - **theta**: (4 Hz to 8 Hz)
  - **alpha**: (8 Hz to 13 Hz)
  - **beta**: (13 Hz to 30 Hz)
  - **gamma**: (30 Hz to 50 Hz)

## Functions

- **ProcessBrainwaveData(float[] signal, float samplingRate)**: 
  - This public method takes in a signal (an array of floats representing brainwave data) and a sampling rate (a float representing the number of samples per second). It computes the power for each frequency band defined in `frequencyBands` by calling the `ComputeBandPower` method and returns a dictionary containing the power values for each band.

- **ComputeBandPower(float[] signal, float samplingRate, float lowFreq, float highFreq)**: 
  - This private method calculates the power of a specific frequency band based on the provided signal and its frequency range (defined by `lowFreq` and `highFreq`). Currently, it contains a placeholder for the actual signal processing logic (e.g., Fast Fourier Transform) and returns a simulated random power value between 0.1 and 1.0 for demonstration purposes.