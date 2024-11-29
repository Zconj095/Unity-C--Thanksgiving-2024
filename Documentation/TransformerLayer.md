# TransformerLayer

## Overview
The `TransformerLayer` class is a Unity script that implements a transformation layer for processing input data in the form of tuples. It performs a transformation on two arrays of floats, known as "query" and "key," using a specified transformation function. This class fits into the codebase by acting as a component that can be attached to GameObjects in Unity, enabling the application of custom transformations to data, potentially for tasks such as machine learning or data processing within a game or application.

## Variables
- **query**: An array of floats representing the "query" input data.
- **key**: An array of floats representing the "key" input data.
- **transformed**: An array of floats that stores the result of applying the transformation function to each corresponding pair of elements from the "query" and "key" arrays.

## Functions
- **Forward(Tuple<float[], float[]> input, Func<float, float, float> transformationFunc)**: 
  - This method takes a tuple containing two float arrays (`input`) and a transformation function (`transformationFunc`). It applies the transformation function to each element of the "query" and "key" arrays and returns a new tuple containing the original "query" array and the resulting transformed array. The transformation is performed element-wise, meaning each element in the "query" array is transformed using its corresponding element in the "key" array.