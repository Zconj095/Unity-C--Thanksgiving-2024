# EmotionalEngine

## Overview
The `EmotionalEngine` script is designed to dynamically update and classify emotional states based on various audio parameters. It fits within a codebase that likely focuses on audio analysis or emotional response systems, allowing for real-time emotion detection and intensity calculation. The script utilizes a set of predefined emotional states and adjusts the current emotion and its intensity based on input parameters such as pitch, energy, volume, speech rate, and spectral content.

## Variables

- **EmotionState currentEmotion**: This variable holds the current emotional state of the engine, which can be one of several predefined emotions or `Unknown` if none match.
  
- **float emotionIntensity**: A floating-point value that represents the intensity of the current emotion, ranging from 0.0 (no intensity) to 1.0 (maximum intensity).

## Functions

- **string UpdateEmotion(float? pitch, float? energy, float? volume, float? speechRate, float[] spectralContent)**: 
  - This function updates the current emotion based on the provided audio parameters. It assigns default values to any missing parameters, classifies the emotion using these inputs, updates the internal state, and calculates the emotion intensity. It returns the name of the classified emotion as a string.

- **private string ClassifyEmotion(float pitch, float energy, float volume, float speechRate, float[] spectralContent)**: 
  - This private function classifies the emotion based on refined conditions using the provided audio parameters. It checks the validity of the spectral content and returns a string representing the classified emotion. If no conditions are met, it returns "Unknown".

- **EmotionState GetCurrentEmotion()**: 
  - This function retrieves the current emotional state stored in the `currentEmotion` variable.

- **float GetEmotionIntensity()**: 
  - This function returns the current emotion intensity stored in the `emotionIntensity` variable.