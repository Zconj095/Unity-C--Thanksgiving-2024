# RhythmCalculator

## Overview
The `RhythmCalculator` class is designed to analyze speech audio data and extract key rhythm metrics. It provides methods to calculate the tempo of speech in beats per minute (BPM), the frequency of pauses in speech, and the variability in speech rhythm based on the time intervals between voiced segments. This class can be particularly useful in applications that require speech analysis, such as language learning tools, speech therapy applications, or any system that needs to interpret the rhythm and flow of spoken language.

## Variables
- `speechRate` (float): Represents the rate of speech in words per second. This value is used to calculate the tempo in beats per minute.
- `samples` (float[]): An array of audio samples representing the speech waveform. This data is analyzed to determine pauses and voiced segments.
- `sampleRate` (int): The number of samples per second in the audio data. This value is essential for calculating the timing of pauses and voiced segments.

## Functions
- `CalculateTempo(float speechRate)`: 
  - **Description**: Converts the speech rate from words per second into beats per minute (BPM). This function is useful for understanding the overall speed of speech.
  
- `CalculatePauseFrequency(float[] samples, int sampleRate)`:
  - **Description**: Analyzes the audio samples to count the number of pauses in the speech. It calculates the frequency of these pauses in terms of pauses per second. The function considers segments of silence longer than a specified threshold to identify pauses.

- `CalculateRhythmVariability(float[] samples, int sampleRate)`:
  - **Description**: Measures the variability of speech rhythm by calculating the average duration of voiced segments within the audio samples. This function helps to understand how consistent or varied the speech rhythm is based on the length of time spent in voiced segments.