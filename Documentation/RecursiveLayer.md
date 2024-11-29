# RecursiveLayer

## Overview
The `RecursiveLayer` script is a Unity component that processes input data by applying a specified adjustment function to each element of the input array in conjunction with a corresponding feedback array. This functionality allows for the manipulation of data in a way that can be useful in various applications, such as neural networks or signal processing, where feedback and adjustments play a critical role in the computation.

## Variables
- **input**: An array of floats representing the initial data that will be processed.
- **feedback**: An array of floats providing feedback values that will be used in conjunction with the input data during processing.
- **adjustmentFunc**: A function that takes two float parameters (an input value and a feedback value) and returns a float. This function defines how the input and feedback values should be combined.

## Functions
- **Forward(float[] input, float[] feedback, Func<float, float, float> adjustmentFunc)**: 
  - This method takes in the `input` and `feedback` arrays along with an `adjustmentFunc`. It initializes an `adjusted` array to store the results of applying the `adjustmentFunc` to each pair of corresponding elements from `input` and `feedback`. It iterates through each element, applies the adjustment function, and returns the adjusted array. This function is crucial for transforming the input data based on the feedback received.