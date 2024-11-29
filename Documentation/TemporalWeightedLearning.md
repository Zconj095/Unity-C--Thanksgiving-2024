# TemporalWeightedLearning

## Overview
The `TemporalWeightedLearning` script is designed to manage a network of learning nodes within a Unity environment. Each node has a temporal weight that adjusts over time based on its activation history. The script facilitates the activation of nodes, the propagation of influence between them, and the visualization of connections. It fits into a larger codebase by providing a mechanism to simulate and visualize learning behaviors in a temporal context, potentially useful in AI or educational applications.

## Variables

- **LearningNode**: A class that represents each node in the system, containing the following properties:
  - `Name`: A string representing the name of the node.
  - `TemporalWeight`: A float indicating the weight of the node based on its activation over time.
  - `ActivationValue`: A float representing the influence of the node (not used in the current implementation).
  - `Timestamp`: A float indicating the last time the node was activated.
  - `Position`: A `Vector3` representing the node's position in the 3D space for visualization.
  - `BaseColor`: A `Color` representing the default color of the node.
  - `HighlightColor`: A `Color` used to highlight the node during activation.

- **Nodes**: A list of `LearningNode` objects representing all nodes in the system.

- **ConnectionWeights**: A dictionary mapping pairs of node indices to their respective connection weights, represented as a float.

- **DecayRate**: A float indicating the rate at which the temporal weight of nodes decays over time.

- **ReinforcementFactor**: A float that determines the amount of reinforcement applied to a node's temporal weight upon reactivation.

- **ConnectionRendererPrefab**: A `LineRenderer` prefab used for visualizing connections between nodes.

- **activeConnections**: A list of `LineRenderer` objects representing the currently active visual connections.

## Functions

- **Start()**: Initializes the `ConnectionWeights` dictionary and sets random initial weights for the connections between nodes. This function is called when the script is first run.

- **Update()**: Called once per frame, this function applies temporal decay to the weights of the nodes and updates the visualization of the connections.

- **ActivateNode(int nodeIndex)**: Activates a specified node by its index, updates its temporal weight based on the time since its last activation, and propagates the activation to connected nodes. If the node index is invalid, it logs a warning.

- **ApplyTemporalDecay()**: Applies decay to the temporal weights of all nodes based on the time elapsed since their last activation.

- **PropagateActivation(int nodeIndex)**: Propagates the activation from the specified node to its connected nodes by updating their connection weights and activating them.

- **VisualizeConnections()**: Clears existing visual connections and redraws them based on the current connection weights, using the `LineRenderer` prefab to represent the connections visually.