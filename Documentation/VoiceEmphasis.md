# VoiceEmphasis

## Overview
The `VoiceEmphasis` script is designed to analyze audio clips and detect emphasis in the audio based on amplitude changes. It processes the audio data by breaking it down into chunks, calculating the amplitude for each chunk, and identifying peaks that indicate moments of emphasis. This script can be integrated into a larger codebase where audio analysis is required, such as in games or applications that rely on voice interaction.

## Variables
- `samples`: An array of floats that stores the audio sample data from the `AudioClip`. This data is used for amplitude analysis.
- `windowSize`: An integer constant set to 1024, which defines the size of the chunks used for analyzing the audio samples. This determines how much data is processed at one time.
- `amplitudes`: An array of floats that stores the calculated average amplitude values for each chunk of audio samples.

## Functions
- **DetectEmphasis(AudioClip clip)**: 
  - This public method takes an `AudioClip` as input, retrieves its sample data, analyzes the amplitude, and detects any emphasis based on the identified peaks. It returns a string indicating whether emphasis was detected or not.

- **AnalyzeAmplitude(float[] samples)**: 
  - This private method processes the audio sample data in chunks defined by `windowSize`. It calculates the average amplitude for each chunk and returns an array of these amplitude values.

- **DetectPeaks(float[] amplitudes)**: 
  - This private method analyzes the amplitude data to find peaks, which represent moments of emphasis in the audio. It checks for values that are greater than their adjacent values and returns a string indicating whether emphasis was detected or not.