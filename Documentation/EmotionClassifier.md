# EmotionClassifier

## Overview
The `EmotionClassifier` script is designed to classify emotions based on various auditory features such as pitch, energy, volume, speech rate, and spectral content. It evaluates these parameters against predefined thresholds to determine the most likely emotional state expressed in a given audio sample. This function is integral to applications that require emotional recognition, such as voice assistants, games, or any interactive systems that respond to user emotions.

## Variables
- **pitch** (float): Represents the pitch of the audio input. It is a crucial factor in determining the emotion.
- **energy** (float): Indicates the energy level of the audio input. Higher energy levels often correlate with more intense emotions.
- **volume** (float): The loudness of the audio input. Volume can influence the perceived emotion.
- **speechRate** (float): Reflects how quickly the speech is delivered. This can affect emotional interpretation.
- **spectralContent** (float[]): An array containing spectral features of the audio input, with each index representing different frequency bands. It must have at least three elements for proper classification.

## Functions
- **ClassifyEmotion** (static):
  - **Parameters**: 
    - `float pitch`: The pitch value of the audio.
    - `float energy`: The energy level of the audio.
    - `float volume`: The volume of the audio.
    - `float speechRate`: The rate of speech in the audio.
    - `float[] spectralContent`: An array of spectral features.
  - **Returns**: A string representing the classified emotion.
  - **Description**: This function first checks if the `spectralContent` array is valid (contains at least three elements). It logs the input values for debugging purposes. Then, it evaluates the input values against a series of conditional thresholds to classify the emotion into one of several categories, such as "COURAGE," "HAPPINESS," or "SERENITY." If no conditions are met, it defaults to returning "UNKNOWN."