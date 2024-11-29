# VoiceCapture Script Documentation

## Overview
The `VoiceCapture` script is designed to handle audio recording functionality within a Unity application. It captures audio from the default microphone and stores it as an `AudioClip`. This script is particularly useful in applications where voice input is required, such as voice commands or audio notes. It initializes the microphone on startup, starts recording audio, and provides a method to retrieve the recorded audio clip.

## Variables
- `recordedClip`: An `AudioClip` object that holds the audio data captured from the microphone.
- `microphone`: A string that stores the name of the default microphone device being used for recording.

## Functions
- `void Start()`: This function is called when the script instance is being loaded. It initializes the `microphone` variable with the name of the first available microphone device and then calls the `StartRecording()` function to begin audio capture.

- `void StartRecording()`: This function starts the audio recording process. It uses the `Microphone.Start` method to begin recording audio from the specified microphone, allowing for a maximum duration of 10 seconds at a sample rate of 44100 Hz. A debug message is logged to indicate that recording has started.

- `void StopRecording()`: This function stops the audio recording process by calling `Microphone.End` with the current microphone. It also logs a debug message indicating that recording has been stopped.

- `public AudioClip GetRecordedClip()`: This public method returns the `recordedClip` variable, allowing other parts of the codebase to access the recorded audio data after recording has stopped.