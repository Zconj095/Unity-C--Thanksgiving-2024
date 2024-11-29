# EmotionalSynthesisAdmin

## Overview
The `EmotionalSynthesisAdmin` script is a Unity MonoBehaviour that manages the user interface (UI) for emotional synthesis. It handles the display of current emotions and their intensity based on user input from sliders. This script interacts with the `EmotionalEngine` component to update emotions dynamically as the user adjusts the pitch and energy sliders. It ensures that the UI components are properly assigned and provides feedback through logging.

## Variables

- `public Text currentEmotionDisplay`: A UI Text component that displays the current emotion.
- `public Text intensityValueText`: A UI Text component that shows the intensity of the current emotion.
- `public Text logOutput`: A UI Text component for logging events and messages.
- `public Slider pitchSlider`: A UI Slider that allows the user to adjust the pitch of the emotion.
- `public Slider energySlider`: A UI Slider that allows the user to adjust the energy of the emotion.
- `private EmotionalEngine emotionalEngine`: An instance of the `EmotionalEngine` class that processes the emotional data based on user inputs.

## Functions

- `private void Start()`: This function initializes the script. It validates the UI components and sliders, initializes the `EmotionalEngine`, attaches listeners to the sliders, and logs an initialization message.

- `private void OnPitchSliderChanged(float pitch)`: This function is called when the pitch slider value changes. It updates the emotion using the current pitch and the energy value from the energy slider.

- `private void OnEnergySliderChanged(float energy)`: This function is called when the energy slider value changes. It updates the emotion using the current energy and the pitch value from the pitch slider.

- `public void OnEmotionUpdate(float pitch, float energy)`: This function updates the emotion based on the pitch and energy values. It parses spectral content and calls the `UpdateEmotion` method from the `EmotionalEngine`, then updates the UI with the current emotion and intensity.

- `private float[] ParseSpectralContent(string input)`: This function attempts to parse a string input representing spectral content into an array of floats. It returns null if the input is invalid or improperly formatted.

- `public void UpdateUI(string emotion, float intensity)`: This function updates the UI elements to display the current emotion and its intensity. It checks if the UI components are assigned before updating.

- `public void LogEvent(string message)`: This function logs a message to the log output UI component and the Unity console.

- `private bool ValidateUIComponents()`: This function checks if all UI components and sliders are assigned in the Inspector. It returns true if all components are valid; otherwise, it logs an error and returns false.