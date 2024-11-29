# LowLevelCognitionHandler

## Overview
The `LowLevelCognitionHandler` class is designed to process sensory inputs from various sources, such as players or enemies, and respond appropriately based on the type and intensity of the input. It manages an internal state that tracks the alert level and the last stimulus received. This script is intended to be used within a Unity game environment, where it can react to dynamic stimuli and trigger corresponding actions for game entities.

## Variables

- **internalStates**: A dictionary that holds the internal state of the cognition handler. It includes:
  - `"AlertLevel"`: A float representing the current alertness level, ranging from 0 to 1.
  - `"LastStimulus"`: An object that stores the last sensory input received.

- **OnCognitionOutput**: An event that allows external listeners to respond to cognitive outputs, such as playing a sound or changing behavior.

## Functions

- **Start()**: Initializes the internal states when the script is first run.

- **InitializeStates()**: Sets the initial values for the internal states:
  - Sets `"AlertLevel"` to 0.
  - Sets `"LastStimulus"` to `null`.

- **ProcessSensoryInput(SensoryInput input)**: The main method that processes incoming sensory inputs. It logs the input details, analyzes the input, and generates an appropriate response based on the analysis.

- **AnalyzeInput(SensoryInput input)**: Analyzes the sensory input and updates the alert level based on the input's intensity. It clamps the alert level to ensure it remains between 0 and 1 and updates the last stimulus.

- **GenerateResponse(SensoryInput input)**: Generates responses based on the type of sensory input:
  - For "sound", if the intensity is greater than 0.5, it triggers a sound alert.
  - For "movement", it adjusts the behavior to investigate.
  - For "interaction", it engages with the source of the input.
  - If the input type is unknown, it logs that no action is taken.

- **Update()**: Monitors the internal state periodically. It decreases the alert level over time if it is greater than 0, simulating a decay in alertness.

- **SimulateInput(LowLevelCognitionHandler cognitionHandler)**: A helper method to create and process a sample sensory input, simulating a sound input from the player.

- **HandleCognitionOutput(string action, object data)**: Responds to the cognitive outputs based on the action received. It logs the action taken, such as playing a sound, changing behavior, or engaging with an entity.