# SpeechSegmenter

## Overview
The `SpeechSegmenter` class is designed to segment an audio clip into smaller parts based on a specified segment length. This functionality is particularly useful in applications such as speech processing, where analyzing smaller audio segments can help in tasks like speech recognition or audio analysis. The main function of this script, `SegmentAudio`, takes an audio clip and divides it into multiple segments of equal length, returning an array of these segments.

## Variables
- **sampleRate**: An integer that holds the frequency of the audio clip, which determines how many samples are played per second.
- **samplesPerSegment**: An integer that calculates the number of audio samples that correspond to the specified segment length in milliseconds.
- **totalSegments**: An integer that represents the total number of segments that can be created from the audio clip based on the length of each segment.
- **segments**: A jagged array of floats that will hold the individual audio segments extracted from the audio clip.
- **fullAudio**: A float array that stores all the audio samples from the audio clip.

## Functions
- **SegmentAudio(AudioClip clip, int segmentLength)**: This static method takes an `AudioClip` and an integer `segmentLength` (in milliseconds) as parameters. It calculates how many samples correspond to the segment length, determines how many segments can be created from the audio clip, and then populates a jagged array with these segments. The method returns the array of audio segments.