# CortexCogniEnigma

## Overview
The `CortexCogniEnigma` script simulates a cognitive system that processes environmental stimuli through neural pathways. It is designed to be attached to a GameObject in Unity and operates by detecting nearby objects, adapting its internal state based on these stimuli, and visually representing its cognitive state through color changes. This script is part of a larger codebase that likely involves interactive or intelligent entities within a game or simulation, enhancing the realism and engagement of the environment.

## Variables
- `neuralPathways`: A `Dictionary<string, float>` that represents the connections between stimuli and their corresponding strength. This simulates the neural pathways of the cognitive system.
- `memory`: A `List<string>` that stores previously detected stimuli, representing the cognitive entity's memory.
- `adaptationThreshold`: A `float` set to 0.5f that determines the threshold for adapting to stimuli. When the strength of a neural pathway exceeds this value, the system evolves.
- `environmentSource`: A `Transform` that represents the source of environmental stimuli the cognitive system will analyze.
- `activeColor`: A `Color` set to cyan that represents the visual appearance of the cognitive system when active.
- `dormantColor`: A `Color` set to gray that represents the visual appearance of the cognitive system when dormant.
- `objectRenderer`: A `Renderer` that is used to change the visual representation of the GameObject based on its cognitive state.

## Functions
- `Start()`: Initializes the `neuralPathways` and `memory` lists, sets the object's initial color to dormant, and starts a repeating analysis of the environment.
  
- `AnalyzeEnvironment()`: Checks for the presence of the `environmentSource`. If found, it detects stimuli and processes them; if no stimuli are detected, it reduces the activation visual.

- `DetectStimuli()`: Uses a physics overlap sphere to identify nearby colliders and creates a "stimuli signature" by concatenating their names. Returns the signature or null if no stimuli are detected.

- `ProcessStimuli(string stimuli)`: Encodes detected stimuli into the neural pathways. If the stimuli are new, it initializes a pathway strength; if they are already known, it reinforces the existing pathway. It also calls the `Adapt` function and updates the visual appearance.

- `Adapt(string stimuli)`: Checks if the strength of a neural pathway exceeds the adaptation threshold. If so, it logs that the enigma is solved and resets the strength to simulate evolution.

- `UpdateAppearance(float intensity)`: Adjusts the visual representation of the GameObject based on the activation intensity, transitioning the color from dormant to active.

- `OnDrawGizmos()`: Visualizes the detection range of the cognitive system in the Unity editor by drawing a wire sphere around the GameObject, aiding in debugging and design.