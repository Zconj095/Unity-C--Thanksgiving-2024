# ReasoningController

## Overview
The `ReasoningController` script is a component in a Unity game that manages the interaction between a cognitive state and a temporal memory system. Its main function is to store the current cognitive state into memory and retrieve a relevant past state based on a generated query. This process allows the game to utilize past experiences or states to inform current behavior or decision-making, enhancing the realism and depth of the AI's reasoning capabilities.

## Variables
- `temporalMemory`: An instance of `TemporalMemory`, which is responsible for storing and retrieving states.
- `cognitiveState`: An instance of `CognitiveState`, which contains the current cognitive state information.
- `queryState`: A float array representing the state used for retrieving relevant past states from memory.
- `retrievedState`: A float array that holds the state retrieved from memory that is closest to the `queryState`.

## Functions
- `Update()`: This method is called once per frame. It checks if the `cognitiveState` and `temporalMemory` are initialized. If they are, it stores the current cognitive state in memory, generates a query state, and retrieves the closest state from memory. If a state is retrieved, it logs the state to the console.

- `GenerateQueryState()`: This private method generates the query state by obtaining the current cognitive state. It is intended to provide a state that can be used to find relevant past experiences.

- `OnDrawGizmos()`: This method is called by Unity to allow visual debugging in the editor. If a `retrievedState` exists, it draws green cubes in the scene view to represent the retrieved state visually. Each cube's position is determined by the index and value of the `retrievedState`.

This documentation serves as a guide for understanding the purpose and functionality of the `ReasoningController` script within the overall codebase, making it easier for developers to work with and extend the functionality as needed.