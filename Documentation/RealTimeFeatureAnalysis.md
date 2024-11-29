# RealTimeFeatureAnalysis

## Overview
The `RealTimeFeatureAnalysis` script is designed to analyze audio input in real-time using Unity's audio capabilities. It captures audio from the microphone, processes it to extract various features such as pitch, energy, volume, speech rate, and spectral content, and then classifies the emotional feeling conveyed in the audio. This functionality is crucial for applications that require real-time audio analysis, such as voice emotion recognition or interactive audio experiences.

## Variables

- **audioSource**: An instance of `AudioSource` used to play audio. It is publicly accessible, allowing other scripts or components to modify its properties.
  
- **recordedClip**: An instance of `AudioClip` that holds the audio data recorded from the microphone. It is used internally to store and process the recorded audio samples.

## Functions

- **Start()**: This method is called when the script instance is being loaded. It initializes the audio recording by starting the microphone. The recording lasts for 10 seconds at a sample rate of 44100 Hz.

- **Update()**: This method is called once per frame. It checks if the microphone is currently recording. If it is, the method retrieves the audio data from `recordedClip`, extracts various audio features (pitch, energy, volume, speech rate, and spectral content), classifies the emotional feeling based on these features, and logs the results to the console.

### Feature Extraction Methods (not defined in this script but referenced)
- **FeatureExtractor.ExtractPitch(float[] audioSamples, int sampleRate)**: Extracts the pitch from the audio samples.
  
- **FeatureExtractor.ExtractEnergy(float[] audioSamples)**: Calculates the energy of the audio samples.
  
- **FeatureExtractor.ExtractVolume(float[] audioSamples)**: Determines the volume of the audio samples.
  
- **FeatureExtractor.CalculateSpeechRate(float[] audioSamples, int sampleRate)**: Computes the speech rate from the audio samples.
  
- **FeatureExtractor.ExtractSpectralContent(float[] audioSamples)**: Analyzes the spectral content of the audio samples.

### Feeling Classification Method (not defined in this script but referenced)
- **FeelingClassifier.ClassifyFeeling(float pitch, float energy, float volume, float speechRate, float[] spectralContent)**: Classifies the emotional feeling based on the extracted audio features.