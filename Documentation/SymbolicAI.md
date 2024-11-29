# SymbolicAI

## Overview
The `SymbolicAI` script is a Unity component that manages a knowledge base using a dictionary to store concepts and their associated importance values. This script allows for the addition of new knowledge, querying existing knowledge, and integrating insights from a neural network. It serves as a foundational element for AI behaviors that rely on symbolic reasoning within a Unity game or application.

## Variables
- **KnowledgeBase**: A `Dictionary<string, float>` that stores concepts as keys and their importance values as float values. This serves as the core data structure for the AI's knowledge.

## Functions
- **AddKnowledge(string concept, float importance)**: This method adds a new concept to the `KnowledgeBase` if it does not already exist. If the concept is already present, it increases its importance by the provided value. This allows the AI to accumulate knowledge over time.

- **Query(string queryConcept)**: This method retrieves the importance of a specified concept from the `KnowledgeBase`. If the concept exists, it returns a string indicating the concept and its importance. If the concept is not found, it returns a message stating that the concept is not found.

- **IntegrateNeuralInsights(string concept, float neuralWeight)**: This method allows the integration of insights from a neural network into the knowledge base by calling the `AddKnowledge` method. It effectively adds or updates the importance of a concept based on neural network outputs.