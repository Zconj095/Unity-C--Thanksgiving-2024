# MultiStateVector

## Overview
The `MultiStateVector` class is a Unity component that manages two states, `PrimaryState` and `SecondaryState`, each represented as arrays of floating-point numbers. These states can be initialized with random values and optimized based on feedback using a specified learning rate. This class is typically used in scenarios where multiple states need to be maintained and refined, such as in machine learning or adaptive systems within a game or simulation.

## Variables
- **PrimaryState**: A private property that holds an array of floats representing the primary state of the system. It is initialized with random values between -1 and 1.
- **SecondaryState**: A private property that holds an array of floats representing the secondary state of the system. Similar to `PrimaryState`, it is also initialized with random values between -1 and 1.

## Functions
- **MultiStateVector(int dimensions)**: Constructor that initializes the `PrimaryState` and `SecondaryState` arrays with a specified number of dimensions. It also calls the `InitializeStates` method to populate these arrays with random values.
  
- **InitializeStates()**: A private method that populates the `PrimaryState` and `SecondaryState` arrays with random floating-point numbers, each ranging from -1 to 1. This uses the `System.Random` class to generate the random values.

- **Optimize(float[] feedback, float learningRate)**: A public method that refines both `PrimaryState` and `SecondaryState` based on the provided feedback and a learning rate. This method calls the `RefineState` method from the `HyperstateOptimizer` class to perform the optimization.