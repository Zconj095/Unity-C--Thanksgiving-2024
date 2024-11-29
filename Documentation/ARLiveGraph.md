# ARLiveGraph

## Overview
The `ARLiveGraph` script is designed to visualize CPU usage in real-time within a Unity application using a line graph. It utilizes the `LineRenderer` component to draw a line that represents the CPU usage data over time. The script initializes an array of data points, updates these points with new CPU usage values, and continuously refreshes the graph to provide a smooth visual representation. This functionality is particularly useful for developers and users who want to monitor performance metrics in augmented reality (AR) applications.

## Variables
- `lineRenderer`: A public variable of type `LineRenderer` that is responsible for rendering the line graph. This variable must be assigned in the Unity Editor to link the script with the appropriate LineRenderer component in the scene.
- `dataPoints`: A private array of floats that stores the CPU usage data points. It has a fixed length of 100, allowing for the storage of the last 100 CPU usage readings.
- `index`: A private integer that keeps track of the current index in the `dataPoints` array. It is used to determine where to insert the next CPU usage value and wraps around when it reaches the end of the array.

## Functions
- `void Start()`: This function is called when the script is first initialized. It sets up the `LineRenderer` by defining the number of positions it will use (equal to the length of the `dataPoints` array) and initializes all data points to zero. It also sets the initial positions of the line graph to start from the origin.

- `void Update()`: This function is called once per frame. It retrieves the current CPU usage (using the `GetCPUUsage` method), updates the `dataPoints` array with this value at the current `index`, and increments the index while wrapping it around if it exceeds the length of the array. It then updates the positions of the line graph using the new data points to ensure a smooth transition in the visual representation.

- `float GetCPUUsage()`: This is a private function that simulates the retrieval of CPU usage data. Currently, it generates a random float value between 0 and 100 for testing purposes. In a real-world scenario, this function should be replaced with an actual implementation to fetch the current CPU usage from the system.