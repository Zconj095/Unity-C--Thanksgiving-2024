# EmotionMetaTagger

## Overview
The `EmotionMetaTagger` class is designed to analyze audio clips and tag them with corresponding emotions based on various audio features. This functionality is essential for applications that require emotional context from audio, such as games or interactive media. The class processes an audio clip by dividing it into segments, extracting relevant features from each segment, and then classifying the emotion associated with those features. This class serves as a key component in the codebase for emotion recognition, enabling other systems to utilize the emotional context of audio data.

## Variables

- **sampleRate**: An integer representing the frequency of the audio clip, which indicates how many samples are taken per second.
- **samplesPerSegment**: An integer that calculates the number of audio samples in each segment based on the specified segment length in milliseconds.
- **totalSegments**: An integer representing the total number of segments the audio clip will be divided into.
- **emotionTags**: A dictionary that maps segment indices (of type int) to their corresponding classified emotions (of type string).
- **audioSamples**: An array of floats that holds the raw audio sample data from the audio clip.

## Functions

### TagEmotions
```csharp
public static Dictionary<int, string> TagEmotions(AudioClip clip, int segmentLengthMs)
```
- **Parameters**:
  - `AudioClip clip`: The audio clip to be analyzed.
  - `int segmentLengthMs`: The length of each segment in milliseconds.
- **Returns**: A dictionary where each key is an integer representing the segment index and each value is a string representing the classified emotion for that segment.
- **Description**: This function processes the provided audio clip by dividing it into segments of specified length. For each segment, it extracts audio features such as pitch, energy, volume, speech rate, and spectral content. It then classifies the emotion based on these features and stores the results in the `emotionTags` dictionary, which is returned at the end of the function.