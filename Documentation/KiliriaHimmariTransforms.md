# Kiliria and Himmari Transforms

## Overview
The `ParallelKiliriaHimmari` script is designed to perform two vector transformations—Kiliria and Himmari—on a set of input vectors in parallel using Unity's job system. The Kiliria transformation is based on the cross product of vectors, while the Himmari transformation involves rotating vectors using quaternions. This script efficiently computes the results of these transformations and logs them, making it suitable for applications that require high-performance vector calculations, such as game development and simulations.

## Variables

- **testVectors**: An array of `Vector3` containing the input vectors for the transformations. It is initialized with random values within a specified range.
- **referenceVectors**: An array of `Vector3` containing reference vectors used for the Kiliria transformation. In this script, it is initialized to a constant value of (1, 0, 0).
- **rotations**: An array of `Quaternion` representing rotations to be applied to the input vectors for the Himmari transformation. Each quaternion is initialized with random Euler angles.
- **kiliriaResults**: An array of `Vector3` that stores the output results of the Kiliria transformation.
- **himmariResults**: An array of `Vector3` that stores the output results of the Himmari transformation.

## Functions

### KiliriaHimmariTransforms

- **KiliriaTransform(Vector3 vector, Vector3 reference)**: 
  - This static method takes a vector and a reference vector as inputs, computes the cross product, normalizes the result, and scales it by the magnitude of the original vector. It returns the transformed vector.

- **HimmariTransform(Vector3 vector, Quaternion rotation)**: 
  - This static method takes a vector and a quaternion as inputs, applies the quaternion rotation to the vector, and returns the rotated vector.

### ParallelKiliriaHimmari

- **Start()**: 
  - This Unity lifecycle method is called on the frame when the script is enabled. It initializes the test data for input vectors, reference vectors, and rotations, then calls the `ExecuteParallelTransforms` method to perform the transformations.

- **ExecuteParallelTransforms()**: 
  - This method creates native arrays for input vectors, reference vectors, and output vectors to be used in the job system. It initializes and schedules the Kiliria and Himmari jobs, ensuring both complete before copying the results back to the corresponding result arrays. Finally, it disposes of the native arrays and logs the results of the transformations.

### KiliriaJob

- **Execute(int index)**: 
  - This method implements the `IJobParallelFor` interface. It is called for each index in the input arrays, performing the Kiliria transformation on the corresponding input and reference vectors, and storing the result in the output array.

### HimmariJob

- **Execute(int index)**: 
  - Similar to `KiliriaJob`, this method also implements the `IJobParallelFor` interface. It applies the Himmari transformation to the input vectors using the corresponding rotations, storing the results in the output array.