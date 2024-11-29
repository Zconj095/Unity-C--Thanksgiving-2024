# EmotionInputManager

## Overview
The `EmotionInputManager` script is responsible for managing user input related to emotional data within a Unity application. It interfaces with UI components such as sliders and input fields to capture and process emotion-related parameters, which are then sent to an `EmotionLayoutController` for further processing. This script is essential for enabling dynamic emotional responses based on user input, thereby enhancing the interactivity of the application.

## Variables
- **emotionController**: An instance of `EmotionLayoutController` that processes the emotional data captured by this script.
- **pitchSlider**: A UI Slider component that allows users to adjust the pitch value, which represents a parameter of emotion.
- **energySlider**: A UI Slider component that allows users to adjust the energy value, another parameter of emotion.
- **spectralContentInput**: An InputField component where users can input spectral content values as a comma-separated string.

## Functions
- **Start()**: This Unity lifecycle method is called before the first frame update. It adds listeners to the `pitchSlider` and `energySlider`, triggering the `UpdateEmotionData` function whenever their values change.
  
- **UpdateEmotionData(float value)**: This method is called whenever the pitch or energy slider value changes. It retrieves the current values from the sliders and parses the spectral content from the input field. It then calls the `ProcessEmotionData` method on the `emotionController`, passing the pitch, energy, a default speech rate, and the parsed spectral content.

- **ParseSpectralContent(string input)**: This private method takes a comma-separated string input and converts it into an array of floats. It splits the string by commas, attempts to parse the first three parts into floats, and returns an array containing these values. If fewer than three values are provided, the remaining elements of the array will remain at their default value of 0.