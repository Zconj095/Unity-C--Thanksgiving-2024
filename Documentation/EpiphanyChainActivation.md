# EpiphanyChainActivation

## Overview
The `EpiphanyChainActivation` script is designed to simulate a network of memory nodes that can activate based on input signals and visualizes their connections in a 3D space using Unity. Each memory node has its own activation threshold, current activation level, and decay rate, allowing for dynamic interactions and visual feedback. This script fits into a larger codebase that likely involves cognitive simulations, neural networks, or interactive experiences where memory-like behavior is represented.

## Variables

- **MemoryNode**: A serializable class that represents individual memory nodes with the following properties:
  - `Name`: The name of the memory node.
  - `ActivationThreshold`: The threshold value that must be reached for the node to activate.
  - `CurrentActivation`: The current activation level of the node.
  - `ForgetRate`: The rate at which the activation level decays over time.
  - `Position`: The 3D position of the node in space.
  - `BaseColor`: The default color of the node when inactive.
  - `ActivatedColor`: The color of the node when it is activated.

- **MemoryNodes**: A list that holds all the memory nodes in the network.

- **ConnectionWeights**: A dictionary that stores weighted connections between pairs of memory nodes, represented as tuples of integers (node indices) and their corresponding weight values.

- **ConnectionLinePrefab**: A LineRenderer prefab used for visualizing the connections between memory nodes.

- **activeConnections**: A list that keeps track of currently active LineRenderer instances for visualizing connections.

## Functions

- **Start()**: Initializes the connection weights between memory nodes with random values. It sets up the dictionary to hold the weights for connections between pairs of nodes.

- **Update()**: Called once per frame. It handles two main tasks: decaying the activation levels of the memory nodes and visualizing the current connections based on their weights.

- **TriggerActivation(int nodeIndex, float inputSignal)**: Activates a memory node at the specified index by increasing its current activation level with the provided input signal. If the activation level exceeds the threshold, it calls the `ActivateNode` function.

- **ActivateNode(int nodeIndex)**: Activates the specified memory node, logs its activation, creates a visual representation (a sphere) at its position, triggers activation on connected nodes based on their connection weights, and strengthens the connections related to the activated node to simulate neuroplasticity.

- **DecayActivation()**: Reduces the current activation level of each memory node over time based on its forget rate, ensuring that activation levels do not drop below zero.

- **VisualizeConnections()**: Clears the previous visual connections and draws new connections between memory nodes based on their weights. It creates LineRenderer instances to represent the connections, adjusting their colors and widths according to the nodes they connect.