# TemporalReasoner

## Overview
The `TemporalReasoner` script is designed to manage and analyze the cognitive state of an entity within a Unity environment by maintaining a temporal memory of past cognitive states and adjusting external stimuli based on this memory. It integrates with the `CognitiveState` class to retrieve and update the state of the entity, allowing it to respond dynamically to its environment. This script plays a crucial role in simulating intelligent behavior by leveraging historical data to influence current decision-making processes.

## Variables
- `public CognitiveState cognitiveState`: A reference to the `CognitiveState` object that holds the current cognitive state of the entity.
- `public float[] externalStimuli`: An array representing environmental stimuli that affect the cognitive state.
- `public int vectorSize`: Defines the size of the vectors used in cognitive state representation and temporal memory.
- `private float[,] temporalMemory`: A 2D array that stores historical cognitive states for the purpose of temporal reasoning.
- `private int memoryCapacity`: The maximum number of past cognitive states that can be stored in memory.
- `private int currentMemoryIndex`: An index to keep track of the current position in the temporal memory for storing new states.

## Functions
- `void Start()`: Initializes the temporal memory and generates initial external stimuli when the script starts.
  
- `void InitializeTemporalMemory()`: Allocates memory for the temporal memory based on the defined `memoryCapacity` and `vectorSize`.

- `void GenerateStimuli()`: Fills the `externalStimuli` array with random values, simulating various environmental influences on the cognitive state.

- `void Update()`: Called once per frame, it triggers the temporal reasoning process to update the cognitive state based on current and historical stimuli.

- `void PerformTemporalReasoning()`: 
  - Stores the current cognitive state in the temporal memory.
  - Calculates the average historical influence from the stored states.
  - Adjusts the external stimuli based on the historical influence.
  - Updates the cognitive state with the adjusted stimuli.

- `void OnDrawGizmos()`: Visualizes the temporal memory in the Unity editor by drawing cubes that represent the historical states, aiding in debugging and understanding the memory's content.