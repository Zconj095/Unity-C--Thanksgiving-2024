# CogniCortexSystem

## Overview
The `CogniCortexSystem` class simulates a cognitive system using spatial reasoning and fractal structures. It generates a network of nodes (representing cognitive points) and allows for the simulation of cognitive activation based on stimuli. The class fits within a larger Unity project, likely related to artificial intelligence or cognitive modeling, where it can be used to mimic neural processes and decision-making.

## Variables

- `SpatialNode`: A private inner class representing a node in the cognitive cortex.
  - `Position`: A `Vector3` indicating the spatial position of the node.
  - `Connections`: A `List<SpatialNode>` containing references to other nodes that are connected to this node.
  - `ActivationLevel`: A `float` representing the activation level of the node, simulating cognitive activation.

- `cortexNodes`: A `List<SpatialNode>` that holds all nodes in the cognitive cortex.
  
- `numNodes`: A `const int` that defines the total number of nodes to be created in the cortex (set to 100).

- `connectionRadius`: A `const float` that specifies the maximum distance for nodes to be connected (set to 5.0f).

## Functions

- `InitializeCogniCortex()`: 
  - Initializes the cognitive cortex by generating `numNodes` random spatial nodes and connecting them if they are within `connectionRadius` of each other. It logs the initialization status.

- `GenerateHoltzFracturez(Vector3 start, int depth, float scale)`: 
  - A private recursive function that generates a list of points forming a fractal structure based on the provided start position, depth of recursion, and scaling factor. Returns a list of `Vector3` points.

- `ZelliaFractalLearning(SpatialNode startNode, int depth, float learningFactor)`: 
  - A private recursive function that simulates a learning process through the spatial nodes. It increases the activation level of connected nodes based on a learning factor and returns the total activation level after traversing the specified depth.

- `SimulateSpatialReasoning(Vector3 stimulus)`: 
  - This public function processes a given stimulus by finding the closest node in the `cortexNodes` list and simulating cognitive activation through the `ZelliaFractalLearning` function. It logs the activation level after processing the stimulus or indicates if no node was found.

- `Start()`: 
  - Unity's built-in method that is called when the script instance is being loaded. It initializes the cognitive cortex, generates a Holtz Fracturez structure, and simulates spatial reasoning using a predefined stimulus.