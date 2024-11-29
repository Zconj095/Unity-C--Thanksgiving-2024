# FeatureVectorCircuit

## Overview
The `FeatureVectorCircuit` class is a Unity MonoBehaviour that initializes and manages a quantum circuit represented by a feature vector. It uses reflection to dynamically set parameters from an external object, ensuring that the feature dimension is a power of two and that the input amplitudes are normalized. This class serves as a foundational component for handling quantum circuits within the broader context of a quantum simulation or machine learning application.

## Variables

- `featureDimension`: An integer representing the dimension of the feature vector, which must be a power of two.
- `numQubits`: An integer indicating the number of qubits in the circuit, derived from the feature dimension.
- `parameters`: An array of floats that stores the normalized amplitudes of the feature vector.

## Functions

- **FeatureVectorCircuit()**
  - Default constructor for the `FeatureVectorCircuit` class.

- **void Initialize(object inputObject, string methodName)**
  - Initializes the circuit with parameters by using reflection to invoke a method on the provided `inputObject`. The method specified by `methodName` should return an array of amplitudes.

- **private void SetAmplitudes(float[] amplitudes)**
  - Validates the input amplitudes to ensure that the feature dimension is a power of two and that the amplitudes are normalized. If validation passes, it sets the `featureDimension`, `numQubits`, and `parameters` variables.

- **void DisplayCircuit()**
  - Logs the configuration of the circuit, including the number of qubits and the feature dimension, along with the parameters to Unity's debug console.

- **int GetFeatureDimension()**
  - Returns the value of `featureDimension`.

- **int GetNumQubits()**
  - Returns the value of `numQubits`.

- **float[] GetParameters()**
  - Returns the normalized parameters stored in the `parameters` array.

- **private bool IsPowerOfTwo(int value)**
  - Checks if the provided integer `value` is a power of two, returning true if it is and false otherwise.

- **private static int Log2(int value)**
  - Calculates and returns the base-2 logarithm of the given integer `value`.

- **private float Normalize(float[] input)**
  - Normalizes the input array so that its L2 norm equals 1. It returns the computed norm after normalization.