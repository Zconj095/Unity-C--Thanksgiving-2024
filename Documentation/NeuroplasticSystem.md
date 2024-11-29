# NeuroplasticSystem

## Overview
The `NeuroplasticSystem` script simulates a network of neural nodes that evolve over time based on their activation levels and the connections between them. This script is designed to be part of a larger Unity project, where it can be used to visualize how neural connections adapt and change, mimicking the principles of neuroplasticity. The main function of the script is to initialize connections between nodes, evolve those connections based on activation levels, and visualize the connections in the Unity scene.

## Variables

- **NeuralNode**: A nested class that represents an individual neural node with the following properties:
  - `Name`: A string representing the name of the neural node.
  - `ActivationLevel`: A float representing the current activation level of the node, ranging from 0 to 1.
  - `Weight`: A float representing the dynamic weight of the connection between nodes.
  - `Position`: A `Vector3` that indicates the position of the node in 3D space for visualization purposes.

- **List<NeuralNode> Nodes**: A list that contains all the neural nodes in the system.

- **Dictionary<(int, int), float> Connections**: A dictionary that holds the connections between nodes, where the key is a tuple of two integers (representing the indices of the connected nodes) and the value is a float representing the weight of the connection.

- **float plasticityRate**: A float that controls the rate at which neuroplastic adjustments occur, set to 0.05 by default.

## Functions

- **void Start()**: Unity's built-in method that is called before the first frame update. It initializes the connections between the nodes by calling the `InitializeConnections` method.

- **void Update()**: Unity's built-in method that is called once per frame. It manages the evolution of connections by calling `EvolveConnections` and visualizes the connections by calling `VisualizeConnections`.

- **private void InitializeConnections()**: Initializes the `Connections` dictionary by creating random weights for each unique pair of nodes. It loops through the list of nodes and assigns a random weight between 0.1 and 0.5 for each connection.

- **private void EvolveConnections()**: Updates the weights of the connections based on the activation levels of the connected nodes. It calculates the activity level as the product of the activation levels of two connected nodes and adjusts the connection weight accordingly, ensuring that the weight remains within the range of 0 to 1.

- **private void VisualizeConnections()**: Visualizes the connections between nodes in the Unity scene using lines. It uses `Debug.DrawLine` to draw lines between the positions of connected nodes, with the color of the line transitioning from blue to red based on the weight of the connection.