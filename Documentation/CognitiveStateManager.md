# CognitiveStatev2Manager

## Overview
The `CognitiveStatev2Manager` script is designed to manage the cognitive states of an entity in a Unity environment. It simulates sensory input through vector data, transitions between various cognitive states, and handles reactions based on the current state. This script is integral to the codebase as it provides a framework for decision-making and action execution based on input data, enhancing the behavior of game characters or entities.

## Variables

- **currentState**: This variable holds the current cognitive state of the manager, initialized to `CognitiveStatev2.Idle`. It determines the behavior of the entity based on its state.
  
- **vectorLocations**: A list of `Vector3` objects that stores the simulated sensory input data (e.g., spatial coordinates). This data is used to influence state transitions and decision-making processes.
  
- **reactionTime**: A float value that acts as a multiplier for the reaction time when executing actions. It controls how quickly the entity responds to the chosen vector location.

## Functions

- **Update()**: This method is called once per frame. It simulates input data, updates the cognitive state based on the input, and handles reactions according to the current state.

- **SimulateInput()**: Generates random vector inputs and adds them to the `vectorLocations` list. It also limits the size of the list to 100 to optimize memory usage.

- **UpdateState()**: Contains logic to transition between cognitive states based on the number of vector inputs available. It checks the current state and determines if a transition to another state is warranted.

- **HandleReactions()**: Executes specific actions and logs messages based on the current cognitive state. Each state has defined reactions that provide feedback or perform operations relevant to that state.

- **AnalyzeVectors()**: Analyzes the vector data to calculate the mean position of the vectors in `vectorLocations`. If there are no vectors, the method returns without action.

- **MakeDecision()**: Makes a decision based on the vector data by randomly selecting a vector from `vectorLocations`. If there are no vectors, the method returns without action.

- **ExecuteAction()**: Moves the GameObject towards the first vector location in `vectorLocations` using linear interpolation. It considers the `reactionTime` to control the speed of movement.

- **ResetSystem()**: Resets the cognitive state manager by clearing the `vectorLocations` list and effectively resetting the system to its initial state.

- **OnDrawGizmos()**: Visualizes the vector locations in the Unity editor. It draws spheres at each vector location and displays the current cognitive state of the manager in the scene view.