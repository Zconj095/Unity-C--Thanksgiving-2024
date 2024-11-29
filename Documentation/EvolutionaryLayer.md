# EvolutionaryLayer

## Overview
The `EvolutionaryLayer` script is a component designed to manage the evolutionary process of a neural network system within a Unity game. It interacts with the `NeuroplasticSystem`, which presumably represents the neural network, and facilitates the evolution of the network by modifying the connections between its nodes. Each time the `Evolve` function is called, it increments the generation count and introduces mutations or weight adjustments to the connections between nodes, thereby simulating an evolutionary algorithm.

## Variables
- **NeuralSystem**: An instance of `NeuroplasticSystem`, which contains the nodes and connections of the neural network being evolved.
- **Generation**: An integer that tracks the current generation of the neural network. It starts at 0 and increments each time the `Evolve` function is called.

## Functions
- **Evolve()**: This method is responsible for evolving the neural network. It performs the following actions:
  - Increments the `Generation` variable to indicate a new generation.
  - Iterates through all pairs of nodes in the `NeuralSystem.Nodes` list.
  - For each pair, it checks if a connection exists between them in the `NeuralSystem.Connections` dictionary.
  - If a connection exists, it applies a mutation by adjusting the weight of the connection using a random factor between 0.9 and 1.1, simulating evolution through slight modifications to the connection weights.