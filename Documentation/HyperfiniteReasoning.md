# HyperfiniteReasoning

## Overview
The `HyperfiniteReasoning` script is designed to represent and evaluate a logical structure using hyperfinite reasoning. It constructs a hierarchy of logical nodes, each containing premises, conclusions, and a truth value that ranges from 0.0 to 1.0. The script allows for the evaluation of these logical nodes recursively, combining their truth values to arrive at a final evaluation result. This functionality is particularly useful in scenarios where complex logical relationships need to be assessed, such as in game development or AI reasoning systems.

## Variables

- **LogicalNode**: A class that represents a logical node in the reasoning structure. It contains:
  - `Premise`: A string representing the premise of the logical node.
  - `Conclusion`: A string representing the conclusion derived from the premise.
  - `TruthValue`: A float representing the hyperfinite truth value of the logical node (ranging from 0.0 to 1.0).
  - `Subjections`: A list of `LogicalNode` objects that represent the subjections (dependencies) of the current node.

## Functions

- **EvaluateLogicalNode(LogicalNode node)**: 
  - This static function evaluates the truth value of a given logical node. It recursively processes all subjections of the node, combining their truth values using logical conjunction (AND). If a node has no subjections, it returns its own truth value.

- **BuildHyperfiniteReasoning()**: 
  - This static function constructs the initial hyperfinite reasoning structure starting from a root logical node. It creates several logical nodes, establishes their relationships through subjections, and returns the root node of the constructed logical hierarchy.

- **Start()**: 
  - This Unity-specific function is called when the script is initialized. It builds the hyperfinite reasoning structure using `BuildHyperfiniteReasoning()` and then evaluates this structure using `EvaluateLogicalNode()`. Finally, it outputs the evaluation result to the debug log.