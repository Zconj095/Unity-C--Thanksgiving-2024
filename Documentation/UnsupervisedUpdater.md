# UnsupervisedUpdater

## Overview
The `UnsupervisedUpdater` class is designed to facilitate updates to different types of unsupervised learning networks within a Unity environment. It provides static methods to update a Hopfield network and a Bayesian network based on provided inputs. This class allows for the integration of unsupervised learning algorithms into the Unity game engine, enabling dynamic learning and adaptation based on real-time data.

## Variables
- **None**: The class does not contain any instance variables. All operations are performed through static methods.

## Functions

### `UpdateHopfield(HopfieldNetworkIntegration network, float[] input)`
This static method updates a Hopfield network by first recalling a stabilized state based on the provided input. It then trains the network using this stabilized state. This method is essential for enhancing the network's ability to recognize patterns based on past experiences.

#### Parameters:
- `HopfieldNetworkIntegration network`: An instance of the Hopfield network that will be updated.
- `float[] input`: An array of float values representing the input data used for recalling the stabilized state.

### `UpdateBayesian(LLMBayesianNetwork network, string nodeName, float evidence)`
This static method updates a Bayesian network by modifying the belief of a specified node based on new evidence. This is crucial for maintaining the accuracy of the network's predictions as new data becomes available.

#### Parameters:
- `LLMBayesianNetwork network`: An instance of the Bayesian network that will be updated.
- `string nodeName`: The name of the node in the Bayesian network whose belief will be updated.
- `float evidence`: A float value representing the new evidence that will influence the node's belief.