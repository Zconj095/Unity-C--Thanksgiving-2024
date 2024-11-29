# DecisionTreeTraversal

## Overview
The `DecisionTreeTraversal` class provides methods for traversing n-ary trees, specifically designed for use with `DecisionNode` objects within the `EdgeLoreMachineLearning` namespace. This class includes three different traversal strategies: Breadth-First, Depth-First, and Post-Order. These methods yield enumerators that allow users to iterate through the nodes of a decision tree in different orders, facilitating various algorithms and analyses that may rely on tree structures.

## Variables
- `queue`: A `Queue<DecisionNode>` used in the Breadth-First traversal method to hold nodes that are yet to be processed.
- `stack`: A `Stack<DecisionNode>` used in the Depth-First traversal method to manage nodes as they are traversed.
- `cursors`: A `Dictionary<DecisionNode, int>` that keeps track of the traversal state for each node during the Post-Order traversal, helping to identify siblings.
- `currentNode`: A `DecisionNode` that represents the node currently being processed during the traversal methods.
- `nextNode`: A `DecisionNode` used to hold the next node in the traversal process, depending on the traversal method being executed.
- `currentIndex`: An `int` that stores the index of the current node within its parent's branches during sibling traversal.

## Functions
- **BreadthFirst(DecisionNode tree)**: This method performs a breadth-first traversal of the given decision tree. It uses a queue to explore all nodes at the present depth level before moving on to nodes at the next depth level. It yields each node as it is processed.

- **DepthFirst(DecisionNode tree)**: This method executes a depth-first traversal of the decision tree. It utilizes a stack to explore as far down a branch as possible before backtracking. Each node is yielded as it is processed.

- **PostOrder(DecisionNode tree)**: This method implements a post-order traversal of the decision tree. It processes all children of a node before processing the node itself. It uses a dictionary to track the state of each node's siblings and yields nodes in post-order fashion.

- **GetNextSibling(Dictionary<DecisionNode, int> cursors, DecisionNode node)**: This private method identifies the next sibling of a given node during post-order traversal. It checks the parent of the current node and retrieves the next sibling based on the traversal state stored in the cursors dictionary. If no sibling exists, it returns null.