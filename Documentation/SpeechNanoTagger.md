# SpeechNanoTagger

## Overview
The `SpeechNanoTagger` class is designed to analyze audio clips and classify segments of speech based on various acoustic features. It segments the audio into smaller parts (nano segments) and extracts features such as pitch, energy, speech rate, and spectral content. The main function, `NanoTagSpeech`, returns a dictionary that maps timestamps to classified emotions for each nano segment. This functionality is crucial for applications that require emotion detection in speech, such as virtual assistants or interactive gaming.

## Variables
- `nanoSegments`: A two-dimensional array of floats that holds the segmented audio data from the input `AudioClip`. Each sub-array represents a segment of audio.
- `nanoTags`: A dictionary that maps a float (timestamp in seconds) to a string (the classified emotion) for each nano segment.

## Functions
### `public static Dictionary<float, string> NanoTagSpeech(AudioClip clip, int nanoSegmentLength)`
This is the main function of the `SpeechNanoTagger` class. It takes an `AudioClip` and an integer representing the length of each nano segment in milliseconds. It performs the following tasks:
1. Segments the audio clip into smaller parts using the `SpeechSegmenter.SegmentAudio` method.
2. Initializes an empty dictionary to store the results.
3. Iterates through each nano segment, extracting features (pitch, energy, speech rate, and spectral content) using the `FeatureExtractor` class.
4. Calculates a timestamp for each segment based on its index.
5. Classifies the emotion of the segment using the `EmotionClassifier.ClassifyEmotion` method and stores the result in the dictionary.
6. Returns the dictionary containing timestamps and their corresponding classified emotions.