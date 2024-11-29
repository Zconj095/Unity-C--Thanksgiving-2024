# VocalEmotionRecognizer

## Overview
The `VocalEmotionRecognizer` script is designed to analyze audio clips to recognize the emotional tone conveyed in the vocal expressions. It utilizes pitch and energy levels derived from the audio samples to classify the emotion as "Happy," "Sad," "Angry," or "Neutral." This functionality is essential for applications that require emotional context from voice data, such as virtual assistants, games, or emotional analysis tools.

## Variables

- **samples**: An array of float values that holds the audio sample data extracted from the provided `AudioClip`. This data is used for further analysis of pitch and energy.
- **pitch**: A float value representing the pitch of the audio sample, derived from the `ExtractPitch` function. It helps in determining the emotional classification.
- **energy**: A float value representing the energy level of the audio sample, calculated by the `CalculateEnergy` function. It is another key factor in the emotion classification process.

## Functions

- **RecognizeEmotion(AudioClip clip)**: 
  - This is the main function of the script. It takes an `AudioClip` as input, extracts the audio samples, calculates the pitch and energy, and classifies the emotion based on predefined thresholds. The function returns a string indicating the recognized emotion.

- **ExtractPitch(float[] samples)**: 
  - This private function analyzes the audio samples to extract the pitch. In its current implementation, it returns a fixed value of 200 for demonstration purposes. In a complete implementation, this function would contain logic to perform actual pitch detection.

- **CalculateEnergy(float[] samples)**: 
  - This private function computes the energy of the audio samples by calculating the average of the square of each sample. The resulting float value represents the overall energy level of the audio clip, which is used in the emotion classification process.