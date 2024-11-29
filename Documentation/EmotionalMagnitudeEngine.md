# EmotionalMagnitudeEngine

## Overview
The `EmotionalMagnitudeEngine` class is designed to calculate an emotional magnitude score based on three input parameters: pitch, energy, and spectral content. This score can be useful in applications such as audio analysis, music processing, or game development where emotional responses to sound are relevant. The `CalculateMagnitude` function combines these parameters to produce a single float value that represents the emotional intensity derived from the input data.

## Variables
- `pitch`: A float representing the pitch of the audio signal. It contributes 30% to the final magnitude score.
- `energy`: A float representing the energy level of the audio signal. It contributes 50% to the final magnitude score.
- `spectralContent`: An array of floats that represents the spectral content of the audio signal. Each value in this array contributes to the overall spectral sum, which accounts for 20% of the final magnitude score.
- `spectralSum`: A float that accumulates the sum of all values in the `spectralContent` array.

## Functions
- `CalculateMagnitude(float pitch, float energy, float[] spectralContent)`: This function takes in the pitch, energy, and spectral content of an audio signal. It calculates the total spectral sum by iterating through the `spectralContent` array, and then combines the pitch, energy, and spectral sum to return a single float value representing the emotional magnitude score. The formula used is: 
  \[
  \text{Magnitude} = (pitch \times 0.3) + (energy \times 0.5) + (spectralSum \times 0.2)
  \]