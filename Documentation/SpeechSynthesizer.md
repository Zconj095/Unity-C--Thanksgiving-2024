# SpeechSynthesizer

## Overview
The `SpeechSynthesizer` class is responsible for converting text input into synthesized speech represented as an `AudioClip`. This functionality is crucial for applications that require audio feedback or voice synthesis, such as games, educational tools, or accessibility features. The class leverages simple waveform generation techniques to create audio data based on the characters in the input text, which is then returned as an audio clip that can be played in Unity.

## Variables
- **text**: A `string` representing the input text that the speech synthesizer will convert into audio.
- **sampleRate**: An `int` that defines the number of samples per second for the audio output. The default value is set to 44100 Hz, which is standard for high-quality audio.
- **amplitude**: A `float` that controls the volume of the synthesized audio, with a default value of 0.5. This value ranges from 0.0 (silence) to 1.0 (maximum volume).

## Functions

### SynthesizeSpeech
```csharp
public static AudioClip SynthesizeSpeech(string text, int sampleRate = 44100, float amplitude = 0.5f)
```
This static method synthesizes speech from the provided text input and returns an `AudioClip`. It first generates an array of samples that represent the audio waveform using the `GenerateSpeechWaveform` method, then creates an `AudioClip` from these samples.

### GenerateSpeechWaveform
```csharp
private static float[] GenerateSpeechWaveform(string text, int sampleRate, float amplitude)
```
This private static method generates an array of float samples that represent the audio waveform for the given text. It calculates the total number of samples based on the length of the text and the sample rate, then iterates through each character to determine its corresponding frequency. For each character, it fills the samples array with sine wave values based on the calculated frequency.

### GetCharacterFrequency
```csharp
private static float GetCharacterFrequency(char c)
```
This private static method maps individual characters to specific frequencies for simple speech synthesis. It returns a frequency value based on the character input, with predefined frequencies for vowels and a default frequency for consonants. This mapping is essential for creating distinguishable sound waves for different characters.