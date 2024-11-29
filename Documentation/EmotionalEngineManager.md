# EmotionalEngineManager

## Overview
The `EmotionalEngineManager` script is responsible for managing and processing emotional data within a Unity application. It integrates various emotional engines to classify emotions based on audio parameters and propagate these emotions through different systems. This script plays a crucial role in how the application responds to emotional cues, influencing the user experience by adjusting ambience, emotional flux, and throughput based on real-time audio analysis.

## Variables
- `EmotionalFluxEngine fluxEngine`: An instance of the `EmotionalFluxEngine` class that manages the emotional flux based on the classified emotion.
- `EmotionalAmbienceEngine ambienceEngine`: An instance of the `EmotionalAmbienceEngine` class that sets the ambience according to the classified emotion.
- `EmotionalMagnitudeEngine magnitudeEngine`: An instance of the `EmotionalMagnitudeEngine` class that calculates the magnitude of emotions based on audio parameters.
- `EmotionalThroughputEngine throughputEngine`: An instance of the `EmotionalThroughputEngine` class that propagates the classified emotion through the system.
- `float pitch`: A float variable representing the pitch of the audio input.
- `float energy`: A float variable representing the energy level of the audio input.
- `float volume`: A float variable representing the volume of the audio input.
- `float speechRate`: A float variable representing the rate of speech in the audio input.
- `float[] spectralContent`: An array of floats representing the spectral content of the audio input, initialized to hold three values.

## Functions
- `void Update()`: This method is called once per frame. It classifies the current emotion using the `EmotionClassifier.ClassifyEmotion` method based on the audio parameters (pitch, energy, volume, speech rate, and spectral content). It then updates the emotional flux, sets the ambience, and propagates the emotion through the system using the respective engine methods. It also logs the current emotion, flux, ambience, and strongest emotion to the console for debugging purposes.