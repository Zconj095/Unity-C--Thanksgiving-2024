# ActivationAdjuster

## Overview
The `ActivationAdjuster` script is designed to provide methods for adjusting activation functions commonly used in neural networks, specifically the ReLU (Rectified Linear Unit) and Sigmoid functions. These adjustments are influenced by a distortion parameter, allowing for the manipulation of the activation outputs. This script can be particularly useful in scenarios where fine-tuning of neural network behaviors is required, such as in machine learning applications within Unity.

## Variables
- **None**: This script does not define any instance or class-level variables. All operations are performed through static methods.

## Functions

### `public static float AdjustReLU(float x, float distortion)`
This function takes an input value `x` and a `distortion` factor. It adjusts the ReLU activation by subtracting the distortion from `x` and ensuring that the result is not less than zero. This allows for controlled activation based on the distortion applied.

### `public static float AdjustSigmoid(float x, float distortion)`
This function computes the Sigmoid activation for the input value `x` and then scales it by the `distortion` factor. The output is the Sigmoid value multiplied by `(1 - distortion)`, effectively reducing the Sigmoid output based on the provided distortion. This allows for a flexible adjustment of the activation level in response to the distortion parameter.