# VoiceSynthesizer

## Overview
The `VoiceSynthesizer` class is designed to convert text input into synthesized speech using simple phoneme-to-waveform mapping. This script fits into a Unity game or application that requires voice synthesis, allowing developers to generate audio clips from text dynamically. The synthesized audio is played through an `AudioSource` component attached to the same GameObject.

## Variables
- **audioSource**: An instance of `AudioSource` that plays the generated audio clip. This variable must be assigned in the Unity Editor to link the audio playback functionality.

## Functions
- **SynthesizeText(string text)**: This public method takes a string of text as input, converts it into phonemes, generates a corresponding waveform, and creates an audio clip from that waveform. Finally, it assigns the audio clip to the `audioSource` and plays it.

- **MapTextToPhonemes(string text)**: This private method converts the input text into an array of phonemes by splitting the text into lowercase words. This is a simplified approach and serves as a basic mapping mechanism.

- **GenerateWaveform(string[] phonemes)**: This private method generates a float array representing the waveform for the given phonemes. It creates a simple sine wave for each phoneme, simulating sound by calculating wave values based on a defined duration and sample rate. The resulting waveform is used to create the audio clip.