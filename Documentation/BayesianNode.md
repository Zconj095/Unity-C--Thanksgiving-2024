# BayesianNode

## Overview
The `BayesianNode` class is designed to represent a node in a Bayesian network, which is a statistical model that represents a set of variables and their conditional dependencies via a directed acyclic graph. This class primarily manages the name of the node and its associated probabilities for different states. It fits within a larger codebase that likely deals with probabilistic reasoning and decision-making processes in applications such as artificial intelligence, machine learning, or data analysis in Unity.

## Variables
- **Name**: A string that holds the name of the Bayesian node. It is set during initialization and is read-only from outside the class.
- **Probabilities**: A dictionary that maps string keys (representing different states) to float values (representing the probabilities of those states). This dictionary is initialized in the constructor and can be updated or queried using the provided methods.

## Functions
- **BayesianNode(string name)**: Constructor that initializes a new instance of the `BayesianNode` class with a specified name. It also initializes the `Probabilities` dictionary to store the probabilities of various states.

- **void UpdateProbability(string state, float probability)**: This method updates the probability of a given state. If the state already exists in the `Probabilities` dictionary, its value is updated; if not, the state is added with the new probability.

- **float GetProbability(string state)**: This method retrieves the probability associated with a specified state. If the state exists in the `Probabilities` dictionary, it returns the corresponding probability; otherwise, it returns `0.0f`.