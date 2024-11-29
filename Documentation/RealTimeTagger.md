# RealTimeTagger

## Overview
The `RealTimeTagger` script is a Unity component that continuously records audio from the microphone and processes it in real-time to classify emotions based on audio features. It segments the recorded audio into smaller chunks, extracts relevant features such as pitch, energy, and speech rate, and then uses these features to classify the emotional state of the speaker. This script is particularly useful in applications that require real-time feedback on emotional expression, such as games or interactive experiences.

## Variables
- `recordedClip`: An `AudioClip` object that stores the audio data recorded from the microphone.
- `segmentLength`: An integer that defines the length of each audio segment to be processed, set to 500 milliseconds.

## Functions
- `Start()`: This method is called before the first frame update. It initializes the audio recording by starting the microphone and continuously captures audio for a duration of 10 seconds at a sample rate of 44100 Hz.
  
- `Update()`: This method is called once per frame. It checks if the microphone is still recording. If it is, it segments the recorded audio into smaller parts using the `SpeechSegmenter.SegmentAudio` method. For each segment, it extracts audio features (pitch, energy, speech rate) using the `FeatureExtractor` class, and classifies the emotion using the `EmotionClassifier.ClassifyEmotion` method. The classified emotion is then logged to the console.