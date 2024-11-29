# ReducedErrorPruning

## Overview
The `ReducedErrorPruning` class implements the Reduced Error Pruning algorithm for decision trees within the Unity environment. This algorithm helps in refining a decision tree by removing branches that do not contribute significantly to the model's accuracy. The class works closely with a `DecisionTree` instance, using a set of input data and corresponding outputs to determine the effectiveness of pruning. By evaluating and pruning nodes based on their contribution to overall error reduction, this class enhances the decision tree's performance.

## Variables
- `tree`: The `DecisionTree` instance that is being pruned.
- `inputs`: A 2D array of doubles representing the input features used for pruning.
- `outputs`: An array of integers representing the expected outputs corresponding to the `inputs`.
- `actual`: An array of integers that stores the actual outputs determined by the decision tree.
- `info`: A dictionary mapping each `DecisionNode` to its associated `NodeInfo`, which contains information about subsets of indices, errors, and gains for each node.

### NodeInfo Class
- `Subset`: A list of integers that holds the indices of the input data points that reach this node.
- `Error`: A double representing the error rate for the subset of data points at this node.
- `Gain`: A double representing the gain in performance achieved by pruning this node.

## Functions
- `ReducedErrorPruning(DecisionTree tree, double[][] inputs, int[] outputs)`: Constructor that initializes a new instance of the `ReducedErrorPruning` class, setting up the decision tree, input features, outputs, and initializing necessary data structures.

- `Run()`: Executes one pass of the pruning algorithm, computes errors and gains, identifies the node with the maximum gain, and prunes it if beneficial. Returns the overall error after pruning.

- `InitializeNodeInfo()`: Initializes the `info` dictionary for all nodes in the decision tree, preparing them for the pruning process.

- `TrackDecisions(DecisionNode root)`: Tracks the decision paths for all input data points starting from the root of the decision tree.

- `TrackDecisionPath(DecisionNode node, double[] input, int index)`: Follows the path of a given input through the decision tree, updating the `info` for each node traversed and recording the actual output.

- `GetNextNode(DecisionNode node, double[] input)`: Determines the next node to traverse in the decision tree based on the current node and the input features.

- `ComputeErrors()`: Calculates the error rates for each node in the decision tree based on the tracked decisions.

- `ComputeNodeError(DecisionNode node)`: Computes the error rate for a specific node by comparing the actual outputs to the expected outputs for the indices in its subset.

- `ComputeGains()`: Computes the gain for each node in the decision tree based on the error reduction achieved by pruning.

- `ComputeNodeGain(DecisionNode node)`: Calculates the gain for a specific node by comparing its error with the summed errors of its child nodes.

- `GetMaxGainNode()`: Identifies the node with the maximum gain that can be achieved through pruning.

- `PruneNode(DecisionNode node)`: Prunes a specified node by clearing its branches and setting its output to the most common output among its subset of indices.

- `ComputeOverallError()`: Calculates the overall error rate of the decision tree after pruning by comparing the predicted outputs to the expected outputs across all input data points.

### DecisionTreeExtensions Class
- `TraverseAll(this DecisionTree tree)`: Extension method that returns all nodes in the decision tree starting from the root.

- `TraverseAll(DecisionNode node)`: Recursively traverses a decision node and its children, yielding each node in the tree.

- `Subset(this int[] array, int[] indices)`: Extension method that returns a new array containing elements from the original array at the specified indices.