# CognizantClusteredQuadTree

## Overview
The `CognizantClusteredQuadTree` script is designed to manage spatial data using a quad tree structure while integrating a neural network for learning from input data points. It initializes a quad tree to store points within a defined boundary and a neural layer to process and learn from these points. This script fits within a larger codebase that likely involves spatial data management and machine learning, allowing for efficient querying and training of a neural model based on spatially clustered data.

## Variables
- **quadTree**: An instance of `CognizantClusteredQuadTreeNode` that represents the quad tree structure for storing spatial points. It is initialized with a rectangular boundary of size 100x100 and a maximum of 4 points per node.
- **neuralLayer**: An instance of `CognizantClusteredNeuralLayer` that represents the neural network layer used for processing inputs. It is configured with 2 input neurons, 1 output neuron, and a learning rate of 0.1.

## Functions
- **Start()**: This function is called when the script is first initialized. It sets up the quad tree and neural layer, inserts several example data points into the quad tree, and performs initial training on a specific point with a target value.

- **Train(Vector2 point, float target)**: This function takes a 2D point and a target value as input. It normalizes the point coordinates, feeds them into the neural layer to get outputs, calculates the error based on the target value, and backpropagates the error through the neural layer. It also logs the training results for debugging purposes.

- **Update()**: This function is called once per frame. It queries the quad tree for points within a specified rectangular region (50x50) and logs the points found in that region for debugging purposes.

- **TrainBatch(List<Vector2> points, List<float> targets)**: This function takes a list of points and corresponding target values. It iterates through each point and its target, calling the `Train` function for each pair to perform batch training on the neural layer.