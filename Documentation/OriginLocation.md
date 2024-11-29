# OriginLocation

## Overview
The `OriginLocation` class is a component in a Unity-based application that represents a point in space-time, characterized by its coordinates and a state vector. This class allows for the adjustment of its state based on feedback and provides a method to compute the distance between two `OriginLocation` instances. It is designed to be versatile within the codebase, enabling various functionalities related to spatial calculations and state adjustments.

## Variables
- **Coordinates**: `float[]`  
  Represents the space-time coordinates (x, y, z, t) of the origin location. It is a private set property, meaning it can only be modified within the class.

- **StateVector**: `float[]`  
  Holds the state vector associated with the origin location. This vector can represent various properties or states and is initialized to a specified dimension.

- **Feedback**: `float[]`  
  Stores feedback values that can be applied to adjust the state vector. This array is also initialized to match the dimension of the state vector.

## Functions
- **OriginLocation(float[] coordinates, int vectorDimensions)**:  
  Constructor that initializes an instance of the `OriginLocation` class. It takes an array of coordinates and the dimensions for the state vector. It sets the coordinates and initializes the state vector and feedback arrays.

- **InitializeState()**:  
  Private method that populates the `StateVector` with random values between -1 and 1. This method is called during the construction of the object to ensure the state vector is initialized upon creation.

- **ApplyFeedback(float[] newFeedback, float learningRate)**:  
  Adjusts the `StateVector` based on the provided feedback. It takes a new feedback array and a learning rate, updating each element of the state vector by applying the feedback scaled by the learning rate.

- **ComputeDistance(OriginLocation other)**:  
  Calculates the Euclidean distance between the current `OriginLocation` instance and another `OriginLocation` instance. It sums the squared differences of their coordinates and returns the square root of that sum, providing a measure of the spatial separation between the two points.