# CozniConceptualizer

## Overview
The `CozniConceptualizer` script is a Unity component that manages a conceptual network of ideas, represented as nodes in a graph. Each node, or `ConceptNode`, can be activated and connected to other nodes, allowing for the visualization of relationships and the dynamics of concept activation. This script is essential for applications that require a visual representation of conceptual relationships, such as educational tools, brainstorming applications, or any interactive system that involves idea mapping.

## Variables
- **ConceptGraph**: A list of `ConceptNode` objects representing the conceptual network.
- **ConnectionWeights**: A dictionary that holds the strength of connections between nodes, using a tuple of indices as keys.
- **ConnectionRendererPrefab**: A prefab for visualizing connections between nodes using a `LineRenderer`.
- **activeConnections**: A list to keep track of currently active visual connections.
- **HindsightSensitivity**: A float value that determines how sensitive the system is to reevaluating connections based on past activations.

## Functions
- **Start()**: Initializes the `ConnectionWeights` dictionary and sets up random initial weights for connections between all pairs of nodes in the `ConceptGraph`.

- **Update()**: Calls the `VisualizeConceptGraph()` function to refresh the visualization of the concept graph on each frame.

- **FormNewConcept(string name, float importance, Vector3 position, string context)**: Creates a new `ConceptNode` with the specified parameters and adds it to the `ConceptGraph`.

- **ActivateConcept(int nodeIndex, float inputSignal)**: Increases the activation level of a specified node and checks if it meets the activation threshold. If activated, it triggers the `ReflectOnHindsight()` function.

- **ReflectOnHindsight(int activatedNodeIndex)**: Adjusts the connection weights of nodes connected to the activated node based on the importance of the activated node and the `HindsightSensitivity`.

- **VisualizeConceptGraph()**: Clears previous visual connections and draws new connections between nodes based on the current weights in the `ConnectionWeights` dictionary, using the `ConnectionRendererPrefab` for visualization.