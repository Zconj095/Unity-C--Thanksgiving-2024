# RealTimeSpeechProcessor

## Overview
The `RealTimeSpeechProcessor` script is designed to capture audio input from the user's microphone in real-time, process the audio samples, and analyze various speech attributes. This script is part of a larger codebase that focuses on audio processing within a Unity application. It utilizes Unity's `Microphone` class to record audio and processes the recorded samples to extract characteristics such as tone, density, clarity, and spectral content. The results of these analyses are logged for further use or evaluation.

## Variables

- **audioSource**: An `AudioSource` component used to play back audio if needed.
- **recordedClip**: An `AudioClip` that holds the audio data captured from the microphone.
- **sampleRate**: An integer representing the number of audio samples per second (default is set to 44100 Hz).

## Functions

- **Start()**: This function is called when the script is first run. It initializes the audio recording by starting the microphone and capturing audio for a duration of 10 seconds at the specified sample rate.

- **Update()**: This function is called once per frame. It checks if the microphone is still recording audio. If so, it extracts the audio samples from the `recordedClip`, processes various speech attributes (tone, density, clarity, crispness, bass, treble, and ambience), and logs the results to the console for monitoring and debugging purposes.