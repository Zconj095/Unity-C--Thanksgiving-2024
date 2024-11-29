# VoiceCommandVerifier

## Overview
The `VoiceCommandVerifier` class is designed to validate voice commands by comparing a recorded audio clip with a reference audio clip. This functionality is crucial for applications that rely on voice recognition, ensuring that the recorded input matches expected commands with a certain level of accuracy. The class uses cross-correlation to assess the similarity between the two audio signals, enabling developers to integrate robust voice command verification into their Unity projects.

## Variables
- **recordedClip**: An `AudioClip` representing the audio input recorded by the user. This clip is compared against a reference clip to verify the command.
- **referenceClip**: An `AudioClip` that serves as the standard or expected input for the voice command. The recorded clip is validated against this reference.

## Functions
- **ValidateCommand(AudioClip recordedClip, AudioClip referenceClip)**: This public method takes two audio clips as parameters: the recorded clip and the reference clip. It converts both clips into float arrays representing their audio samples and then uses cross-correlation to determine if the recorded clip matches the reference clip with a threshold of 0.8 for similarity.

- **ConvertToFloatArray(AudioClip clip)**: This private method converts an `AudioClip` into an array of floats. It initializes a float array with the number of samples in the clip and retrieves the audio data from the clip, returning it as a float array. This conversion is essential for processing the audio data for validation.