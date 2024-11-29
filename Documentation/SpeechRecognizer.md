# SpeechRecognizer

## Overview
The `SpeechRecognizer` script is designed to recognize spoken words from an audio clip in a Unity environment. It serves as a component that can be attached to GameObjects, allowing them to process audio input and return recognized speech. The main function, `RecognizeSpeech`, takes an `AudioClip` as input, analyzes its waveform, and attempts to match it to predefined words. This functionality can be integrated into various applications, such as voice-controlled games or interactive experiences, enhancing user engagement through voice recognition.

## Variables
- **audioData**: An array of floats that stores the audio samples extracted from the provided `AudioClip`. It represents the waveform data for analysis.

## Functions
- **RecognizeSpeech(AudioClip clip)**: 
  - This public method takes an `AudioClip` as input, retrieves its audio data, and processes it to recognize spoken words. It returns a string representing the recognized word or "unknown" if no match is found.

- **MatchAudioToWord(float[] audioData)**: 
  - This private method analyzes the audio data by calculating the average amplitude of the samples. It uses simple rules to match the amplitude against predefined thresholds to determine if the spoken word corresponds to "hello" or is classified as "unknown".