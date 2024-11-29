# LLMBayesianNetwork

## Overview
The `LLMBayesianNetwork` class represents a Bayesian Network that manages a collection of Bayesian nodes. It allows for the addition of nodes, updating beliefs based on new evidence, and retrieving the current belief probabilities of specific states within those nodes. This class is designed to facilitate probabilistic reasoning in a Unity environment, making it a critical component for applications that require decision-making under uncertainty.

## Variables
- `nodes`: A private list that stores instances of `BayesianNode`. This list represents the collection of nodes in the Bayesian Network.

## Functions
- `LLMBayesianNetwork()`: Constructor that initializes the `nodes` list to an empty state when a new instance of the `LLMBayesianNetwork` class is created.

- `void AddNode(BayesianNode node)`: This method takes a `BayesianNode` as a parameter and adds it to the `nodes` list. It allows the dynamic expansion of the Bayesian Network by including new nodes.

- `void UpdateBelief(string nodeName, string state, float evidence)`: This method updates the belief probability of a specified state within a given node. It calculates the posterior probability using Bayes' theorem based on the prior probability of the state and the evidence provided. If the node is found, it updates the node's probability.

- `float GetBelief(string nodeName, string state)`: This method retrieves the current belief probability of a specified state within a given node. If the node exists, it returns the probability; otherwise, it returns 0.0f, indicating that the state does not exist in the specified node.