# RealTimeEmphasisAnalyzer

## Overview
The `RealTimeEmphasisAnalyzer` script is designed to analyze real-time audio input from the microphone and classify the type of emphasis present in the speech. This script records audio for a limited duration, extracts various audio features such as pitch, energy, volume, speech rate, and spectral content, and then classifies the emphasis using a separate classifier. It fits within a codebase that may be focused on audio analysis, speech recognition, or interactive applications that respond to vocal input.

## Variables

- `audioSource`: An instance of `AudioSource` used for playing back audio. This public variable allows other components to reference the audio source in the Unity editor.
  
- `recordedClip`: An instance of `AudioClip` that holds the audio data recorded from the microphone.
  
- `sampleRate`: An integer representing the sample rate for microphone input. It is set to 44100 Hz, which is a standard sample rate for audio recording.

## Functions

- `Start()`: This method is called before the first frame update. It initializes the recording process by starting to record audio from the default microphone for a duration of 10 seconds.

- `Update()`: This method is called once per frame. It checks if the microphone is still recording. If it is, it extracts audio samples from the `recordedClip`, computes various audio features (pitch, energy, volume, speech rate, and spectral content), classifies the emphasis using the `EmphasisClassifier`, and logs the results to the console for debugging purposes.